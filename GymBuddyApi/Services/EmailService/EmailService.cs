using System.Net;
using System.Net.Mail;
using GymBuddyApi.Configuration;
using Microsoft.Extensions.Options;

namespace GymBuddyApi.Services.EmailService;

public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;

    public EmailService(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public async Task SendEmailAsync(string email, string subject, string message)
    {
        var mail = new MailMessage
        {
            From = new MailAddress(_emailSettings.SenderEmail, _emailSettings.SenderName),
            Subject = subject,
            Body = message,
            IsBodyHtml = true
        };
        mail.To.Add(email);

        using var smtp = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.SmtpPort);
        smtp.Credentials = new NetworkCredential(_emailSettings.SmtpUsername, _emailSettings.SmtpPassword);
        smtp.EnableSsl = true;

        await smtp.SendMailAsync(mail);

    }
}