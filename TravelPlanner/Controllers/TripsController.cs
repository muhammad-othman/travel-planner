using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelPlanner.Model;
using TravelPlanner.Model.Entities;
using Microsoft.AspNetCore.Authorization;
using TravelPlanner.Model.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TravelPlanner.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors;

namespace TravelPlanner.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[Controller]")]
    public class TripsController : Controller
    {
        private readonly UserManager<TravelUser> _userManager;
        private readonly ITripRepository _tripRepository;
        private readonly IMapper _mapper;

        public TripsController(UserManager<TravelUser> userManager, ITripRepository tripRepository, IMapper mapper)
        {
            _userManager = userManager;
            _tripRepository = tripRepository;
            _mapper = mapper;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(User.Identity.Name);
                TripViewModel Trip= _mapper.Map<TripViewModel>(_tripRepository.GetById(id));
                var roles = await _userManager.GetRolesAsync(user);
                    if (Trip.UserEmail != user.Email && !roles.Contains("admin"))
                    return NotFound();
                return Ok(Trip);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Get(int pageIndex = 1, int pageSize = 10, DateTime? from = null, DateTime? to = null, string destination = null, bool alltrips = false)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(User.Identity.Name);
                if (!alltrips)
                {
                    IEnumerable<TripViewModel> Trips = _tripRepository.GetUserTrips(user.Id, from, to, destination).Select(_mapper.Map<TripViewModel>).OrderByDescending(e => e.StartDate);
                    int totalCount = Trips.Count();
                    Trips = Trips.Skip((pageIndex - 1) * pageSize).Take(pageSize);
                    return Ok(new { Trips, totalCount });
                }
                else
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    if (!roles.Contains("admin"))
                        return Unauthorized();
                    IEnumerable<TripViewModel> Trips = _tripRepository.GetTrips(from, to, destination).Select(_mapper.Map<TripViewModel>).OrderByDescending(e => e.StartDate);
                    int totalCount = Trips.Count();
                    Trips = Trips.Skip((pageIndex - 1) * pageSize).Take(pageSize);
                    return Ok(new { Trips, totalCount });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]TripViewModel trip)
        {

            if (!ModelState.IsValid) return BadRequest();
            try
            {
                var user = await _userManager.FindByEmailAsync(User.Identity.Name);
                var t = _mapper.Map<Trip>(trip);
                t.TravelUserId = user.Id;
                var createdTrip = _tripRepository.Create(t);
                if (createdTrip != null)
                    return Ok(createdTrip);
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]TripViewModel trip)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                trip.Id = id;
                var user = await _userManager.FindByEmailAsync(User.Identity.Name);
                var roles = await _userManager.GetRolesAsync(user);
                if (trip.UserEmail != user.Email && !roles.Contains("admin"))
                    return NotFound();
                var updatedTrip = _tripRepository.Update(id,_mapper.Map<Trip>(trip));
                if (updatedTrip == null)
                    return NotFound();

                return Ok(Mapper.Map<TripViewModel>(updatedTrip));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(User.Identity.Name);
                TripViewModel Trip = _mapper.Map<TripViewModel>(_tripRepository.GetById(id));
                var roles = await _userManager.GetRolesAsync(user);
                if (Trip.UserEmail != user.Email && !roles.Contains("admin"))
                    return NotFound();
                var deleted = _tripRepository.Delete(id);
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