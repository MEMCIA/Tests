using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    class FacebookSearchPage
    {
        public FacebookSearchPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public string FindWordInResultHeader()
        {
            var locatorHeader = By.CssSelector("h1[tabindex='-1']");
            var header =  Utils.GetElement(locatorHeader, this._driver);
            return header.FindElement(By.CssSelector("span")).Text;
        }

        private IWebDriver _driver;
    }
}
