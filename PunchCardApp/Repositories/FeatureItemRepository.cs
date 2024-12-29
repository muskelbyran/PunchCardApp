using PunchCardApp.Data;
using PunchCardApp.Entities;

namespace PunchCardApp.Repositories;

public class FeatureItemRepository(ApplicationDbContext context) : BaseRepository<FeatureItemEntity>(context)
{
    private readonly ApplicationDbContext _context = context;
}
