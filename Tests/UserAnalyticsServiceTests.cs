using System;
using System.Threading.Tasks;
using System.Linq;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Moq;
using PunchCardApp.Data;
using PunchCardApp.Models;
using PunchCardApp.Services;

public class UserAnalyticsServiceTests
{
    [Fact]
    public async Task GetDailyActiveUsersAsync_ReturnsCorrectCount()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // unik för varje test
        .Options;

        var context = new ApplicationDbContext(options);

        var today = DateTime.UtcNow.Date;

        context.UserEngagementLogs.AddRange(
        new UserEngagementLog { UserId = "user1", Type = EngagementType.Login, Timestamp = today },
        new UserEngagementLog { UserId = "user2", Type = EngagementType.Login, Timestamp = today },
        new UserEngagementLog { UserId = "user1", Type = EngagementType.Login, Timestamp = today } // Kopia
        );

        await context.SaveChangesAsync();

        var mockFactory = new Mock<IDbContextFactory<ApplicationDbContext>>();
        mockFactory.Setup(f => f.CreateDbContextAsync(It.IsAny<CancellationToken>()))
        .ReturnsAsync(context);

        var service = new UserAnalyticsService(mockFactory.Object);

        // Act
        var result = await service.GetDailyActiveUsersAsync();

        // Assert
        Assert.Equal(2, result); // user1, user2
    }

    [Fact]
    public async Task GetDailyActiveUsersAsync_NoLoginsToday_ReturnsZero()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        .Options;

        var context = new ApplicationDbContext(options);

        context.UserEngagementLogs.Add(new UserEngagementLog
        {
            UserId = "user1",
            Type = EngagementType.Login,
            Timestamp = DateTime.UtcNow.Date.AddDays(-1),
            IsFirstLogin = true
        });

        await context.SaveChangesAsync();

        var mockFactory = new Mock<IDbContextFactory<ApplicationDbContext>>();
        mockFactory.Setup(f => f.CreateDbContextAsync(It.IsAny<CancellationToken>()))
        .ReturnsAsync(context);

        var service = new UserAnalyticsService(mockFactory.Object);

        // Act
        var result = await service.GetDailyActiveUsersAsync();

        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public async Task GetDailyActiveUsersAsync_LoginsYesterdayOnly_ReturnsZero()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        .Options;

        var context = new ApplicationDbContext(options);

        context.UserEngagementLogs.AddRange(new[]
        {
            new UserEngagementLog
            {
                UserId = "user1",
                Type = EngagementType.Login,
                Timestamp = DateTime.UtcNow.Date.AddDays(-1),
                IsFirstLogin = false
            },
            new UserEngagementLog
            {
            UserId = "user2",
            Type = EngagementType.Login,
            Timestamp = DateTime.UtcNow.Date.AddDays(-2),
            IsFirstLogin = false
            }
        });

        await context.SaveChangesAsync();

        var mockFactory = new Mock<IDbContextFactory<ApplicationDbContext>>();
        mockFactory.Setup(f => f.CreateDbContextAsync(It.IsAny<CancellationToken>()))
        .ReturnsAsync(context);

        var service = new UserAnalyticsService(mockFactory.Object);

        // Act
        var result = await service.GetDailyActiveUsersAsync();

        // Assert
        Assert.Equal(0, result);
    }
}