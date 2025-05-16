using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;
using System.IO;

public static class ReportManager
{
    private static ExtentReports _extent;
    private static ExtentHtmlReporter _htmlReporter;
    private static ExtentTest _test;

    public static void InitReport()
    {
        string reportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports", "TestReport.html");

        Directory.CreateDirectory(Path.GetDirectoryName(reportPath)); // Ensure Reports folder exists

        _htmlReporter = new ExtentHtmlReporter(reportPath);
        _extent = new ExtentReports();
        _extent.AttachReporter(_htmlReporter);

        _extent.AddSystemInfo("Host Name", Environment.MachineName);
        _extent.AddSystemInfo("Environment", "QA");
        _extent.AddSystemInfo("User Name", Environment.UserName);
    }

    public static void CreateTest(string testName)
    {
        _test = _extent.CreateTest(testName);
    }

    public static void LogInfo(string message)
    {
        _test?.Info(message);
    }

    public static void LogPass(string message)
    {
        _test?.Pass(message);
    }

    public static void LogFail(string message)
    {
        _test?.Fail(message);
    }

    public static void FlushReport()
    {
        _extent?.Flush();
    }

    public static void LogFailWithScreenshot(string message, string screenshotPath)
    {
        _test?.Fail(message, MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
    }
}
