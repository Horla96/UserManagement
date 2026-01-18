using Microsoft.EntityFrameworkCore;
using UserManagement_API.Models;

namespace UserManagement_API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
