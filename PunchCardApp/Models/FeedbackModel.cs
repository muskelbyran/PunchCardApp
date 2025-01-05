namespace PunchCardApp.Models;

public class FeedbackModel
{
    public string FromName { get; set; } = string.Empty;
    public string Message { get; set; } = "";
    public bool IsTypo { get; set; }
    public bool IsBug { get; set; }
    public bool IsIdea { get; set; }
    public bool IsPraise { get; set; }
    public DateTime SubmittedAt { get; set; } // Add this property
}
