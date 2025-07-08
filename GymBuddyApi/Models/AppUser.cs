using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace GymBuddyApi.Models;

public class AppUser : IdentityUser
{
    [Required]
    [StringLength(15, MinimumLength = 3, ErrorMessage = "Username should be 3 to 15 characters long.")]
    public override string UserName { get; set; } = string.Empty;
    // public byte[] PasswordHash { get; set; }
    // public byte[] PasswordSalt { get; set; }
}