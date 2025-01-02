
namespace PunchCardApp.Entities
{
    public interface IFeedbackEntity
    {
        string FromName { get; set; }
        int Id { get; set; }
        bool IsBug { get; set; }
        bool IsIdea { get; set; }
        bool IsPraise { get; set; }
        bool IsTypo { get; set; }
        string Message { get; set; }
        DateTime SubmittedAt { get; set; }
    }
}