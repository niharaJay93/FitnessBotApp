namespace FitnessBotApp.Models
{
    public class MealPlan
    {
        public int Id { get; set; }
        public string MealDetails { get; set; } = null!;
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
