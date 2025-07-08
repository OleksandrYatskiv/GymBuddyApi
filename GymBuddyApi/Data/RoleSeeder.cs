using GymBuddyApi.Constants;
using Microsoft.AspNetCore.Identity;

namespace GymBuddyApi.Data;

public class RoleSeeder
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ILogger<RoleSeeder> _logger;

    public RoleSeeder(RoleManager<IdentityRole> roleManager, ILogger<RoleSeeder> logger)
    {
        _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task SeedRolesAsync()
    {
        try
        {
            _logger.LogInformation("Starting role seeding...");

            var roles = new[] { Roles.Admin, Roles.User };

            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    _logger.LogInformation("Creating role: {Role}", role);
                    var result = await _roleManager.CreateAsync(new IdentityRole(role));
                    
                    if (!result.Succeeded)
                    {
                        var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                        _logger.LogError("Failed to create role {Role}. Errors: {Errors}", role, errors);
                        throw new ApplicationException($"Failed to create role {role}");
                    }
                }
            }

            _logger.LogInformation("Role seeding completed successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding roles");
            throw;
        }
    }
}