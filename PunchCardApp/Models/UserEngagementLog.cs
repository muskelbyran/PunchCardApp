namespace PunchCardApp.Models;

public class UserEngagementLog
{
    public int Id { get; set; }
    public string UserId { get; set; } = default!;
    public DateTime Timestamp { get; set; }
    public EngagementType Type { get; set; }
    public string? Metadata { get; set; }
    public bool IsFirstLogin { get; set; }
}

public enum EngagementType
{
    Login,
    PageView,
    Click,
    Other
}