using Microsoft.AspNetCore.Mvc;
using RestaurantApi.Models;
using RestaurantApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantApi.Controllers
{
    [Route("/api/restaurant/{restaurantId}/dish")]
    [ApiController]
    public class DishController: ControllerBase
    {
        private readonly IDishService _dishService;

        public DishController(IDishService dishService)
        {
            _dishService = dishService;
        }

        [HttpGet("{dishId}")]
        public ActionResult<DishDto> Get([FromRoute] int restaurantId, [FromRoute] int dishId)
        {
            var dish = _dishService.GetById(restaurantId, dishId);
            return Ok(dish);
        }

        [HttpGet]
        public ActionResult<List<DishDto>> Get([FromRoute] int restaurantId)
        {
            var dishes = _dishService.GetAll(restaurantId);
            return Ok(dishes);
        }

        [HttpPost]
        public ActionResult Post([FromRoute]int restaurantId, [FromBody] CreateDishDto createDishDto)
        {
            var id = _dishService.Create(restaurantId, createDishDto);

            return Created($"/api/restaurantId/{restaurantId}/dish/{id}", null);
        }

        [HttpDelete("{dishId}")]
        public ActionResult Remove([FromRoute] int dishId)
        {
            _dishService.Delete(dishId);

            return NoContent();
        }

        [HttpDelete]
        public ActionResult RemoveAll([FromRoute] int restaurantId)
        {
            _dishService.DeleteAll(restaurantId);

            return NoContent();
        }
    }
}
