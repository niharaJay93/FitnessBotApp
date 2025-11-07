using FitnessBotApp.Data;
using FitnessBotApp.Models;

public static class DbSeeder
{
    public static void Seed(ApplicationDbContext context)
    {
        if (!context.MealPlans.Any())
        {
            context.MealPlans.AddRange(
                new MealPlan { Id = 1,MealDetails="Muscle Gain Plan", /* other properties */ },
                new MealPlan { Id = 2, MealDetails = "Fat Loss Plan", /* other properties */ }
            );
        }

        if (!context.WorkoutPlans.Any())
        {
            context.WorkoutPlans.AddRange(
                new WorkoutPlan { Id = 1, PlanDetails = "Chest and Back", /* other properties */ },
                new WorkoutPlan { Id = 2 , PlanDetails =  "Leg Day", /* other properties */ }
            );
        }

        context.SaveChanges();
    }
}
