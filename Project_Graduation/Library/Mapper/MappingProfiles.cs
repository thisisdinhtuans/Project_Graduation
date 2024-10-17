using System;
using AutoMapper;
using Domain.Models.Dto.Restaurant;
using Infrastructure.Entities;

namespace Library.Mapper;

public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateRestaurantDto, Restaurant>();
            CreateMap<RestaurantDto, Restaurant>();
            CreateMap<Restaurant,RestaurantDto>();

    }
}
