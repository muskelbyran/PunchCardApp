using Microsoft.EntityFrameworkCore;
using PunchCardApp.Data;
using PunchCardApp.Models;

namespace PunchCardApp.Services;

public class UserEngagementService(IDbContextFactory<ApplicationDbContext> dbContextFactory) : IUserEngagementService
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory = dbContextFactory;

    public async Task LogLoginAsync(string userId)
    {
        using var context = _dbContextFactory.CreateDbContext();

        var isFirstLogin = !await context.UserEngagementLogs
        .AnyAsync(l => l.UserId == userId && l.Type == EngagementType.Login);

        context.UserEngagementLogs.Add(new UserEngagementLog
        {
            UserId = userId,
            Timestamp = DateTime.UtcNow,
            Type = EngagementType.Login,
            IsFirstLogin = isFirstLogin
        });

        await context.SaveChangesAsync();
    }

    public async Task LogPageViewAsync(string userId, string pageName)
    {
        using var context = _dbContextFactory.CreateDbContext();

        context.UserEngagementLogs.Add(new UserEngagementLog
        {
            UserId = userId,
            Timestamp = DateTime.UtcNow,
            Type = EngagementType.PageView,
            Metadata = pageName
        });

        await context.SaveChangesAsync();
    }

    public async Task LogEventAsync(string userId, EngagementType type, string? metadata = null)
    {
        using var context = _dbContextFactory.CreateDbContext();

        context.UserEngagementLogs.Add(new UserEngagementLog
        {
            UserId = userId,
            Timestamp = DateTime.UtcNow,
            Type = type,
            Metadata = metadata
        });

        await context.SaveChangesAsync();
    }
}

