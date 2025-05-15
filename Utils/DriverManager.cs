using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using System;

namespace SeleniumSpecFlowFramework.Utils
{
    public static class DriverManager
    {
        [ThreadStatic] private static IWebDriver _driver;

        public static void InitDriver(string browser = "chrome")
        {
            switch (browser.ToLower())
            {
                case "chrome":
                    var chromeOptions = new ChromeOptions();
                    //chromeOptions.AddArgument("--headless"); // Uncomment for headless mode
                    _driver = new ChromeDriver(chromeOptions);
                    break;
                case "firefox":
                    var firefoxOptions = new FirefoxOptions();
                    //firefoxOptions.AddArgument("--headless"); // Uncomment for headless mode
                    _driver = new FirefoxDriver(firefoxOptions);
                    break;
                case "edge":
                    var edgeOptions = new EdgeOptions();
                    //edgeOptions.AddArgument("--headless"); // Uncomment for headless mode
                    _driver = new EdgeDriver(edgeOptions);
                    break;
                default:
                    throw new ArgumentException("Unsupported browser: " + browser);
            }

            _driver.Manage().Window.Maximize();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        public static IWebDriver GetDriver()
        {
            if (_driver == null)
                throw new NullReferenceException("WebDriver is not initialized. Call InitDriver() first.");
            return _driver;
        }

        public static void QuitDriver()
        {
            _driver?.Quit();
            _driver = null;
        }
    }
}
