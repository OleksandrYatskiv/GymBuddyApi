namespace GymBuddyApi.Services.EmailService;

public interface IEmailService
{
    Task SendEmailAsync(string email, string subject, string message);
}
