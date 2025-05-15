using Microsoft.Extensions.Configuration;
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
            .AddJsonFile("appsettings.json")
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
}