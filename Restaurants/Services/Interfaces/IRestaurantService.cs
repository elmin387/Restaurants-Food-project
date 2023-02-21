using Restaurants.Models.Contracts;

namespace Restaurants.Services.Interfaces
{
    public interface IRestaurantService
    {
        Task<IEnumerable<RestaurantItem>> GetAsync();
        Task<RestaurantItem> GetByIdAsync(int id);
        Task<RestaurantItem> AddAsync(RestaurantItem request);
        Task<RestaurantItem> UpdateAsync(RestaurantItem request);
    }
}
