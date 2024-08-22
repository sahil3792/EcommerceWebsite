using E_commerce_webApp.Models;
using Microsoft.EntityFrameworkCore;

namespace E_commerce_webApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions <ApplicationDbContext> options):base(options) { 
        
        }

        public DbSet<User> Users { get; set; }
    }
}
