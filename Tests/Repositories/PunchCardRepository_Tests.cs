using Microsoft.EntityFrameworkCore;
using PunchCardApp.Data;
using PunchCardApp.Entities;

namespace Tests.Repositories;

public class PunchCardRepository_Tests
{
    private readonly ApplicationDbContext _context;
    private readonly IPunchCardRepository _punchCardRepository;

    public PunchCardRepository_Tests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase($"{Guid.NewGuid}")
            .Options;
        _context = new ApplicationDbContext(options);
        _punchCardRepository = new PunchCardRepository(_context);
    }

    [Fact]
    public async Task AddPunchCardAsync_ShouldAddPunchCard()
    {
        // Arrange
        var punchCard = new PunchCardEntity
        {
            Type = "Test Card",
            TotalUses = 10,
            UsesLeft = 10,
            UserProfileId = "test-user-id"
        };
        // Act
        await _punchCardRepository.AddPunchCardAsync(punchCard);
        var result = await _context.PunchCards.FindAsync(punchCard.Id);
        // Assert
        Assert.NotNull(result);
        Assert.Equal(punchCard.Type, result.Type);
        Assert.Equal(punchCard.TotalUses, result.TotalUses);
        Assert.Equal(punchCard.UsesLeft, result.UsesLeft);
    }
}