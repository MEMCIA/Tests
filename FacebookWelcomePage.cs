using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    class FacebookWelcomePage
    {
        public FacebookWelcomePage(WebDriver driver)
        {
            _driver = driver;
        }

        public void Open()
        {
           _driver.Navigate().GoToUrl(FacebookWelcomePage.Url);
        }

        private WebDriver _driver;
        public static string Url = "https://www.facebook.com/?sk=welcome";
    }
}
