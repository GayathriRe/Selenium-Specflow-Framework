using System;
using System.IO;
using System.Net;
using System.Net.Mail;



    public static class EmailHelper
    {
        public static void SendReportByEmail()
        {
            var emailSettings = ConfigHelper.GetEmailSettings();

            string reportPath = Path.Combine(Directory.GetCurrentDirectory(), "Reports", "TestReport.html");

            if (!File.Exists(reportPath))
                throw new FileNotFoundException("Test report not found", reportPath);

            // Securely fetch password from environment variable
            string securePassword = Environment.GetEnvironmentVariable("EmailPassword") ?? emailSettings.Password;

            MailMessage message = new MailMessage(emailSettings.FromEmail, emailSettings.ToEmail)
            {
                Subject = "Automation Test Report",
                Body = "Please find the attached test execution report."
            };

            message.Attachments.Add(new Attachment(reportPath));

            SmtpClient client = new SmtpClient(emailSettings.SmtpServer, emailSettings.Port)
            {
                Credentials = new NetworkCredential(emailSettings.FromEmail, securePassword),
                EnableSsl = true
            };

            client.Send(message);
        }
    }
