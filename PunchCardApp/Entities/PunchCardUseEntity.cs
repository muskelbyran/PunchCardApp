namespace PunchCardApp.Entities;

public class PunchCardUseEntity
{
    public int Id { get; set; }

    public int PunchCardId { get; set; }
    public PunchCardEntity PunchCard { get; set; } = null!;

    public DateTime UsedDate { get; set; }
}