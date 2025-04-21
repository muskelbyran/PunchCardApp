using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PunchCardApp.Models;
public class CreateCourseModel
{
    [Required]
    public string Title { get; set; } = "";

    public string? Tagline { get; set; }

    [Required]
    public string Category { get; set; } = "";

    [Required]
    [Range(0, 100000)]
    [Precision(18, 2)]
    public decimal Price { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    public IFormFile? ImageFile { get; set; }

    [Required]
    public string Description { get; set; } = "";

    public string? Times { get; set; }

    public string? Location { get; set; } = "";

    public string? ImageUrl { get; set; }
}