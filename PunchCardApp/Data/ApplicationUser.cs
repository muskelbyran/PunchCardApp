using Microsoft.AspNetCore.Identity;
using PunchCardApp.Entities;

namespace PunchCardApp.Data;

public class ApplicationUser : IdentityUser
{
    public string? UserProfileId { get; set; }
    public UserProfile? UserProfile { get; set; }
    public string? UserAddressId { get; set; }
    public UserAddress? UserAddress { get; set; }
    public bool CreatedByAdmin { get; set; } = false;
    public bool MustChangePassword { get; set; } = false;
    public ICollection<CourseRegistrationEntity> CourseRegistrations { get; set; } = new List<CourseRegistrationEntity>();
}