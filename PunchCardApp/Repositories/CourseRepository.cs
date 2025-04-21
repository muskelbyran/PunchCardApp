using Microsoft.Azure.Amqp.Framing;
using Microsoft.EntityFrameworkCore;
using PunchCardApp.Data;
using PunchCardApp.Entities;
using PunchCardApp.Models;
using static PunchCardApp.Components.Admin.Pages.Maintainance;

namespace PunchCardApp.Repositories;

public class CourseRepository(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;

    public async Task CreateCourseAsync(CourseEntity course)
    {
        _context.Courses.Add(course);
        await _context.SaveChangesAsync();
    }

    public async Task<List<CourseEntity>> GetAllCoursesAsync()
    {
        return await _context.Courses.ToListAsync();
    }


    public async Task<CourseEntity?> GetCourseByIdAsync(int id)
    {
        return await _context.Courses
            .Include(c => c.Registrations)
            .FirstOrDefaultAsync(c => c.Id == id);
    }



    public async Task DeleteCourseAsync(int courseId)
    {
        var course = await _context.Courses.FindAsync(courseId);

        if (course == null)
        {
            throw new InvalidOperationException("Course not found.");
        }

        _context.Courses.Remove(course);
        await _context.SaveChangesAsync();
    }


}