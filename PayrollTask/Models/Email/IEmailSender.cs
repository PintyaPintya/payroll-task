namespace PayrollTask.Models.Email;

public interface IEmailSenderService
{
    Task SendEmail(string toEmail, string subject, string body);
}