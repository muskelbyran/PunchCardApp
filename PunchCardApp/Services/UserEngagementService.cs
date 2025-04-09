using Microsoft.EntityFrameworkCore;
using PunchCardApp.Data;
using PunchCardApp.Models;

namespace PunchCardApp.Services;

public class UserEngagementService : IUserEngagementService
{
    private readonly ApplicationDbContext _context;

    public UserEngagementService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task LogLoginAsync(string userId)
    {
        var isFirstLogin = !await _context.UserEngagementLogs
            .AnyAsync(l => l.UserId == userId && l.Type == EngagementType.Login);

        _context.UserEngagementLogs.Add(new UserEngagementLog
        {
            UserId = userId,
            Timestamp = DateTime.UtcNow,
            Type = EngagementType.Login,
            IsFirstLogin = isFirstLogin
        });

        await _context.SaveChangesAsync();
    }

    public async Task LogPageViewAsync(string userId, string pageName)
    {
        _context.UserEngagementLogs.Add(new UserEngagementLog
        {
            UserId = userId,
            Timestamp = DateTime.UtcNow,
            Type = EngagementType.PageView,
            Metadata = pageName
        });

        await _context.SaveChangesAsync();
    }

    public async Task LogEventAsync(string userId, EngagementType type, string? metadata = null)
    {
        _context.UserEngagementLogs.Add(new UserEngagementLog
        {
            UserId = userId,
            Timestamp = DateTime.UtcNow,
            Type = type,
            Metadata = metadata
        });

        await _context.SaveChangesAsync();
    }
}