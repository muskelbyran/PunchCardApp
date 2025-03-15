using Microsoft.EntityFrameworkCore;
using PunchCardApp.Entities;
using PunchCardApp.Models;

public class PunchCardService(PunchCardRepository repository)
{
    private readonly PunchCardRepository _repository = repository;

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

    public async Task<List<PunchCardHistoryModel>> GetPunchCardHistoryAsync(string userProfileId)
    {
        var punchCards = await _repository.GetPunchCardsByUserProfileIdAsync(userProfileId);

        return punchCards.Select(pc => new PunchCardHistoryModel
        {
            PunchCardType = pc.Type,
            PurchasedDate = pc.PurchasedDate,
            TotalUses = pc.TotalUses,
            UsesLeft = pc.UsesLeft,
            UsageHistory = pc.PunchCardUses
                .Select(use => new PunchCardUseHistoryModel
                {
                    UsedDate = use.UsedDate,
                    UsedBy = use.UsedBy 
                })
                .ToList()
        }).ToList();
    }

    public async Task UsePunchCardAsync(int punchCardId, string usedBy)
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
            UsedDate = DateTime.UtcNow,
            UsedBy = usedBy
        });

        await _repository.UpdatePunchCardAsync(punchCard);
    }  

    public async Task DeletePunchCardAsync(int punchCardId)
    {
        await _repository.DeletePunchCardAsync(punchCardId);
    }


}