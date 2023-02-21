using Restaurants.Models.Contracts;

namespace Restaurants.Services.Interfaces
{
    public interface IFoodService
    {
        Task<IEnumerable<FoodItem>> GetAsync(int? restaurantId);
        Task<FoodItem> GetByIdAsync(int id);
        Task<FoodItem> AddAsync(FoodItem request);
        Task<FoodItem> UpdateAsync(FoodItem request);
        int? GetIdBySuggestion();
    }
}
