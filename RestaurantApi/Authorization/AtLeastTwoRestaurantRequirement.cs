using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantApi.Authorization
{
    public class AtLeastTwoRestaurantRequirement: IAuthorizationRequirement
    {
        public int MinimumRestaurantsCreated { get; }

        public AtLeastTwoRestaurantRequirement(int minimumRestaurants)
        {
            MinimumRestaurantsCreated = minimumRestaurants;
        }
    }
}
