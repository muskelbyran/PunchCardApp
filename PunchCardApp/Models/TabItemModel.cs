namespace PunchCardApp.Models;

using Microsoft.AspNetCore.Components;

public class TabItemModel<TItem>
{
    public string Title { get; set; } = null!;
    public RenderFragment Content { get; set; } = null!;
}