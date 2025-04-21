namespace PunchCardApp.Models;

public class FeedbackModel
{
    public string FromName { get; set; } = string.Empty;
    public string Message { get; set; } = "";
    public string FeedbackType { get; set; } = null!;
    public DateTime SubmittedAt { get; set; } // Add this property
}
