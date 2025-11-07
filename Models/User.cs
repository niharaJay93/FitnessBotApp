namespace FitnessBotApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Goal { get; set; } // e.g., "Gain Muscle", "Lose Fat"
        public int WorkoutDaysPerWeek { get; set; }
        public string PasswordHash { get; set; } // Hashed password
    }
}
