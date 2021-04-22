using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantApi.Entities;
using RestaurantApi.Exceptions;
using RestaurantApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantApi.Services
{
    public interface IDishService
    {
        int Create(int restaurantId, CreateDishDto createDishDto);
        DishDto GetById(int restaurantId, int dishId);
        List<DishDto> GetAll(int restaurantId);
        void Delete(int dishId);
        void DeleteAll(int restaurantId);
    }
    public class DishService: IDishService
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;

        public DishService(RestaurantDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public int Create(int restaurantId,  CreateDishDto dto)
        {
            var restaurant = GetRestaurantById(restaurantId);

            var dishEntity = _mapper.Map<Dish>(dto);
            dishEntity.RestaurantId = restaurantId;

            _dbContext.Dishes.Add(dishEntity);
            _dbContext.SaveChanges();

            return dishEntity.Id; 
        }
        public DishDto GetById(int restaurantId, int dishId)
        {
            var restaurant = GetRestaurantById(restaurantId);

            var dish = _dbContext.Dishes.FirstOrDefault(d => d.Id == dishId);
            if (dish is null ||  dish.RestaurantId != restaurantId)
            {
                throw new NotFoundException("Dish not found");
            }

            var dishDto = _mapper.Map<DishDto>(dish);
            return dishDto;
        }

        public List<DishDto> GetAll(int restaurantId)
        {
            Restaurant restaurant = GetRestaurantById(restaurantId);

            var dishDto = _mapper.Map<List<DishDto>>(restaurant.Dishes);
            return dishDto;
        }


        public void Delete(int dishId)
        {
            var dish = _dbContext.Dishes.FirstOrDefault(d => d.Id == dishId);
            if (dish is null)
                throw new NotFoundException("Dish not found");

            _dbContext.Dishes.Remove(dish);
            _dbContext.SaveChanges();
        }

        public void DeleteAll(int restaurantId)
        {
            var restaurant = GetRestaurantById(restaurantId);

            _dbContext.RemoveRange(restaurant.Dishes);
            _dbContext.SaveChanges();
        }
        private Restaurant GetRestaurantById(int restaurantId)
        {
            var restaurant = _dbContext
                .Restaurants
                .Include(r => r.Dishes)
                .FirstOrDefault(r => r.Id == restaurantId);
            if (restaurant is null)
                throw new NotFoundException("Not found restaurant");
            return restaurant;
        }
    }
}
