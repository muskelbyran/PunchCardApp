using PunchCardApp.Entities;

public class PunchCardService(PunchCardRepository repository)
{
    private readonly PunchCardRepository _repository = repository;

    public async Task<List<PunchCardEntity>> GetPunchCardsForUserAsync(string userProfileId)
    {
        return await _repository.GetPunchCardsByUserProfileIdAsync(userProfileId);
    }

    public async Task UsePunchCardAsync(int punchCardId)
    {
        var punchCard = await _repository.GetPunchCardByIdAsync(punchCardId);

        if (punchCard == null || punchCard.UsesLeft <= 0)
        {
            throw new InvalidOperationException("Punch card is invalid or has no uses left.");
        }

        punchCard.UsesLeft--;
        punchCard.PunchCardUses.Add(new PunchCardUseEntity
        {
            PunchCardId = punchCard.Id,
            UsedDate = DateTime.UtcNow
        });

        await _repository.UpdatePunchCardAsync(punchCard);
    }

    public async Task CreatePunchCardAsync(string userProfileId, string type, int totalUses)
    {
        var punchCard = new PunchCardEntity
        {
            UserProfileId = userProfileId,
            Type = type,
            TotalUses = totalUses,
            UsesLeft = totalUses,
            PurchasedDate = DateTime.UtcNow,
            PunchCardUses = new List<PunchCardUseEntity>()
        };

        await _repository.AddPunchCardAsync(punchCard);
    }

    public async Task DeletePunchCardAsync(int punchCardId)
    {
        await _repository.DeletePunchCardAsync(punchCardId);
    }
}