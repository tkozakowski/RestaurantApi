using RestaurantApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantApi
{
    public class RestaurantSeeder
    {
        private readonly RestaurantDbContext _context;

        public RestaurantSeeder(RestaurantDbContext context)
        {
            _context = context;
        }
        public void Seed()
        {

            if (_context.Database.CanConnect())
            {
                if (!_context.Roles.Any())
                {
                    var roles = GetRoles();
                    _context.Roles.AddRange(roles);
                    _context.SaveChanges();
                }

                if (!_context.Restaurants.Any())
                {
                    var restaurants = GetRestaurants();
                    _context.AddRange(restaurants);
                    _context.SaveChanges();
                }
            }
        }

        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "User"
                },
                new Role()
                {
                    Name = "Manager"
                },
                new Role()
                {
                    Name = "Admin"
                }
            };
            return roles;
        }

        private IEnumerable<Restaurant> GetRestaurants()
        {
            var restaurants = new List<Restaurant>()
            {
                new Restaurant
                {
                    Name = "KFC",
                    Category = "Fast Food",
                    Description = "Junk food",
                    Address = new Address()
                    {
                        City = "Pruszków",
                        PostalCode = "05-808",
                        Street = "Działkowa"
                    },
                    Dishes = new List<Dish>()
                    {
                        new Dish()
                        {
                            Name = "Chicken",
                            Description = "chicken legs",
                            Price = 8.20M,
                        },
                        new Dish()
                        {
                            Name = "Nashwille",
                            Description = "chicken legs2",
                            Price = 5.80M,
                        }
                    }
                },
                new Restaurant
                {
                    Name = "KFC",
                    Category = "Fast Food",
                    Description = "Junk food",
                    Address = new Address()
                    {
                        City = "Pruszków",
                        PostalCode = "05-808",
                        Street = "Działkowa"
                    },
                    Dishes = new List<Dish>()
                    {
                        new Dish()
                        {
                            Name = "Chicken",
                            Description = "chicken legs",
                            Price = 8.20M,
                        },
                        new Dish()
                        {
                            Name = "Nashwille",
                            Description = "chicken legs2",
                            Price = 5.80M,
                        }
                    }
                },
            };
            return restaurants;
        }
    }
}
