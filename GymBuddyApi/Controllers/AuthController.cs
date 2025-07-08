using GymBuddyApi.Dtos;
using GymBuddyApi.Helpers;
using GymBuddyApi.Models;
using GymBuddyApi.Services.EmailService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GymBuddyApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IEmailService _emailService;

    public AuthController(
        UserManager<AppUser> userManager,
        IJwtTokenGenerator jwtTokenGenerator,
        IEmailService emailService)
    {
        _userManager = userManager;
        _jwtTokenGenerator = jwtTokenGenerator;
        _emailService = emailService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserDto request)
    {
        var user = new AppUser
        {
            UserName = request.Email,
            Email = request.Email,
            EmailConfirmed = false
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        // Generate email confirmation token
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var confirmationLink = Url.Action("ConfirmEmail", "Auth",
            new { userId = user.Id, token = token }, Request.Scheme);

        await _emailService.SendEmailAsync(
            user.Email,
            EmailText.subject,
            EmailText.GetEmailText(confirmationLink!)
        );

        return Ok("Registration successful. Please confirm your email.");
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(UserDto userDto)
    {
        var user = await _userManager.FindByEmailAsync(userDto.Email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, userDto.Password))
        {
            if (user != null) // failed attempts count for existing users
                await _userManager.AccessFailedAsync(user);

            return Unauthorized("Invalid credentials");
        }

        if (!user.EmailConfirmed)
            return Forbid("Please verify your email before logging in.");

        // Reset failed access attempts on successful login
        await _userManager.ResetAccessFailedCountAsync(user);

        var token = await _jwtTokenGenerator.CreateTokenAsync(user);

        return Ok(new
        {
            token,
            email = user.Email,
            emailConfirmed = user.EmailConfirmed
        });
    }

    [HttpPost("resend-confirmation")]
    public async Task<IActionResult> ResendConfirmation(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null || user.EmailConfirmed)
            return BadRequest("Invalid request");

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var confirmationLink = Url.Action("ConfirmEmail", "Auth",
            new { userId = user.Id, token = token }, Request.Scheme);

        await _emailService.SendEmailAsync(
            user.Email,
            EmailText.subject,
            EmailText.GetEmailText(confirmationLink!)
        );

        return Ok("Confirmation email sent");
    }

    [HttpGet("confirm-email")]
    public async Task<IActionResult> ConfirmEmail(string userId, string token)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return BadRequest("Invalid user");

        var result = await _userManager.ConfirmEmailAsync(user, token);
        if (!result.Succeeded)
            return BadRequest("Invalid token");

        return Ok("Email confirmed successfully");
        // return Redirect("email-confirmed"); // redirect to email-confirmed page after
    }
}