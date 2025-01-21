using PunchCardApp.Data;

namespace PunchCardApp.Entities;

public class PunchCardEntity
{
    public int Id { get; set; }
    public string UserProfileId { get; set; } = null!;
    public UserProfile UserProfile { get; set; } = null!;
    public string Type { get; set; } = null!;
    public DateTime PurchasedDate { get; set; }
    public int TotalUses { get; set; }
    public int UsesLeft { get; set; }
    public ICollection<PunchCardUseEntity> PunchCardUses { get; set; } = null!;
    public byte[]? RowVersion { get; set; }  
}