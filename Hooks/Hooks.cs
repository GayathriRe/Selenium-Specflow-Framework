using AventStack.ExtentReports;
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

        public TestHooks(IObjectContainer container, ScenarioContext scenarioContext)
        {
            _container = container;
            _scenarioContext = scenarioContext;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            ReportManager.InitReport();
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            DriverManager.InitDriver("chrome"); // or "firefox", "edge"
            ReportManager.CreateTest(_scenarioContext.ScenarioInfo.Title);
            ReportManager.LogInfo("Scenario started");
        }

        [AfterStep]
        public void AfterEachStep()
        {
            var driver = DriverManager.GetDriver();
            var stepText = _scenarioContext.StepContext.StepInfo.Text;
            if (_scenarioContext.TestError != null)
            {
                try
                {
                    string screenshotsDir = Path.Combine(Directory.GetCurrentDirectory(), "Screenshots");
                    Directory.CreateDirectory(screenshotsDir);

                    var fileName = $"{_scenarioContext.ScenarioInfo.Title}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
                    var filePath = Path.Combine(screenshotsDir, fileName);
                    var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                    screenshot.SaveAsFile(filePath);
                    string relativePath = Path.Combine("Screenshots", fileName);

                    // Log with screenshot
                    ReportManager.LogFailWithScreenshot($"Step Failed: {stepText}", relativePath);


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
            if (ScenarioContext.Current.TestError != null)
            {
                ReportManager.LogFail(ScenarioContext.Current.TestError.Message);
            }
            else
            {
                ReportManager.LogPass("Scenario passed");
            }
            DriverManager.QuitDriver(); // or _driver.Quit();
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            ReportManager.FlushReport();
            EmailHelper.SendReportByEmail();
        }
    }
}
