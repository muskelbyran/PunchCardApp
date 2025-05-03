using System.ComponentModel.DataAnnotations;

namespace PunchCardApp.Models;

public class FeedbackModel
{
    public string FromName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Du måste fylla i feedback meddelande.")]
    public string Message { get; set; } = "";

    //[Required(ErrorMessage = "Du måste välja typ av feedback.")]
    public string FeedbackType { get; set; } = "";

    public DateTime SubmittedAt { get; set; }
}
