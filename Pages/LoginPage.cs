using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium_Specflow_Framework.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }
        private IWebElement Username => _driver.FindElement(By.Id("user-name")); // change as needed
        private IWebElement Password => _driver.FindElement(By.Id("password")); // change as needed
        private IWebElement LoginButton => _driver.FindElement(By.Id("login-button")); // change as needed
        private IWebElement HomePage => _driver.FindElement(By.XPath("//span[@class='title' and text()='Products']")); // change as needed

        public void login(string username, string password)
        {
            Username.SendKeys(username);
            Password.SendKeys(password);
            LoginButton.Click();
        }


       
        public bool IsLoggedIn()
        {
            bool isLoggedIn = false;
            try
            {
                isLoggedIn = HomePage.Displayed;
            }
            catch (NoSuchElementException)
            {
                isLoggedIn = false;
            }
            return isLoggedIn;
        }
    }
}