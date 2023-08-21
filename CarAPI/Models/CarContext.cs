using Microsoft.EntityFrameworkCore;

namespace CarAPI.Models
{
    public class CarContext : DbContext 
    {
        public CarContext(DbContextOptions<CarContext> options) : base(options)
        {
        }
        public DbSet<Cars> Cars { get; set; } = null!;
    }
    
}
