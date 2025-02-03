using Microsoft.EntityFrameworkCore;

namespace SingleSignIn.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<AppUser> GoogleUsers { get; set; }
    }
}
