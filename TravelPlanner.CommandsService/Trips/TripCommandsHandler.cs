﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TravelPlanner.CommandsServices.Trips;
using TravelPlanner.CommandsServices.Trips.Commands;
using TravelPlanner.Shared.Entities;
using TravelPlanner.Shared.Enums;
using TravelPlanner.Shared.IRepos;

namespace TravelPlanner.CommandsServices.Trips
{
    public class TripCommandsHandler : 
        IRequestHandler<CreateTripCommand, TripCommandResponse>, 
        IRequestHandler<DeleteTripCommand, TripCommandResponse>, 
        IRequestHandler<UpdateTripCommand, TripCommandResponse>
    {
        private readonly ITripsWriteRepo _repo;

        public TripCommandsHandler(ITripsWriteRepo repo)
        {
            _repo = repo;
        }
        public Task<TripCommandResponse> Handle(CreateTripCommand request, CancellationToken cancellationToken)
        {
            Trip Trip = _repo.CreateTrip(request.Data);
            var response = new TripCommandResponse(Trip);
            if (Trip != null)
                response.Status = ResponseStatus.Succeeded;
            else
                response.Status = ResponseStatus.Failed;

            return Task.FromResult(response);
        }

        public Task<TripCommandResponse> Handle(UpdateTripCommand request, CancellationToken cancellationToken)
        {
            Trip Trip = _repo.UpdateTrip(request.Data);
            var response = new TripCommandResponse(Trip);
            if (Trip != null)
                response.Status = ResponseStatus.Succeeded;
            else
                response.Status = ResponseStatus.Failed;

            return Task.FromResult(response);
        }

        public Task<TripCommandResponse> Handle(DeleteTripCommand request, CancellationToken cancellationToken)
        {
            Trip Trip = _repo.DeleteTrip(request.TripId);
            var response = new TripCommandResponse(Trip);
            if (Trip != null)
                response.Status = ResponseStatus.Succeeded;
            else
                response.Status = ResponseStatus.Failed;

            return Task.FromResult(response);
        }
    }
}
