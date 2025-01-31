namespace PunchCardApp.Models;

public class PunchCardHistoryModel
{
    public string PunchCardType { get; set; } = null!;
    public DateTime PurchasedDate { get; set; }
    public int TotalUses { get; set; }
    public int UsesLeft { get; set; }
    public List<PunchCardUseHistoryModel> UsageHistory { get; set; } = new();
}

