using AutoMapper;
using RestaurantApi.Entities;
using RestaurantApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantApi
{
    public class RestaurantMappingProfile: Profile
    {
        public RestaurantMappingProfile()
        {
            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(r => r.City, c => c.MapFrom(s => s.Address.City))
                .ForMember(r => r.Street, c => c.MapFrom(s => s.Address.Street))
                .ForMember(r => r.PostalCode, c => c.MapFrom(s => s.Address.Street));

            CreateMap<Dish, DishDto>();

            CreateMap<CreateRestaurantDto, Restaurant>()
                .ForMember(t => t.Address, s => s.MapFrom(dto => new Address()
                {
                   City = dto.City, PostalCode = dto.PostalCode, Street = dto.Street 
                }));

            CreateMap<UpdateRestaurantDto, Restaurant>();

            CreateMap<CreateDishDto, Dish>();
        }
    }
}
