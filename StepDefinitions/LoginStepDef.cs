using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V125.WebAuthn;
using Selenium_Specflow_Framework.Pages;
using SeleniumSpecFlowFramework.Hooks;
using SeleniumSpecFlowFramework.Utils;
using System;
using System.Data;
using System.IO;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;





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

        [When(@"you login with ""(.*)"" and ""(.*)""")]
        public void WhenYouLoginWithAnd(string username, string password)
        {
            _loginPage.login(username, password);
        }

       

        [StepDefinition("the home page opens successfully")]
        public void ThenTheHomePageOpensSuccessfully()
        {
            Assert.IsTrue(_loginPage.IsLoggedIn(), "Home page is not displayed after login.");

        }

        [StepDefinition("I login using valid credentials from Excel")]
        public void WhenILoginUsingValidCredentialsFromExcel()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", "TestData.xlsx");
            var dataTable = ExcelReaderUtil.ReadExcel(filePath, "Login");

            foreach (DataRow row in dataTable.Rows)
            {
                string username = row["Username"].ToString();
                string password = row["Password"].ToString();

                Console.WriteLine($"Logging in with Username: {username}, Password: {password}");

                var loginPage = new LoginPage(_driver);
                _loginPage.login(username, password);

                
            }
           


        }
        [StepDefinition("you login with the credentials")]
        public void WhenYouLoginWithTheCredentials(Table table)
        {
            _loginPage.login(table.Rows[0]["username"], table.Rows[0]["password"]);
        }

    }


}
