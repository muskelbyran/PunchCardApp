namespace PunchCardApp.Models;

public class PunchCardUseHistoryModel
{
    public DateTime UsedDate { get; set; }
    public string UsedBy { get; set; } = null!;
}

