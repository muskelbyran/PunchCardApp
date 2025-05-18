using Microsoft.EntityFrameworkCore;
using PunchCardApp.Data;
using PunchCardApp.Models;

namespace PunchCardApp.Services;

public class UserAnalyticsService(IDbContextFactory<ApplicationDbContext> contextFactory) : IUserAnalyticsService
{
    private readonly IDbContextFactory<ApplicationDbContext> _contextFactory = contextFactory;

    public async Task<int> GetDailyActiveUsersAsync()
    {
        await using var context = await _contextFactory.CreateDbContextAsync();

        var today = DateTime.UtcNow.Date;
        return await context.UserEngagementLogs
        .AsNoTracking()
        .Where(l => l.Type == EngagementType.Login && l.Timestamp.Date == today)
        .Select(l => l.UserId)
        .Distinct()
        .CountAsync();
    }

    public async Task<int> GetMonthlyActiveUsersAsync()
    {
        await using var context = await _contextFactory.CreateDbContextAsync();

        var startOfMonth = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
        var endOfMonth = startOfMonth.AddMonths(1);

        var activeUsers = await context.UserEngagementLogs
        .AsNoTracking()
        .Where(log => log.Timestamp >= startOfMonth && log.Timestamp < endOfMonth)
        .Select(log => log.UserId)
        .Distinct()
        .ToListAsync();

        return activeUsers.Count;
    }

    //public async Task<int> GetNewUsersThisMonthAsync()
    //{
    // await using var context = await _contextFactory.CreateDbContextAsync();

    // var startDate = DateTime.UtcNow.Date.AddDays(-30);
    // return await context.UserEngagementLogs
    // .AsNoTracking()
    // .Where(l => l.Type == EngagementType.Login && l.Timestamp >= startDate && l.IsFirstLogin)
    // .Select(l => l.UserId)
    // .Distinct()
    // .CountAsync();
    //}

    public async Task<int> GetNewUsersThisMonthAsync()
    {
        await using var context = await _contextFactory.CreateDbContextAsync();

        var startOfMonth = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
        var startOfNextMonth = startOfMonth.AddMonths(1);

        return await context.UserEngagementLogs
        .AsNoTracking()
        .Where(l => l.Type == EngagementType.Login
        && l.IsFirstLogin
        && l.Timestamp >= startOfMonth
        && l.Timestamp < startOfNextMonth)
        .Select(l => l.UserId)
        .Distinct()
        .CountAsync();
    }

    public async Task<List<string>> GetChurnedUsersAsync()
    {
        await using var context = await _contextFactory.CreateDbContextAsync();

        var lastMonth = DateTime.UtcNow.Date.AddMonths(-1);
        var thisMonth = DateTime.UtcNow.Date.AddDays(-30);

        var lastMonthUsers = await context.UserEngagementLogs
        .AsNoTracking()
        .Where(l => l.Type == EngagementType.Login && l.Timestamp >= lastMonth && l.Timestamp < thisMonth)
        .Select(l => l.UserId)
        .Distinct()
        .ToListAsync();

        var thisMonthUsers = await context.UserEngagementLogs
        .AsNoTracking()
        .Where(l => l.Type == EngagementType.Login && l.Timestamp >= thisMonth)
        .Select(l => l.UserId)
        .Distinct()
        .ToListAsync();

        return lastMonthUsers.Except(thisMonthUsers).ToList();
    }
}

