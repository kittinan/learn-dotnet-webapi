using Microsoft.EntityFrameworkCore;
using TunApi.Models;

namespace TunApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Todo> Todo { get; set; }
    }
}