using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FollowersInsta.PageObjects
{
    public class AccountPage : BasePage
    {
        public AccountPage() { }
        public AccountPage(IWebDriver driver)
        {
            PageFactory.InitElements(driver, this);
        }

        #region Elements
        [FindsBy(How = How.XPath, Using = "//a[text()=' подписчиков']")]
        private IWebElement btnFollowers { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[text()='Подписаться']")]
        private IWebElement btnFollow { get; set; }

        [FindsBy(How=How.XPath, Using = "//a[text()=' подписок']")]
        private IWebElement btnFollowing { get; set; }
        #endregion

        #region Helper Methods
        private void clickFollowersButton()
        {
            btnFollowers.Click();
        }
        #endregion

        #region Methods
        public void ClickFollowersButton()
        {
            Wait(2);
            clickFollowersButton();
            Wait(2);
        }

        public void ClickFollowingButton()
        {
            Wait(2);
            btnFollowing.Click();
            Wait(2);
        }

        public void FollowAccount()
        {
            Wait(2);
            btnFollow.Click();
            Wait(2);
        }
        #endregion
    }
}
