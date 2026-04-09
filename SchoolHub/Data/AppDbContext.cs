using Microsoft.EntityFrameworkCore;
using SchoolHub.Models;


namespace SchoolHub.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users {  get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
        
        }
    }
}
