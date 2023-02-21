using Microsoft.EntityFrameworkCore;
using Restaurants.Models.Contracts;
using Restaurants.Models.Entities;
using Restaurants.Persistance;
using Restaurants.Services.Interfaces;

namespace Restaurants.Services.Implementations
{
    public class RestaurantService : IRestaurantService
    {
        private readonly ApplicationDbContext _dbContext;

        public RestaurantService(ApplicationDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<RestaurantItem>> GetAsync()
        {
            var restaurants = await _dbContext.Restaurants
                .Select(s => new RestaurantItem
                {
                   RestaurantId = s.RestaurantId,
                   Name = s.Name,
                   Address = s.Address,
                   Telephone = s.Telephone
                }).ToListAsync();

            return restaurants;
        }

        public async Task<RestaurantItem> GetByIdAsync(int id)
        {
            var restaurant = await _dbContext.Restaurants.FindAsync(id);

            if(restaurant != null)
            {
                return new RestaurantItem
                {
                    RestaurantId = restaurant.RestaurantId,
                    Name = restaurant.Name,
                    Address = restaurant.Address,
                    Telephone = restaurant.Telephone
                };
            }

            return new RestaurantItem();
        }

        public async Task<RestaurantItem> AddAsync(RestaurantItem request)
        {
            var entity = new Restaurant
            {
                Name = request.Name,
                Address = request.Address,
                Telephone = request.Telephone
            };

            await _dbContext.Restaurants.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return await GetByIdAsync(entity.RestaurantId);
        }

        public async Task<RestaurantItem> UpdateAsync(RestaurantItem request)
        {
            var restaurant = await _dbContext.Restaurants.FindAsync(request.RestaurantId);

            restaurant.Name = request.Name;
            restaurant.Address = request.Address;
            restaurant.Telephone = request.Telephone;

            _dbContext.Restaurants.Update(restaurant);
            await _dbContext.SaveChangesAsync();

            return await GetByIdAsync(restaurant.RestaurantId);
        }
    }
}
