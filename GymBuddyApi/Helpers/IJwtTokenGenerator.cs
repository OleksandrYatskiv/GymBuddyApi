using GymBuddyApi.Models;

namespace GymBuddyApi.Helpers;

public interface IJwtTokenGenerator
{
    Task<string> CreateTokenAsync(AppUser user);
}