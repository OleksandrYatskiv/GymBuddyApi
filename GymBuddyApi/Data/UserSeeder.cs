using GymBuddyApi.Constants;
using GymBuddyApi.Models;
using Microsoft.AspNetCore.Identity;

namespace GymBuddyApi.Data;

public class UserSeeder
{
    private readonly UserManager<AppUser> _userManager;

    public UserSeeder(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }
    
    private async Task CreateUserWithRole(string email, string password, string role)
    {
        if (await _userManager.FindByEmailAsync(email) == null)
        {
            var user = new AppUser { Email = email, EmailConfirmed = true, UserName = email };
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, role);
            }
            else
            {
                throw new Exception(
                    $"Failed to create new user with email {user.Email}. Errors: {string.Join(", ", result.Errors)}");
            }
        }
    }

    public async Task SeedUsers()
    {
        await CreateUserWithRole("admin@gmail.com", "Admin123!", Roles.Admin);
        await CreateUserWithRole("user@gmail.com", "User123!", Roles.User);
    }
}