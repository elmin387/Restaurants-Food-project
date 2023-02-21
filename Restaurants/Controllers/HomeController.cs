using Microsoft.AspNetCore.Mvc;
using Restaurants.Models;
using Restaurants.Models.Contracts;
using Restaurants.Services.Interfaces;
using System.Diagnostics;

namespace Restaurants.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFoodService _foodService;

        public HomeController(ILogger<HomeController> logger,
            IFoodService foodService)
        {
            _logger = logger;
            _foodService = foodService;
        }

        public async Task<IActionResult> Index(int? foodId)
        {
            if(foodId != null)
            {
                var food = await _foodService.GetByIdAsync((int)foodId);
                return View(food);
            }

            return View(new FoodItem());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}