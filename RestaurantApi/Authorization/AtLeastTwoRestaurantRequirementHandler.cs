﻿using Microsoft.AspNetCore.Authorization;
using RestaurantApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RestaurantApi.Authorization
{
    public class AtLeastTwoRestaurantRequirementHandler : AuthorizationHandler<AtLeastTwoRestaurantRequirement>
    {
        private readonly RestaurantDbContext _dbContext;

        public AtLeastTwoRestaurantRequirementHandler(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, 
            AtLeastTwoRestaurantRequirement requirement)
        {
            var userId = int.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
            
            var createdRestaurantsCount = _dbContext.Restaurants.Where(r => r.CreatedById == userId).Count();

            if(createdRestaurantsCount >= requirement.MinimumRestaurantsCreated)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
