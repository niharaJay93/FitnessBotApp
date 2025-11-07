using FitnessBotApp.Models;

public class WorkoutPlan
{
    public int Id { get; set; }
    public string Goal { get; set; } = null!;
    public string BodyType { get; set; } = null!;
    public string PlanDetails { get; set; } = null!;  // Could be JSON string or structured data
    public int UserId { get; set; }
    public User User { get; set; } = null!;
}
