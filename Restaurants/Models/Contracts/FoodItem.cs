using System.ComponentModel.DataAnnotations;

namespace Restaurants.Models.Contracts
{
    public class FoodItem
    {
       
        public int? FoodId { get; set; }
        [StringLength (100)]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int RestaurantId { get; set; }
        public string? RestaurantName { get; set; }
    }
}
