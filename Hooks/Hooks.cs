using BoDi;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumSpecFlowFramework.Utils;
using System;
using System.IO;
using TechTalk.SpecFlow;

namespace SeleniumSpecFlowFramework.Hooks
{
    [Binding]
    public class TestHooks
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly IObjectContainer _container;


        public TestHooks(IObjectContainer container)
        {


            _container = container;
            _scenarioContext = container.Resolve<ScenarioContext>();

        }



        [BeforeScenario]
        public void BeforeScenario()
        {
            DriverManager.InitDriver("chrome"); // or "firefox", "edge"

        }

        [AfterStep]
        public void AfterEachStep()
        {
            if (_scenarioContext.TestError != null)
            {
                try
                {
                    string screenshotsDir = Path.Combine(Directory.GetCurrentDirectory(), "Screenshots");
                    Directory.CreateDirectory(screenshotsDir);
                    var driver = DriverManager.GetDriver();
                    var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                    var fileName = $"{_scenarioContext.ScenarioInfo.Title}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
                    var filePath = Path.Combine(screenshotsDir, fileName);

                    screenshot.SaveAsFile(filePath);

                    Console.WriteLine($"[Screenshot] Saved to: {filePath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Screenshot Error] {ex.Message}");
                }
            }


        }

        [AfterScenario]
        public void AfterScenario()
        {
            DriverManager.QuitDriver(); // or _driver.Quit();
        }


    }
}
