using Microsoft.EntityFrameworkCore;
using PunchCardApp.Data;
using PunchCardApp.Entities;
using PunchCardApp.Models;

namespace PunchCardApp.Repositories;

public class CourseRepository(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;

    public async Task CreateCourseAsync(CourseEntity course)
    {
        _context.Courses.Add(course);
        await _context.SaveChangesAsync();
    }

    public async Task<CourseEntity?> GetCourseByIdAsync(int id)
    {
        return await _context.Courses
            .Include(c => c.Registrations)
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}