using System.Text;
using FitnessBotApp.Data;
using FitnessBotApp.Models;

public class FitnessService
{
    private readonly ApplicationDbContext _context;

    public FitnessService(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<WorkoutPlan> GetWorkoutPlans() => _context.WorkoutPlans.ToList();

    public IEnumerable<MealPlan> GetMealPlans() => _context.MealPlans.ToList();

    private double bmiValue;

    /*public string GeneratePlan(UserInputData input)
        {
            var sb = new StringBuilder();

            sb.AppendLine("✅ **Your Personalized Fitness Plan**");
            sb.AppendLine($"🎯 Goal: {input.Goal}");
            sb.AppendLine($"🏋️‍♂️ Workout Days: {input.DaysPerWeek} / week");
            sb.AppendLine($"📊 Body Type: {input.BodyType}");
            sb.AppendLine($"⚖️ Weight: {input.Weight} kg, 📏 Height: {input.Height} cm");
            sb.AppendLine($"🍽️ Diet: {input.DietPreference}");
            sb.AppendLine();
            sb.AppendLine("---");

            // Example Workout Schedule
            string[] workoutDays = { "Chest & Triceps", "Back & Biceps", "Shoulders", "Leg Day", "Core & Cardio" };

            sb.AppendLine("📅 **Workout Plan:**");

            for (int i = 0; i < input.DaysPerWeek; i++)
            {
                var workout = i < workoutDays.Length ? workoutDays[i] : "Full Body";
                sb.AppendLine($"Day {i + 1}: {workout}");
            }

            sb.AppendLine();
            sb.AppendLine("🥗 **Daily Meal Plan (5 Meals):**");

            var meals = input.DietPreference?.ToLower() == "veg"
                ? GetVegMeals(input.Weight)
                : GetNonVegMeals(input.Weight);

            for (int i = 0; i < meals.Length; i++)
            {
                sb.AppendLine($"Meal {i + 1}: {meals[i]}");
            }

            return sb.ToString();
        }

        private string[] GetNonVegMeals(double weight)
        {
            return new string[]
            {
                $"Breakfast: 4 eggs, 2 slices whole grain bread, 200ml milk",
                $"Snack: Protein shake, 1 banana",
                $"Lunch: 200g rice, 150g chicken, vegetables",
                $"Snack: 1 handful almonds, yogurt",
                $"Dinner: 150g grilled fish, quinoa, steamed broccoli"
            };
        }

        private string[] GetVegMeals(double weight)
        {
            return new string[]
            {
                $"Breakfast: Oats with 200ml soy milk, 1 apple",
                $"Snack: Peanut butter sandwich, 1 boiled egg",
                $"Lunch: 200g rice, 150g paneer, mixed vegetables",
                $"Snack: Greek yogurt, 1 banana",
                $"Dinner: 2 chapatis, 150g tofu, salad"
            };
        }
        */

    public string GeneratePlan(UserInputData input)
    {
        var sb = new StringBuilder();
        string[] workoutdays = new string[input.DaysPerWeek];
        string[] meal = new string[5];

        /*sb.AppendLine("✅ **Your Personalized Fitness Plan**" + Environment.NewLine);
        sb.AppendLine($"🎯 Goal: {input.Goal}" + Environment.NewLine);
        sb.AppendLine($"🏋️‍♂️ Workout Days: {input.DaysPerWeek} / week" + Environment.NewLine);
        sb.AppendLine($"📊 Body Type: {input.BodyType}" + Environment.NewLine);
        sb.AppendLine($"⚖️ Weight: {input.Weight} kg, 📏 Height: {input.Height} cm" + Environment.NewLine);
        sb.AppendLine($"🍽️ Diet: {input.DietPreference}" + Environment.NewLine);
        sb.AppendLine();
        sb.AppendLine("---" + Environment.NewLine);*/

        sb.AppendLine("📅 **Workout Plan (with Sets & Reps):**" + Environment.NewLine);

        for (int i = 0; i < input.DaysPerWeek; i++)
        {
            string dayName = $"Day {i + 1}";
            var exercises = GenerateWorkoutDay(i, input.Goal, input.BodyType, input.Weight, input.FitnessLevel,input.Gender);
            sb.AppendLine($"{dayName}:");
            string workoutText = string.Empty;
            foreach (var ex in exercises)
            {
                sb.AppendLine($"- {ex.Name}: {ex.Sets} sets x {ex.Reps} reps" + Environment.NewLine);
                //workoutdays[i] = $"- {ex.Name}: {ex.Sets} sets x {ex.Reps} reps";
                workoutText = workoutText + $"- {ex.Name}: {ex.Sets} sets x {ex.Reps} reps;";
            }
            //remove the last ; because when creating the html it will add new empty line
            //to avoid that remove the last ; (thats mean last index)
            workoutdays[i] = workoutText.Substring(0, workoutText.Length - 1);
            workoutText = "";

            sb.AppendLine();
            sb.AppendLine("---" + Environment.NewLine);
        }

        sb.AppendLine("🥗 **Daily Meal Plan (5 Meals):**" + Environment.NewLine);

        var meals = input.DietPreference?.ToLower() == "veg"
        ? GetVegMeals(input.Weight, input.Goal, input.Height)
        : GetNonVegMeals(input.Weight, input.Goal, input.Height);


        for (int i = 0; i < meals.Length; i++)
        {
            sb.AppendLine($"Meal {i + 1}: {meals[i]}" + Environment.NewLine);
            meal[i] = $"Meal {i + 1}: {meals[i]}";
        }

        //return sb.ToString();
        return GenerateFormattedFitnessPlanHtml(input, workoutdays, meal);
    }

private string GenerateFormattedFitnessPlanHtml(UserInputData input, string[] workoutDays, string[] meals)
{
    var sb = new StringBuilder();

    sb.AppendLine("<div class='fitness-plan'>");

    // Header
    sb.AppendLine("<h2>✅ Your Personalized Fitness Plan</h2>");
    sb.AppendLine("<p><strong>🎯 Goal:</strong> " + input.Goal + "</p>");
    sb.AppendLine("<p><strong>🏋️‍♂️ Workout Days:</strong> " + input.DaysPerWeek + " / week</p>");
    sb.AppendLine("<p><strong>📊 Body Type:</strong> " + input.BodyType + "</p>");
    sb.AppendLine("<p><strong>⚖️ Weight:</strong> " + input.Weight + " kg, <strong>📏 Height:</strong> " + input.Height + " cm</p>");
    sb.AppendLine("<p><strong>🍽️ Diet:</strong> " + input.DietPreference + "</p>");
    
    sb.AppendLine("<hr />");

    // Workout Section
    sb.AppendLine("<h3>📅 Workout Plan (with Sets & Reps)</h3>");
    for (int i = 0; i < workoutDays.Length; i++)
    {
        sb.AppendLine($"<h4>Day {i + 1}</h4><ul>");
        foreach (var exercise in workoutDays[i].Split(';'))
        {
            sb.AppendLine($"<li>{exercise}</li>");
        }
        sb.AppendLine("</ul>");
    }

    sb.AppendLine("<hr />");

    // Meal Section
    sb.AppendLine("<h3>🥗 Daily Meal Plan (5 Meals)</h3>");
    for (int i = 0; i < meals.Length; i++)
    {
        sb.AppendLine($"<p><strong>Meal {i + 1}:</strong> {meals[i]}</p>");
    }
        //give tips based on the bmi range
    sb.AppendLine("<h3>📊 BMI Tips</h3>");
    
    if (bmiValue < 18.5)
    {
        sb.AppendLine("<p>📉 <strong>Your BMI indicates that you may be underweight.</strong> Here are some supportive tips to help you gain weight healthily:</p>");
        sb.AppendLine("<ul>");
        sb.AppendLine("<li>✅ Focus on nutrient-dense meals like nuts, whole grains, and healthy fats.</li>");
        sb.AppendLine("<li>✅ Eat more frequent meals and snacks (every 3–4 hours).</li>");
        sb.AppendLine("<li>✅ Try smoothies with fruits, milk, and protein powder.</li>");
        sb.AppendLine("<li>✅ Add strength training to build lean muscle mass.</li>");
        sb.AppendLine("<li>✅ Consider speaking with a dietitian if weight gain is difficult.</li>");
        sb.AppendLine("</ul>");
    }
    else if (bmiValue >= 18.5 && bmiValue <= 24.9)
    {
        sb.AppendLine("<p>⚖️ <strong>Your BMI is within the healthy range.</strong> Great job! Here are tips to maintain it:</p>");
        sb.AppendLine("<ul>");
        sb.AppendLine("<li>✅ Keep a balanced diet with complex carbs, proteins, and fats.</li>");
        sb.AppendLine("<li>✅ Aim for at least 150 minutes of moderate activity per week.</li>");
        sb.AppendLine("<li>✅ Monitor your energy levels and adjust nutrition if needed.</li>");
        sb.AppendLine("<li>✅ Stay consistent — sustainability is key.</li>");
        sb.AppendLine("</ul>");
    }
    else if (bmiValue >= 25 && bmiValue <= 29.9)
    {
        sb.AppendLine("<p>📈 <strong>Your BMI indicates you may be slightly overweight.</strong> No worries — here’s how you can take small steps:</p>");
        sb.AppendLine("<ul>");
        sb.AppendLine("<li>✅ Aim for a modest calorie deficit (avoid extreme dieting).</li>");
        sb.AppendLine("<li>✅ Choose whole, unprocessed foods as often as possible.</li>");
        sb.AppendLine("<li>✅ Walk more daily — even short walks help.</li>");
        sb.AppendLine("<li>✅ Plan meals ahead to avoid overeating.</li>");
        sb.AppendLine("<li>✅ Stay focused on long-term change, not quick fixes.</li>");
        sb.AppendLine("</ul>");
    }
    else if (bmiValue >= 30)
    {
        sb.AppendLine("<p>🚨 <strong>Your BMI suggests you may be in the obese range.</strong> Let’s work toward progress step by step:</p>");
        sb.AppendLine("<ul>");
        sb.AppendLine("<li>✅ Start with low-impact movement like walking or swimming.</li>");
        sb.AppendLine("<li>✅ Reduce sugary snacks and beverages.</li>");
        sb.AppendLine("<li>✅ Break long sitting periods with light activity.</li>");
        sb.AppendLine("<li>✅ Surround yourself with positive support (friends or coaches).</li>");
        sb.AppendLine("<li>✅ Celebrate small wins — improved mood, energy, or movement counts!</li>");
        sb.AppendLine("</ul>");
    }


    sb.AppendLine("</div>");

    return sb.ToString();
}


    private List<WorkoutExercise> GenerateWorkoutDay(int dayIndex, string goal, string bodyType, double weight,string fitnessLevel,string gender)
    {
        var workouts = new Dictionary<string, List<WorkoutExercise>>
        {
            ["Chest & Triceps"] = new List<WorkoutExercise>
            {
                new("Bench Press", 4, goal == "Muscle Gain" ? 10 : 15),
                new("Incline Dumbbell Press", 3, goal == "Muscle Gain" ? 10 : 12),
                new("Tricep Pushdown", 3, 12)
            },
                ["Back & Biceps"] = new List<WorkoutExercise>
            {
                new("Pull-ups", 4, 8),
                new("Bent Over Row", 3, 10),
                new("Bicep Curls", 3, 12)
            },
                ["Shoulders"] = new List<WorkoutExercise>
            {
                new("Overhead Press", 4, 10),
                new("Lateral Raises", 3, 12),
                new("Shrugs", 3, 15)
            },
                ["Leg Day"] = new List<WorkoutExercise>
            {
                new("Squats", 4, goal == "Fat Loss" ? 15 : 10),
                new("Lunges", 3, 12),
                new("Leg Curls", 3, 12)
            },
                ["Core & Cardio"] = new List<WorkoutExercise>
            {
                new("Plank", 3, 60), // seconds
                new("Russian Twists", 3, 20),
                new("HIIT Sprints", 5, 30) // seconds
            },
                ["Full Body"] = new List<WorkoutExercise>
            {
                new("Burpees", 4, 15),
                new("Deadlifts", 4, 8),
                new("Push-ups", 4, 20)
            }
        };

        string[] workoutDays = { "Chest & Triceps", "Back & Biceps", "Shoulders", "Leg Day", "Core & Cardio", "Full Body" };

        string selectedDay = dayIndex < workoutDays.Length ? workoutDays[dayIndex] : "Full Body";

        // Optional: adjust reps for ectomorph, endomorph, mesomorph and fitness level
        if (bodyType == "Ectomorph" && goal == "Muscle Gain")
        {
            workouts[selectedDay].ForEach(w => w.Reps += 2);
            switch (fitnessLevel.ToLower())
            {
                case "intermediate":
                    workouts[selectedDay].ForEach(w => w.Reps += 2);
                    break;
                case "expert":
                    workouts[selectedDay].ForEach(w => w.Reps += 4);
                    break;
            }
        }
        else if (bodyType == "Endomorph" && goal == "Fat Loss")
        {
            workouts[selectedDay].ForEach(w => w.Reps += 3);
            switch (fitnessLevel.ToLower())
            {
                case "intermediate":
                    workouts[selectedDay].ForEach(w => w.Reps += 3);
                    break;
                case "expert":
                    workouts[selectedDay].ForEach(w => w.Reps += 5);
                    break;
            }
        }
        //based on gender increase the sets for males
        if (gender.ToLower().Equals("male"))
        {
            workouts[selectedDay].ForEach(w =>
            {
                if (w.Sets < 4)
                {
                    w.Sets += 1;
                }
            });
        }

        return workouts[selectedDay];
    }

    private string[] GetNonVegMeals(double weight, string goal,double height)
    {
        int proteinAmount = (int)(weight * (goal == "Muscle Gain" ? 2.0 : goal == "Fat Loss" ? 1.6 : 1.8));
        int riceAmount = goal == "Fat Loss" ? 150 : goal == "Muscle Gain" ? 250 : 200;
        int chickenAmount = goal == "Fat Loss" ? 120 : goal == "Muscle Gain" ? 180 : 150;
        int fishAmount = goal == "Fat Loss" ? 120 : 150;

        //calculate bmi and change the amounts
        double bmi = weight / (height * height);
        bmiValue = bmi;
        //bmi ranges
        //healthier - 18.5 to 24.9
        //Overweight	25.0 to 29.9
        //Obesity	30.0 or greater
        //Class 1 Obesity	30.0 to 34.9
        if (bmi < 18.5)
        {
            //increase the portion bit
            riceAmount = riceAmount + 20;
            chickenAmount = chickenAmount + 20;
            fishAmount = fishAmount + 20;
            proteinAmount = proteinAmount + 10;
        }
        else if (bmi > 24.9 && bmi < 30.0)
        {
            //reduce the portion slightly
            riceAmount = riceAmount - 20;
            chickenAmount = chickenAmount - 20;
            fishAmount = fishAmount - 20;
            proteinAmount = proteinAmount - 10;
        }
        else if (bmi > 29.9)
        {
            //reduce the portion effectively
            riceAmount = riceAmount - 30;
            chickenAmount = chickenAmount - 30;
            fishAmount = fishAmount - 30;
            proteinAmount = proteinAmount - 20;
        }

            return new string[]
            {
                    $"Breakfast: 4 eggs, 2 slices whole grain bread, 200ml {(goal == "Fat Loss" ? "low-fat" : "full cream")} milk",
                    $"Snack: Protein shake ({proteinAmount}g/day target), 1 banana",
                    $"Lunch: {riceAmount}g rice, {chickenAmount}g chicken, mixed vegetables",
                    $"Snack: 1 handful almonds, yogurt (low-fat for Fat Loss)",
                    $"Dinner: {fishAmount}g grilled fish, quinoa, steamed broccoli"
            };
    }

    private string[] GetVegMeals(double weight, string goal,double height)
    {
        int proteinAmount = (int)(weight * (goal == "Muscle Gain" ? 2.0 : goal == "Fat Loss" ? 1.6 : 1.8));
        int riceAmount = goal == "Fat Loss" ? 150 : goal == "Muscle Gain" ? 250 : 200;
        int paneerAmount = goal == "Fat Loss" ? 120 : goal == "Muscle Gain" ? 180 : 150;
        int tofuAmount = goal == "Fat Loss" ? 120 : 150;

         //calculate bmi and change the amounts
        double bmi = weight / (height * height);
        //bmi ranges
        //healthier - 18.5 to 24.9
        //Overweight	25.0 to 29.9
        //Obesity	30.0 or greater
        //Class 1 Obesity	30.0 to 34.9
        if (bmi < 18.5)
        {
            //increase the portion bit
            riceAmount = riceAmount + 20;
            paneerAmount = paneerAmount + 20;
            tofuAmount = tofuAmount + 20;
            proteinAmount = proteinAmount + 10;
        }
        else if (bmi > 24.9 && bmi < 30.0)
        {
            //reduce the portion slightly
            riceAmount = riceAmount - 20;
            paneerAmount = paneerAmount - 20;
            tofuAmount = tofuAmount - 20;
            proteinAmount = proteinAmount - 10;
        }
        else if (bmi > 29.9)
        {
            //reduce the portion effectively
            riceAmount = riceAmount - 30;
            paneerAmount = paneerAmount - 30;
            tofuAmount = tofuAmount - 30;
            proteinAmount = proteinAmount - 20;
        }

        return new string[]
        {
            $"Breakfast: Oats with 200ml {(goal == "Fat Loss" ? "unsweetened almond" : "soy")} milk, 1 apple",
            $"Snack: Peanut butter sandwich, {(goal == "Fat Loss" ? "boiled chickpeas" : "1 boiled egg or soy protein bar")}",
            $"Lunch: {riceAmount}g rice, {paneerAmount}g paneer, mixed vegetables",
            $"Snack: Greek yogurt, 1 banana (or apple for Fat Loss)",
            $"Dinner: 2 chapatis, {tofuAmount}g tofu, salad"
        };
    }


    public class WorkoutExercise
    {
        public string Name { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }

        public WorkoutExercise(string name, int sets, int reps)
        {
            Name = name;
            Sets = sets;
            Reps = reps;
        }
    }

}
