using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using PunchCardApp.Services;

namespace PunchCardApp.Components.Account.Sections
{
	public partial class Contact : ComponentBase, IDisposable
	{
		public string MyCircuitMessage = "";
		public string MyUserId = "";

		CircuitHandlerService handler;

		protected override async Task OnInitializedAsync()
		{
			var handler = (CircuitHandlerService)BlazorCircuitHandler;

			MyCircuitMessage = $"My Circuit ID = {handler.CircuitId}";

			if (string.IsNullOrEmpty(handler.CircuitId))
			{
				// Log error if the CircuitId is null or empty
				Console.WriteLine("Error: Circuit ID is null or empty!");
				return; // Exit early or handle the error as needed
			}

			var authState = await AuthStateProvider.GetAuthenticationStateAsync();
			var user = authState.User;

			if (user.Identity?.IsAuthenticated ?? false)
			{
				var userId = user.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

				if (!string.IsNullOrEmpty(userId))
				{
					await using var dbContext = await DbFactory.CreateDbContextAsync();
					var appUser = await dbContext.Users
						.Include(u => u.UserProfile)
						.FirstOrDefaultAsync(u => u.Id == userId);

					MyUserId = appUser?.UserProfile is not null
						? $"{appUser.UserProfile.FirstName} {appUser.UserProfile.LastName}"
						: appUser?.Email ?? "Unknown";
				}
			}
			else
			{
				MyUserId = "Guest";
			}

			// Proceed only if CircuitId is valid
			if (!string.IsNullOrEmpty(MyUserId))
			{
				UserService.Connect(handler.CircuitId, MyUserId);
				UserService.CircuitsChanged += UserService_CircuitsChanged;
			}
		}




		private void UserService_CircuitsChanged(object? sender, EventArgs e)
		{
			InvokeAsync(StateHasChanged);
		}

		public void Dispose()
		{
			UserService.CircuitsChanged -= UserService_CircuitsChanged;
		}
	}
}
