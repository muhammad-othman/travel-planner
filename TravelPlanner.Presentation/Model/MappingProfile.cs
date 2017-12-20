using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPlanner.Presentation.Model.Entities;
using TravelPlanner.Presentation.ViewModels;

namespace TravelPlanner.Presentation.Model
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Trip, TripViewModel>().
                ForMember(vm => vm.UserEmail,
                ex => ex.MapFrom(s => s.TravelUser.Email)).
                ForMember(vm=>vm.Sightseeings,
                ex=>ex.MapFrom(s=> ConvertToList(s)));


            CreateMap<TripViewModel, Trip>().
               ForMember(source => source.SightseeingsCollection,
               ex => ex.MapFrom(s => string.Join(",", s.Sightseeings)));

            CreateMap<TravelUser, UserViewModel>().
                ForMember(source => source.isLocked, u => u.MapFrom(s => s.LockoutEnd != null));

        }

        

        private object ConvertToList(Trip s)
        {
            return s.SightseeingsCollection.Split(',').ToList();
        }
    }
}
