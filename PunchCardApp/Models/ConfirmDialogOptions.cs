using PunchCardApp.Enums;

namespace PunchCardApp.Models;

public class ConfirmDialogOptions
{
    public string YesButtonText { get; set; } = "Yes";
    public ButtonColor YesButtonColor { get; set; } = ButtonColor.Primary;
    public string NoButtonText { get; set; } = "No";
    public ButtonColor NoButtonColor { get; set; } = ButtonColor.Secondary;
}