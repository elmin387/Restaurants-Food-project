using Microsoft.EntityFrameworkCore;
using Restaurants.Models.Entities;

namespace Restaurants.Persistance
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        { }
        

       public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Food> Foods { get; set; }
    }
}
