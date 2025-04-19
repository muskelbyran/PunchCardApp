using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components.Server.Circuits;
using System.Collections.Concurrent;
using Microsoft.AspNetCore.Components;
using PunchCardApp.Components;
using PunchCardApp.Components.Account;
using PunchCardApp.Data;
using PunchCardApp.Repositories;
using PunchCardApp.Services;
using PunchCardApp;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
})
    .AddIdentityCookies();

var secretsFilePath = builder.Environment.IsDevelopment()
        ? "secrets.Development.json"
        : Path.Combine(builder.Environment.ContentRootPath, "../secrets.Production.json");

if (File.Exists(secretsFilePath))
{
    builder.Configuration.AddJsonFile(secretsFilePath, optional: false, reloadOnChange: true);
}
else
{
    Console.WriteLine($"Secrets file not found at: {secretsFilePath}");
}

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? builder.Configuration["DatabaseConnectionString"] // Fallback to secrets
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
var serviceBusConnectionString = builder.Configuration["AzureServiceBus:ConnectionString"]
    ?? throw new InvalidOperationException("Azure Service Bus connection string not found.");
var serviceBusQueueName = builder.Configuration["AzureServiceBus:QueueName"]
    ?? throw new InvalidOperationException("Azure Service Bus queue name not found.");

// For Identity / background jobs 
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// For Blazor Server components
builder.Services.AddDbContextFactory<ApplicationDbContext>(
    options => options.UseSqlServer(connectionString),
    ServiceLifetime.Scoped);

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.User.RequireUniqueEmail = true;
    options.Password.RequiredLength = 8;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(x =>
{
    x.LoginPath = "/signin";
    x.AccessDeniedPath = "/Error";
    x.Cookie.HttpOnly = true;
    x.Cookie.SecurePolicy = CookieSecurePolicy.Always; 
    x.ExpireTimeSpan = TimeSpan.FromMinutes(5);
    x.SlidingExpiration = true;
});

builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("SuperAdmins", policy => policy.RequireRole("SuperAdmin"));
    x.AddPolicy("Admins", policy => policy.RequireRole("SuperAdmin", "Admin"));
    x.AddPolicy("Managers", policy => policy.RequireRole("Admin", "SuperAdmin", "Manager"));
    x.AddPolicy("AuthenticatedUsers", policy => policy.RequireRole("Admin", "SuperAdmin", "Manager", "User"));
});

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

// Repositories
builder.Services.AddScoped<FeatureRepository>();
builder.Services.AddScoped<FeatureItemRepository>();
builder.Services.AddScoped<FeedbackRepository>();
builder.Services.AddScoped<PunchCardRepository>();
builder.Services.AddScoped<CourseRepository>();

// Services
builder.Services.AddScoped<FeatureService>();
builder.Services.AddScoped<RoleService>();
builder.Services.AddScoped<FeedbackService>();
builder.Services.AddScoped<PunchCardService>();
builder.Services.AddScoped<CourseService>();

// Logged on users
builder.Services.AddSingleton<ICircuitUserService, CircuitUserService>();
builder.Services.AddScoped<CircuitHandler>((sp) =>
    new CircuitHandlerService(sp.GetRequiredService<ICircuitUserService>()));

// User engagement
builder.Services.AddScoped<IUserAnalyticsService, UserAnalyticsService>();
builder.Services.AddScoped<IUserEngagementService, UserEngagementService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    string[] roles = { "SuperAdmin", "Admin", "Manager", "User" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(PunchCardApp.Client._Imports).Assembly);

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();