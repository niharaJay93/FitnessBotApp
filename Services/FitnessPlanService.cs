using System;
using System.Collections.Generic;
using System.Text;

public class FitnessPlanService
{
    public class Exercise
    {
        public string Name { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
    }

    public class WorkoutDay
    {
        public string MuscleGroup { get; set; }
        public List<Exercise> Exercises { get; set; } = new();
    }

    public class Meal
    {
        public string MealType { get; set; }
        public string Description { get; set; }
    }

    public class UserPlan
    {
        public List<WorkoutDay> WeeklyWorkouts { get; set; } = new();
        public List<Meal> DailyMeals { get; set; } = new();
    }

    private readonly Dictionary<string, Dictionary<string, UserPlan>> PlanMatrix = new(StringComparer.OrdinalIgnoreCase);

    public FitnessPlanService()
    {
        // Initialize your plan matrix here (same as before)
        PlanMatrix = new Dictionary<string, Dictionary<string, UserPlan>>(StringComparer.OrdinalIgnoreCase)
        {
            ["muscle gain"] = new Dictionary<string, UserPlan>(StringComparer.OrdinalIgnoreCase)
            {
                ["Ectomorph"] = new UserPlan
                {
                    WeeklyWorkouts = new List<WorkoutDay>
                    {
                        new WorkoutDay
                        {
                            MuscleGroup = "Chest Day",
                            Exercises = new List<Exercise>
                            {
                                new Exercise { Name = "Push-ups", Sets = 3, Reps = 12 },
                                new Exercise { Name = "Dumbbell Bench Press", Sets = 4, Reps = 8 },
                                new Exercise { Name = "Chest Flyes", Sets = 3, Reps = 10 }
                            }
                        },
                        new WorkoutDay
                        {
                            MuscleGroup = "Back Day",
                            Exercises = new List<Exercise>
                            {
                                new Exercise { Name = "Pull-ups", Sets = 3, Reps = 8 },
                                new Exercise { Name = "Barbell Rows", Sets = 4, Reps = 10 },
                                new Exercise { Name = "Lat Pulldown", Sets = 3, Reps = 12 }
                            }
                        }
                    },
                    DailyMeals = new List<Meal>
                    {
                        new Meal { MealType = "Breakfast", Description = "Oatmeal with banana and protein powder" },
                        new Meal { MealType = "Lunch", Description = "Grilled chicken breast with brown rice and vegetables" },
                        new Meal { MealType = "Dinner", Description = "Salmon with quinoa and steamed broccoli" }
                    }
                },
                // Add other body types and goals here...
            }
        };
    }

    public bool IsValidGoal(string goal)
{
    return PlanMatrix.ContainsKey(goal);
}


    public string GenerateCustomPlan(string goal, string bodyType, string fitnessLevel, float bodyWeight)
    {
        if (!PlanMatrix.TryGetValue(goal, out var bodyDict))
            return "Sorry, we don't have a plan for your goal.";

        if (!bodyDict.TryGetValue(bodyType, out var plan))
            return "Sorry, we don't have a plan for your body type.";

        var sb = new StringBuilder();

        sb.AppendLine($"🏋️‍♂️ **Workout Plan for {fitnessLevel} {bodyType} aiming for {goal} (Body weight: {bodyWeight} kg):**");
        sb.AppendLine();

        int dayNum = 1;
        foreach (var day in plan.WeeklyWorkouts)
        {
            sb.AppendLine($"Day {dayNum}: {day.MuscleGroup}");
            foreach (var ex in day.Exercises)
            {
                sb.AppendLine($" - {ex.Name}: {ex.Sets} sets x {ex.Reps} reps");
            }
            sb.AppendLine();
            dayNum++;
        }

        sb.AppendLine("🍎 **Daily Meal Plan:**");
        foreach (var meal in plan.DailyMeals)
        {
            sb.AppendLine($"- {meal.MealType}: {meal.Description}");
        }

        return sb.ToString();
    }
}
