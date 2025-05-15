using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Selenium_Specflow_Framework.Pages;
using SeleniumSpecFlowFramework.Hooks;
using SeleniumSpecFlowFramework.Utils;
using TechTalk.SpecFlow;





namespace Selenium_Specflow_Framework.StepDefinitions
{
    [Binding]
    public class LoginStepDef
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly IWebDriver _driver;
        private readonly LoginPage _loginPage;

        public LoginStepDef(ScenarioContext scenarioContext)
        {
            _driver = DriverManager.GetDriver();
            _scenarioContext = scenarioContext;
            _loginPage = new LoginPage(_driver);
        }

        [StepDefinition("you have launched the application")]
        public void GivenYouHaveLaunchedTheApplication()
        {
            _driver.Navigate().GoToUrl("https://www.saucedemo.com/"); // change to your application URL
        }

        [StepDefinition("you login to the application")]
        public void WhenYouLoginToTheApplication(Table table)
        {
            _loginPage.login(table.Rows[0]["username"], table.Rows[0]["password"]);
        }

        [StepDefinition("the home page opens successfully")]
        public void ThenTheHomePageOpensSuccessfully()
        {
            Assert.IsTrue(_loginPage.IsLoggedIn(), "Home page is not displayed after login.");

        }
    }


}
