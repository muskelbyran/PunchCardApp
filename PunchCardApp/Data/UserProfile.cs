﻿namespace PunchCardApp.Data;

public class UserProfile
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? ProfilePicture { get; set; } = "avatar.jpg";
    public string? Biography { get; set; }
}