using System;
using System.IO;
using System.Net;
using System.Net.Mail;

public static class EmailHelper
{
    public static void SendReportByEmail()
    {
        var emailSettings = ConfigHelper.GetEmailSettings();

        string reportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports", "TestReport.html");

        if (!File.Exists(reportPath))
            throw new FileNotFoundException("Test report not found", reportPath);

        MailMessage message = new MailMessage(emailSettings.FromEmail, emailSettings.ToEmail)
        {
            Subject = "Automation Test Report",
            Body = "Please find the attached test execution report."
        };

        message.Attachments.Add(new Attachment(reportPath));

        SmtpClient client = new SmtpClient(emailSettings.SmtpServer, emailSettings.Port)
        {
            Credentials = new NetworkCredential(emailSettings.FromEmail, emailSettings.Password),
            EnableSsl = true
        };

        client.Send(message);
    }
}
