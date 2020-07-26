using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FollowersInsta.PageObjects
{
    public class BasePage
    {
        IWebDriver driver;

        public BasePage() { }
        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public void Wait(int seconds)
        {
            Thread.Sleep(seconds * 1000);
        }

        public void NavigateByUrl(string urlToNavigate)
        {
            driver.Navigate().GoToUrl(urlToNavigate);
        }
    }
}
