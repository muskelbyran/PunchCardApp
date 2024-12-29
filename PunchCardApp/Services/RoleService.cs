﻿using Microsoft.AspNetCore.Components.Authorization;

namespace PunchCardApp.Services;

public class RoleService
{
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public RoleService(AuthenticationStateProvider authenticationStateProvider)
    {
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async Task<bool> IsUserAsync()
    {
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        return user.Identity != null && user.Identity.IsAuthenticated && user.IsInRole("User");
    }

    /// <summary>
    /// Async check if the current user is authenticated and has the "Manager" role.
    /// </summary>
    /// <returns>
    /// The task result returns a bool whether the current user is authenticated and 
    /// has the "Manager" role.
    /// </returns>
    public async Task<bool> IsManagerAsync()
    {
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        return user.Identity != null && user.Identity.IsAuthenticated && user.IsInRole("Manager");
    }

    public async Task<bool> IsAdminAsync()
    {
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        return user.Identity != null && user.Identity.IsAuthenticated && user.IsInRole("Admin");
    }

    public async Task<bool> IsSuperAdminAsync()
    {
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        return user.Identity != null && user.Identity.IsAuthenticated && user.IsInRole("SuperAdmin");
    }
}