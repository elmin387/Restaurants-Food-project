using Microsoft.EntityFrameworkCore;
using Restaurants.Models.Contracts;
using Restaurants.Models.Entities;
using Restaurants.Persistance;
using Restaurants.Services.Interfaces;

namespace Restaurants.Services.Implementations
{
    public class FoodService : IFoodService
    {
        private readonly ApplicationDbContext _dbContext;

        public FoodService(ApplicationDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<FoodItem>> GetAsync(int? restaurantId)
        {
            var foods = await _dbContext.Foods
                .Include(s=>s.Restaurant)
                .Where(s => restaurantId == null || s.RestaurantId == restaurantId)
                .Select(s => new FoodItem
                {
                   FoodId = s.FoodId,
                   Name = s.Name,
                   Price = s.Price,
                   RestaurantId = s.RestaurantId,
                   RestaurantName = s.Restaurant.Name
                }).ToListAsync();

            return foods;
        }

        public async Task<FoodItem> GetByIdAsync(int id)
        {
            var food = await _dbContext.Foods
                .Include(s => s.Restaurant)
                .FirstOrDefaultAsync(s => s.FoodId == id);

            if(food != null)
            {
                return new FoodItem
                {
                    FoodId = food.FoodId,
                    Name = food.Name,
                    Price = food.Price,
                    RestaurantId = food.RestaurantId,
                    RestaurantName = food.Restaurant.Name
                };
            }

            return new FoodItem();
        }

        public async Task<FoodItem> AddAsync(FoodItem request)
        {
            var entity = new Food
            {
                Name = request.Name,
                Price = request.Price,
                RestaurantId = request.RestaurantId
            };

            await _dbContext.Foods.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return await GetByIdAsync(entity.FoodId);
        }

        public async Task<FoodItem> UpdateAsync(FoodItem request)
        {
            var food = await _dbContext.Foods.FindAsync(request.FoodId);

            food.Name = request.Name;
            food.Price = request.Price;

            _dbContext.Foods.Update(food);
            await _dbContext.SaveChangesAsync();

            return await GetByIdAsync(food.FoodId);
        }

        public int? GetIdBySuggestion()
        {
            var suggestion = _dbContext.Foods
                .OrderBy(s => Guid.NewGuid())
                .Take(1);

            return suggestion.FirstOrDefault()?.FoodId;
        }
    }
}
