using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public static class ConfigHelper
{
    private static IConfigurationRoot configuration;

    static ConfigHelper()
    {
        configuration = new ConfigurationBuilder()
            .SetBasePath(System.IO.Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json",optional: false, reloadOnChange: true)
            .Build();
    }
    

    public class Credential
    {
        public string Key { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public static Credential GetActiveCredential()
    {
        string activeKey = configuration["ActiveUser"];
        var allCreds = configuration.GetSection("Credentials").Get<List<Credential>>();
        return allCreds.FirstOrDefault(c => c.Key == activeKey);

    }
    public class EmailSettings
    {
        public string FromEmail { get; set; }
        public string ToEmail { get; set; }
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string Password { get; set; }
    }

    public static EmailSettings GetEmailSettings()
    {
        var settings = configuration.GetSection("EmailSettings").Get<EmailSettings>();
        settings.Password = Environment.GetEnvironmentVariable("EmailPassword");
        return settings;
    }
}