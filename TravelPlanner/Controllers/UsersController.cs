using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using TravelPlanner.Model.Entities;
using AutoMapper;
using TravelPlanner.Model.Repositories.IRepositories;
using TravelPlanner.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace TravelPlanner.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles ="admin,manager")]
    [Route("api/Users")]
    public class UsersController : Controller
    {
        private readonly UserManager<TravelUser> _userManager;
        private readonly ITravelUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(UserManager<TravelUser> userManager, ITravelUserRepository userRepository, IMapper mapper)
        {
            _userManager = userManager;
            _userRepository = userRepository;
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

        UserViewModel MapToVM(TravelUser user)
        {
            var vm =  _mapper.Map<UserViewModel>(user);
            vm.Role = GetUserRoles(user);
            return vm;
        }


        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                UserViewModel user = MapToVM(_userRepository.GetById(id));
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
                    IEnumerable<UserViewModel> users = _userRepository.GetUsers(email).Select(MapToVM).OrderByDescending(e => e.CreationDate);
                    int totalCount = users.Count();
                    users = users.Skip((pageIndex - 1) * pageSize).Take(pageSize);
                    return Ok(new { users, totalCount });
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
                if (user.Role == "admin" &&  !roles.Contains("admin"))
                    return Unauthorized();
                var updatedUser = _userRepository.Update(id, user, _userManager);
                if (updatedUser == null)
                    return NotFound();

                return Ok(updatedUser);
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
                UserViewModel user = MapToVM(_userRepository.GetById(id));
                var editor = await _userManager.FindByEmailAsync(User.Identity.Name);

                if (user.Id == editor.Id)
                    return BadRequest("You can't delete yourself");
                 var roles = await _userManager.GetRolesAsync(editor);
                if (user.Role == "admin" && !roles.Contains("admin"))
                    return Unauthorized();

                var deleted = _userRepository.Delete(id);
                if (deleted == null)
                    return NotFound();
                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
