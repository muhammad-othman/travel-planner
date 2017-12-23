using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using TravelPlanner.Presentation.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using TravelPlanner.Shared.Entities;
using TravelPlanner.QueryServices.Users;
using TravelPlanner.CommandsServices.Users;
using TravelPlanner.Shared.Enums;

namespace TravelPlanner.Presentation.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles ="admin,manager")]
    [Route("api/Users")]
    public class UsersController : Controller
    {
        private readonly UserManager<TravelUser> _userManager;
        private readonly IUsersWriteService _usersWriteService;
        private readonly IUsersReadService _usersReadService;
        private readonly IMapper _mapper;

        public UsersController(UserManager<TravelUser> userManager,
            IUsersWriteService usersWriteService, IUsersReadService usersReadService, IMapper mapper)
        {
            _userManager = userManager;
            _usersWriteService = usersWriteService;
            _usersReadService = usersReadService;
            _mapper = mapper;
        }
        [HttpGet("{id}")]

        private string GetUserRoles(TravelUser s)
        {
            var roles = _userManager.GetRolesAsync(s).Result;
            if (roles.Contains("admin"))
                return "admin";
            if (roles.Contains("manager"))
                return "manager";
            return "user";
        }

        UserViewModel MapToUserToUserVM(TravelUser user)
        {
            var vm =  _mapper.Map<UserViewModel>(user);
            vm.Role = GetUserRoles(user);
            return vm;
        }


        public IActionResult GetById(string id)
        {
            try
            {
                UserViewModel user = MapToUserToUserVM(_usersReadService.GetUserById(id).Result.User);
                return Ok(user);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public async Task<IActionResult> Get(int pageIndex = 1, int pageSize = 10, string email = null)
        {
            try
            {
                var currentUser = await _userManager.FindByEmailAsync(User.Identity.Name);
                var result = await _usersReadService.GetAllUsersAsync(currentUser, email, pageIndex,pageSize);
                if (result.Status == ResponseStatus.Unauthorized)
                    return Unauthorized();
                if (result.Status == ResponseStatus.Failed)
                    return BadRequest();

                IEnumerable<UserViewModel> users = result.Users.Select(MapToUserToUserVM).OrderByDescending(e => e.CreationDate);
                return Ok(new { users, result.TotalCount });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody]UserViewModel user)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                var editor = await _userManager.FindByEmailAsync(User.Identity.Name);
                var roles = await _userManager.GetRolesAsync(editor);
                if (user.Role == "admin" && !roles.Contains("admin"))
                    return Unauthorized();

               
                var result = await _usersWriteService.UpdateUserAsync(_mapper.Map<TravelUser>(user));

                if (result.Status == ResponseStatus.Unauthorized)
                    return Unauthorized();
                if (result.Status == ResponseStatus.Failed)
                    return BadRequest();

                var updatedUser = result.User;

                if (updatedUser == null)
                    return NotFound();

                return Ok(MapToUserToUserVM(updatedUser));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                UserViewModel user = MapToUserToUserVM(_usersReadService.GetUserById(id).Result.User);
                var editor = await _userManager.FindByEmailAsync(User.Identity.Name);

                if (user.Id == editor.Id)
                    return BadRequest("You can't delete yourself");
                 var roles = await _userManager.GetRolesAsync(editor);
                if (user.Role == "admin" && !roles.Contains("admin"))
                    return Unauthorized();

                var result = await _usersWriteService.DeleteUserAsync(id);

                if (result.Status == ResponseStatus.Unauthorized)
                    return Unauthorized();
                if (result.Status == ResponseStatus.Failed)
                    return BadRequest();

                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
