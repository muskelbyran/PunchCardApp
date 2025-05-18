namespace PunchCardApp.Models;

public class ToastMessage : IEquatable<ToastMessage>
{
    public ToastMessage()
    {
        Id = Guid.NewGuid();
    }

    public bool Equals(ToastMessage? other) => other != null && Id.Equals(other?.Id);

    public bool AutoHide { get; set; }

    internal Guid Id { get; }
}