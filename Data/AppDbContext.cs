using Microsoft.EntityFrameworkCore;
using Web_API_Assessment.Models;

namespace Web_API_Assessment.Data
{
   
        public class AppDbContext : DbContext
        {
            public DbSet<User> Users { get; set; }
            public DbSet<Event> Events { get; set; }
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        }
    
}
