using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FollowersInsta.PageObjects
{
    public class FollowersPage : BasePage
    {
        public FollowersPage() { }
        public FollowersPage(IWebDriver driver)
        {
            PageFactory.InitElements(driver, this);
        }

        #region XPathes
        private readonly string listOfUsersToFollowXPath = "//div[@role='presentation']//button[text()='Подписаться']/parent::div";
        private readonly string listOfFollowingXPath = "//div[@role='presentation']//button[text()='Подписки']/parent::div";
        private string listOfUsernamesToFollowXPath => $"{listOfUsersToFollowXPath}/preceding-sibling::div//a[contains(@class, 'notranslate')]";
        private string listOfFollowingUsernamesXPath => $"{listOfFollowingXPath}/preceding-sibling::div//a[contains(@class, 'notranslate')]";

        #endregion

        #region Elements
        [FindsBy(How = How.XPath, Using = "//div[@role='presentation']//button[text()='Подписаться']/ancestor::ul/..")]
        private IWebElement wndFollowers { get; set; }
        #endregion

        #region Methods
        public string GetWindowClassName()
        {
            Wait(1);
            return wndFollowers.GetAttribute("class");
        }

        public void ScrollDown(IWebDriver driver)
        {
            string followersWindowClassName = GetWindowClassName();
            int indexToScroll = 500;
            for (int i = 0; i < 20; i++)
            {
                ScrollTo(driver, followersWindowClassName, indexToScroll);
                indexToScroll += 500;
                Wait(1);
            }
        }

        public string FollowPeople(IWebDriver driver)
        {
            string loggerText = string.Empty;
            var listOfUsersToFollow = driver.FindElements(By.XPath(listOfUsersToFollowXPath));
            var listOfUsernamesToFollow = driver.FindElements(By.XPath(listOfUsernamesToFollowXPath));

            for (int i = 1; i <= 10; i++) // Больше 1000 в день = бан 
            {
                listOfUsersToFollow[i].Click();
                Wait(1);
                loggerText += $"\n{DateTime.Now} You followed user {i} - {listOfUsernamesToFollow[i].Text}";
                if (i % 50 == 0)
                {
                    loggerText += "Pause for 5 minutes";
                    Wait(300);
                }
            }
            driver.Close();
            return loggerText;
        }

        public string UnfollowPeople(IWebDriver driver)
        {
            string loggerText = string.Empty;
            var listOfFollowing = driver.FindElements(By.XPath(listOfFollowingXPath));
            var listOfFollowingUsernames = driver.FindElements(By.XPath(listOfFollowingUsernamesXPath));

            for (int i = 0; i < 10 ; i++) //listOfFollowing.Count
            {
                listOfFollowing[i].Click();
                Wait(2);
                try
                {
                    driver.FindElement(By.XPath("//button[text()='Отменить подписку']")).Click();
                }
                catch (Exception e)
                {
                    loggerText += e.Message;
                    continue;
                }
                Wait(2);
                loggerText += $"\n{DateTime.Now} You unfollowed user {i} - {listOfFollowingUsernames[i].Text}";
            }
            driver.Close();
            return loggerText;
        }

        public string ScrollFollowersWindow(IWebDriver driver)
        {
            string loggerText = string.Empty;
            loggerText += $"{DateTime.Now} Started scrolling followers list \n";
            try
            {
                ScrollDown(driver);
            }
            catch (Exception ex)
            {
                loggerText += ex.Message;
            }

            return loggerText;
        }

        static void ScrollTo(IWebDriver driver, string className, int indexToScroll)
        {
            var js = String.Format($"document.getElementsByClassName('{className}').item(0).scroll(0, {indexToScroll}) ");
            ((IJavaScriptExecutor)driver).ExecuteScript(js);
        }
        #endregion
    }
}
