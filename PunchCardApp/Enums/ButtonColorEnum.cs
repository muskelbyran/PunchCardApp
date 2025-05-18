namespace PunchCardApp.Enums;

public enum ButtonColor
{
    Primary,
    Secondary,
    Success,
    Danger
}

public static class ButtonColorExtensions
{
    public static string ToCssClass(this ButtonColor buttonColor)
    {
        return buttonColor switch
        {
            ButtonColor.Primary => "btn-primary",
            ButtonColor.Secondary => "btn-secondary",
            ButtonColor.Success => "btn-success",
            ButtonColor.Danger => "btn-danger",
            _ => "btn-primary"
        };
    }
}

