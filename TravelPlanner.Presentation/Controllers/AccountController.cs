using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TravelPlanner.CommandsServices.Users;
using TravelPlanner.Presentation.Services;
using TravelPlanner.Presentation.ViewModels;
using TravelPlanner.Shared.Entities;
using TravelPlanner.Shared.Enums;

namespace TravelPlanner.Presentation.Controllers
{
    [Route("api/[Controller]")]
    public class AccountController : Controller
    {
        private readonly SignInManager<TravelUser> _signInManager;
        private readonly UserManager<TravelUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;
        private readonly IUsersWriteService _usersWriteService;

        public AccountController(SignInManager<TravelUser> signInManager,
            UserManager<TravelUser> userManager,
            IConfiguration configuration,
            IEmailSender emailSender,
            IUsersWriteService usersWriteService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
            _emailSender = emailSender;
            _usersWriteService = usersWriteService;
        }
        public IActionResult Get()
        {
            return Ok("TravelPlannerApp");
        }

        [Route("invite/{email}")]
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]

        private string GenerateInvitationMessage(bool isHtml, string callbackURL)
        {
            if (!isHtml)
                return "I would like to invite you to join our Travel planner at " + callbackURL;
            
            return "I would like to invite you to join our Travel planner at  <br> < a href = '" + callbackURL + "' > Travel Planner </ a > + ";
        }

        public async Task<IActionResult> InviteUserAsync(string email)
        {
            var message = GenerateInvitationMessage(false, _configuration["PresentationUrl"]);
            var htmlmessage = GenerateInvitationMessage(true, _configuration["PresentationUrl"]);

            var apiKey = _configuration["SendGrid:SendGridKey"];
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("admin@travelplanner.com", "TravelPlanner Admin"),
                Subject = "Travel Planner Invitation",
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));
            var resoponse = await client.SendEmailAsync(msg);
            return Ok();
        }
        [HttpGet]
        [Route("ExternalLogin")]
        public IActionResult ExternalLogin(string provider)
        {
            var redirectUrl = "/api/Account/ExternalLoginCallback";
            if (provider.ToLower() == "facebook")
                provider = FacebookDefaults.AuthenticationScheme;
            else if (provider.ToLower() == "google")
                provider = GoogleDefaults.AuthenticationScheme;
            else
                return NotFound();
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterViewModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
                return BadRequest("Your registeration info are invalid");
            var user = new TravelUser { NormalizedEmail = model.Email, Email = model.Email };
            user.CreationDate = DateTime.Now;
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return BadRequest("Something Went Wrong");

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
            await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
                return BadRequest("Something Went Wrong");
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return BadRequest();
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
                return Redirect(_configuration["PresentationUrl"]);
            return BadRequest("Couldn't Confirm your Email, Please Try Again");
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }


        [HttpGet]
        [Route("ExternalLoginCallback")]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
                BadRequest(remoteError);

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
                return NotFound();
            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

            TravelUser user = null;
            if (result.Succeeded)
                user = await _userManager.FindByEmailAsync(email);
            else //User doesn't exist so create a new user to this external login info
            {
                string picture = await GetPictureUrlAsync(info);
                
                user = new TravelUser { NormalizedEmail = email, Email = email, Picture = picture, EmailConfirmed = true };
                user.CreationDate = DateTime.Now;
                var createdUser = await _userManager.CreateAsync(user);

            }
            var signinResult = await _userManager.AddLoginAsync(user, info);
            if (signinResult.Succeeded)
                await _signInManager.SignInAsync(user, isPersistent: false);

            var jwt = await GenerateJwT(user);
            var encoded = _configuration["PresentationUrl"] + "/externallogin/" + jwt.token;
            return Redirect(encoded);
        }

        private async Task<string> GetPictureUrlAsync(ExternalLoginInfo info)
        {
            string picture = string.Empty;
            if (info.LoginProvider == FacebookDefaults.AuthenticationScheme)
            {
                var identifier = info.Principal.FindFirstValue(ClaimTypes.NameIdentifier);
                picture = $"https://graph.facebook.com/{identifier}/picture?type=large";
            }
            else if (info.LoginProvider == GoogleDefaults.AuthenticationScheme)
            {
                string googleApiKey = _configuration["SocialMediaAuthentication:Google:ApiKey"];
                string nameIdentifier = info.Principal.FindFirstValue(ClaimTypes.NameIdentifier);
                var jsonUrl = $"https://www.googleapis.com/plus/v1/people/{nameIdentifier}?fields=image&key={googleApiKey}";
                using (HttpClient httpClient = new HttpClient())
                {
                    dynamic deserializeObject = JsonConvert.DeserializeObject(await httpClient.GetStringAsync(jsonUrl));
                    picture = (string)deserializeObject.image.url;
                }
            }
            return picture;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody]LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest("Check your email and password");

            var user = await _userManager.FindByEmailAsync(viewModel.Email);

            if (user == null)
                return NotFound(viewModel.Email + " Isn't Found");
            var result = await _signInManager.CheckPasswordSignInAsync(user, viewModel.Password, true);

            if (!result.Succeeded)
            {
                string message = "Wrong Password";

                if (result.IsLockedOut)
                    message = "Your Account is Locked, Please Contact the Manager";

                else if (result.IsNotAllowed)
                    message = "You aren't allowed to Login";

                return BadRequest(message);
            }
            var jwt = await GenerateJwT(user);
            return Ok(jwt);
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("Picture")]
        public async Task<IActionResult> UploadPicture(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Content("file not selected");

            var user = await _userManager.FindByEmailAsync(User.Identity.Name);

            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot",
                        user.Email+file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            user.Picture = user.Email + file.FileName;
            var response = await _usersWriteService.UpdateUserAsync(user);

            if (response.Status == ResponseStatus.Failed)
                return BadRequest();
            else if (response.Status == ResponseStatus.Unauthorized)
                return Unauthorized();

            return Ok(user.Picture);
        }

        private async Task<dynamic> GenerateJwT(TravelUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var picture = user.Picture ?? "";
            var claims = new[]
            {
              new Claim(JwtRegisteredClaimNames.Sub, user.Email),
              new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
              new Claim(JwtRegisteredClaimNames.UniqueName, user.Email),
              new Claim("picture", picture),
              new Claim("roles",  string.Join(",",roles))
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
              issuer: _configuration["Tokens:Issuer"],
              audience: _configuration["Tokens:Audience"],
              claims: claims,
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: credentials);
            var results = new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            };

            return results;
        }
    }
}
