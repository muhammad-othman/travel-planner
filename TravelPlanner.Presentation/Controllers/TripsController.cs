using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TravelPlanner.Presentation.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using TravelPlanner.Shared.Entities;
using TravelPlanner.CommandsServices.Trips;
using TravelPlanner.QueryServices.Trips;
using TravelPlanner.Shared.Enums;
using AutoMapper;

namespace TravelPlanner.Presentation.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[Controller]")]
    public class TripsController : Controller
    {
        private readonly UserManager<TravelUser> _userManager;
        private readonly ITripsWriteService _tripsWriteService;
        private readonly ITripsReadService _tripsReadService;
        private readonly IMapper _mapper;

        public TripsController(UserManager<TravelUser> userManager,
            ITripsWriteService tripsWriteService, ITripsReadService tripsReadService, IMapper mapper)
        {                                                           
            _userManager = userManager;
            _tripsWriteService = tripsWriteService;
            _tripsReadService = tripsReadService;
            _mapper = mapper;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(User.Identity.Name);
                var result = await _tripsReadService.GetTripById(user,id);

                if (result.Status == ResponseStatus.Unauthorized)
                    return Unauthorized();
                if (result.Status == ResponseStatus.Failed)
                    return BadRequest();

                TripViewModel Trip = _mapper.Map<TripViewModel>(result.Trip);

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
                    return await GetUserTrips(pageIndex, pageSize, from, to, destination, user);
                }
                else
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    if (!roles.Contains("admin"))
                        return Unauthorized();
                    return await GetAllTrips(pageIndex, pageSize, from, to, destination, user);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        private async Task<IActionResult> GetAllTrips(int pageIndex, int pageSize, DateTime? from, DateTime? to, string destination, TravelUser user)
        {
            var result = await _tripsReadService.GetAllTrips(user, from, to, destination, pageIndex, pageSize);

            if (result.Status == ResponseStatus.Unauthorized)
                return Unauthorized();
            if (result.Status == ResponseStatus.Failed)
                return BadRequest();

            IEnumerable<TripViewModel> Trips = result.Trips.Select(_mapper.Map<TripViewModel>);
            return Ok(new { Trips, result.TotalCount });
        }

        private async Task<IActionResult> GetUserTrips(int pageIndex, int pageSize, DateTime? from, DateTime? to, string destination, TravelUser user)
        {
            var result = await _tripsReadService.GetUserTrips(user, user.Id, from, to, destination, pageIndex, pageSize);

            if (result.Status == ResponseStatus.Unauthorized)
                return Unauthorized();
            if (result.Status == ResponseStatus.Failed)
                return BadRequest();

            IEnumerable<TripViewModel> Trips = result.Trips.Select(_mapper.Map<TripViewModel>);
            return Ok(new { Trips, result.TotalCount });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]TripViewModel trip)
        {

            if (!ModelState.IsValid) return BadRequest();
            try
            {
                var user = await _userManager.FindByEmailAsync(User.Identity.Name);
                var mappedTrip = _mapper.Map<Trip>(trip);
                mappedTrip.TravelUserId = user.Id;

                var result = await _tripsWriteService.CreateTripAsync(user, mappedTrip);

                if (result.Status == ResponseStatus.Unauthorized)
                    return Unauthorized();
                if (result.Status == ResponseStatus.Failed)
                    return BadRequest();
                return Ok(_mapper.Map<TripViewModel>(result.Trip));
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

                var result = await _tripsWriteService.UpdateTripAsync(user, _mapper.Map<Trip>(trip));

                if (result.Status == ResponseStatus.Unauthorized)
                    return Unauthorized();
                if (result.Status == ResponseStatus.Failed)
                    return BadRequest();
                return Ok(_mapper.Map<TripViewModel>(result.Trip));
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

                var result = await _tripsWriteService.DeleteTripAsync(user, id);

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