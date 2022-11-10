using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    class FacebookSearchPage
    {
        public FacebookSearchPage(WebDriver driver)
        {
            _driver = driver;
        }

        public string findWordInResultHeader()
        {
            var locatorHeader = By.CssSelector("h1[tabindex='-1']");
            var header =  Utils.GetElement(locatorHeader, this._driver);
            return header.FindElement(By.CssSelector("span")).Text;
        }

        private WebDriver _driver;
    }
}
