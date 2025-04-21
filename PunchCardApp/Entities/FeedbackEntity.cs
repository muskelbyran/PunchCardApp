namespace PunchCardApp.Entities;

public class FeedbackEntity
{
    public int Id { get; set; }
    public string FromName { get; set; } = null!;
    public string? Message { get; set; }
    public string FeedbackType { get; set; } = null!;
    public DateTime SubmittedAt { get; set; }
}
