
namespace PunchCardApp.Services;

public interface IUserAnalyticsService
{
    Task<List<string>> GetChurnedUsersAsync();
    Task<int> GetDailyActiveUsersAsync();
    Task<int> GetMonthlyActiveUsersAsync();
    Task<int> GetNewUsersThisMonthAsync();
}