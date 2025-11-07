using Microsoft.EntityFrameworkCore;
using FitnessBotApp.Models;

namespace FitnessBotApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<WorkoutPlan> WorkoutPlans { get; set; }
        public DbSet<MealPlan> MealPlans { get; set; }
    }
}
