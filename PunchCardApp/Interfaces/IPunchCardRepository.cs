using PunchCardApp.Entities;

public interface IPunchCardRepository
{
    Task AddPunchCardAsync(PunchCardEntity punchCard);
    Task DeletePunchCardAsync(int punchCardId);
    Task<PunchCardEntity?> GetPunchCardByIdAsync(int punchCardId);
    Task<List<PunchCardEntity>> GetPunchCardsByUserProfileIdAsync(string userProfileId);
    Task<string> GetUserFullNameByIdAsync(Guid userId);
    Task UpdatePunchCardAsync(PunchCardEntity punchCard);
}