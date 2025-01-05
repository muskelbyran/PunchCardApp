using PunchCardApp.Data;
using PunchCardApp.Entities;
using PunchCardApp.Models;

namespace PunchCardApp.Repositories;

public class FeedbackRepository : BaseRepository<FeedbackEntity>
{
    private readonly ApplicationDbContext _context;

    public FeedbackRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }   
}