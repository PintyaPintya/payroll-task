namespace PayrollTask.Models.Email;

public class EmailSettings
{
    public required string SmtpServer { get; set; }
    public required int SmtpPort { get; set; }
    public required string SenderEmail { get; set; }
    public required string SenderPassword { get; set; }
    public bool UseSsl { get; set; }
}