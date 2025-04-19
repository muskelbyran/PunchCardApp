using Microsoft.EntityFrameworkCore;
using PunchCardApp.Data;
using System.ComponentModel.DataAnnotations;

namespace PunchCardApp.Entities;

public class CourseEntity
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string? Tagline { get; set; }
    public string Category { get; set; } = "";
    [Precision(18, 2)]
    public decimal Price { get; set; }
    public DateTime StartDate { get; set; }
    public string? ImageUrl { get; set; }
    public string Description { get; set; } = "";
    public string? Times { get; set; }
    public string? Location { get; set; }

    public ICollection<CourseRegistrationEntity> Registrations { get; set; } = new List<CourseRegistrationEntity>();
}

/// <summary>
/// Representerar en användarens registrering till en specifik kurs.
/// Refererar till så väl användare som kurs och metadata.
/// Attended kan vara confirmed, cancelled eller annan status.
/// </summary>
public class CourseRegistrationEntity
{
    public int Id { get; set; }

    // Relationer
    public string UserId { get; set; } = "";
    public ApplicationUser User { get; set; } = null!;

    public int CourseId { get; set; }
    public CourseEntity Course { get; set; } = null!;

    public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
    public bool Attended { get; set; } = false;
}