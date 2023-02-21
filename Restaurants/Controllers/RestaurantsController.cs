using Microsoft.AspNetCore.Mvc;
using Restaurants.Models.Contracts;
using Restaurants.Services.Interfaces;

namespace Restaurants.Controllers
{
    public class RestaurantsController : Controller
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantsController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        public async Task<IActionResult> Index()
        {
            var restaurants = await _restaurantService.GetAsync();

            return View(restaurants);
        }

        public IActionResult Create()
        {
            return View(new RestaurantItem());
        }

        [HttpPost]
        public async Task<IActionResult> Create(RestaurantItem request)
        {
            if (ModelState.IsValid)
            {
                var restaurant = await _restaurantService.AddAsync(request);

                return RedirectToAction(nameof(Index));
            }

            return View(request);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var restaurant = await _restaurantService.GetByIdAsync(id);
            return View(restaurant);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RestaurantItem request)
        {
            if (ModelState.IsValid)
            {
                var restaurant = await _restaurantService.UpdateAsync(request);

                return RedirectToAction(nameof(Index));
            }

            return View(request);
        }
    }
}
