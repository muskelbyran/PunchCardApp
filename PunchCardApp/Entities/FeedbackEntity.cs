namespace PunchCardApp.Entities;

public class FeedbackEntity : IFeedbackEntity
{
    public int Id { get; set; }
    public string FromName { get; set; } = null!;
    public string Message { get; set; } = null!;
    public bool IsTypo { get; set; }
    public bool IsBug { get; set; }
    public bool IsIdea { get; set; }
    public bool IsPraise { get; set; }
    public DateTime SubmittedAt { get; set; }
}
