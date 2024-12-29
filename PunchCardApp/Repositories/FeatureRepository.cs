using Microsoft.EntityFrameworkCore;
using PunchCardApp.Data;
using PunchCardApp.Entities;
using PunchCardApp.Factories;
using PunchCardApp.Models;

namespace PunchCardApp.Repositories;

public class FeatureRepository(ApplicationDbContext context) : BaseRepository<FeatureEntity>(context)
{
    private readonly ApplicationDbContext _context = context;

    public override async Task<ResponseResult> GetAllAsync()
    {
        try
        {
            IEnumerable<FeatureEntity> result = await _context.Features
                .Include(i => i.FeatureItems)
                .ToListAsync();

            return ResponseFactory.Ok(result);
        }

        catch (Exception ex)
        {
            return ResponseFactory.Error(ex.Message);
        }
    }
}