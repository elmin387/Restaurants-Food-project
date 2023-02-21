using System.ComponentModel.DataAnnotations;

namespace Restaurants.Models.Entities
{
    public class Food
    {
        [Key]
        public int FoodId { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
    }
}
