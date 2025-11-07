using Microsoft.EntityFrameworkCore;
using FitnessBotApp.Models;

namespace FitnessBotApp.Data
{
    public class FitnessDbContext : DbContext
    {
        public FitnessDbContext(DbContextOptions<FitnessDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
