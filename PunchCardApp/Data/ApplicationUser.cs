using Microsoft.AspNetCore.Identity;

namespace PunchCardApp.Data;

public class ApplicationUser : IdentityUser
{
    public string? UserProfileId { get; set; }
    public UserProfile? UserProfile { get; set; }
    public string? UserAddressId { get; set; }
    public UserAddress? UserAddress { get; set; }
    public bool CreatedByAdmin { get; set; } = false;
    public bool MustChangePassword { get; set; } = false;
}