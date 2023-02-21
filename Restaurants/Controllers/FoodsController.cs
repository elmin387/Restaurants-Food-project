using Microsoft.AspNetCore.Mvc;
using Restaurants.Models.Contracts;
using Restaurants.Services.Interfaces;

namespace Restaurants.Controllers
{
    public class FoodsController : Controller
    {
        private readonly IFoodService _foodService;

        public FoodsController(IFoodService foodService)
        {
            _foodService = foodService;
        }

        
        public async Task<IActionResult> Index(int? restaurantId)
        {
            var foods = await _foodService.GetAsync(restaurantId);

            return View(foods);
        }

        public IActionResult Create(int restaurantId)
        {
            return View(new FoodItem { RestaurantId = restaurantId});
        }

        [HttpPost]
        public async Task<IActionResult> Create(FoodItem request)
        {
            if (ModelState.IsValid)
            {
                var food = await _foodService.AddAsync(request);

                return RedirectToAction(nameof(Index),
                    new { restaurantId = food.RestaurantId });
            }

            return View(request);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var food = await _foodService.GetByIdAsync(id);
            return View(food);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(FoodItem request)
        {
            if (ModelState.IsValid)
            {
                var food = await _foodService.UpdateAsync(request);

                return RedirectToAction(nameof(Index),
                    new { restaurantId = food.RestaurantId });
            }

            return View(request);
        }

        public IActionResult Suggest()
        {
            var foodId = _foodService.GetIdBySuggestion();
            if(foodId != null)
            {
                return RedirectToAction(nameof(Index), "Home",
                            new { foodId = foodId });
            }

            return RedirectToAction(nameof(Index), "Home");
        }
    }
}
