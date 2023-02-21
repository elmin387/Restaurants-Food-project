using System.ComponentModel.DataAnnotations;

namespace Restaurants.Models.Contracts
{
    public class RestaurantItem
    {
        public int? RestaurantId { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        [StringLength(200)]
        public string? Address { get; set; }
        [StringLength(30)]
        public string? Telephone { get; set; }
    }
}
