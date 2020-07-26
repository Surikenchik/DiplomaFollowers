using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FollowersInsta.PageObjects
{
    public class LoginPage : BasePage
    {
        public LoginPage() { }
        public LoginPage(IWebDriver driver) 
        {
            PageFactory.InitElements(driver, this);
        }

        #region Elements
        [FindsBy(How = How.Name, Using = "username")]
        private IWebElement txtUsername { get; set; }

        [FindsBy(How = How.Name, Using = "password")]
        private IWebElement txtPassword { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[text()='Войти']")]
        private IWebElement btnLogin { get; set; }
        #endregion

        #region Helper Methods
        private void SetUsername(string usernameToSet)
        {
            txtUsername.SendKeys(usernameToSet);
        }

        private void SetPassword(string passwordToSet)
        {
            txtPassword.SendKeys(passwordToSet);
        }

        private void ClickLoginButton(IWebDriver driver)
        {
            try
            {
                btnLogin.Click();
            }
            catch(Exception ex)
            {
                driver.Dispose();
                Window1 window = new Window1();
                window.ShowDialog();
            }
        }
        #endregion

        #region Methods
        public void PerformLogin(string username, string password, IWebDriver driver)
        {
            Wait(2);
            SetUsername(username);
            SetPassword(password);
            ClickLoginButton(driver);
            Wait(2);
        }
        #endregion
    }
}
