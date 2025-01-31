using Microsoft.EntityFrameworkCore;
using PunchCardApp.Data;
using PunchCardApp.Entities;

public class PunchCardRepository(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;

    public async Task<List<PunchCardEntity>> GetPunchCardsByUserProfileIdAsync(string userProfileId)
    {
        //return await _context.PunchCards
        //    .Where(pc => pc.UserProfileId == userProfileId)
        //    .Include(pc => pc.PunchCardUses)
        //    .ToListAsync();

        var punchCards = await _context.PunchCards
                               .Where(pc => pc.UserProfileId == userProfileId)
                               .Include(pc => pc.PunchCardUses)
                               .ToListAsync();

        Console.WriteLine($"Fetched {punchCards.Count} punch cards for UserProfileId: {userProfileId}");

        foreach (var pc in punchCards)
        {
             Console.WriteLine($"PunchCard {pc.Id}: Type={pc.Type}, TotalUses={pc.TotalUses}, UsesLeft={pc.UsesLeft}");
             Console.WriteLine($"Usage count: {pc.PunchCardUses.Count}");
        }

        return punchCards;
    }
   
    public async Task<PunchCardEntity?> GetPunchCardByIdAsync(int punchCardId)
    {
        return await _context.PunchCards
            .Include(pc => pc.PunchCardUses)
            .FirstOrDefaultAsync(pc => pc.Id == punchCardId);
    }

    public async Task AddPunchCardAsync(PunchCardEntity punchCard)
    {
        _context.PunchCards.Add(punchCard);
        await _context.SaveChangesAsync();
    }

    public async Task UpdatePunchCardAsync(PunchCardEntity punchCard)
    {
        _context.PunchCards.Update(punchCard);
        await _context.SaveChangesAsync();
    }

    public async Task DeletePunchCardAsync(int punchCardId)
    {
        var punchCard = await _context.PunchCards.FindAsync(punchCardId);
        if (punchCard != null)
        {
            _context.PunchCards.Remove(punchCard);
            await _context.SaveChangesAsync();
        }
    }
}
