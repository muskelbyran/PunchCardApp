using PunchCardApp.Models;

namespace PunchCardApp.Services
{
    public interface IUserEngagementService
    {
        Task LogEventAsync(string userId, EngagementType type, string? metadata = null);
        Task LogLoginAsync(string userId);
        Task LogPageViewAsync(string userId, string pageName);
    }
}