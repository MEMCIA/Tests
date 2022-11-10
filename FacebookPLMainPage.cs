using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    class FacebookPLMainPage
    {
        FacebookPLMainPage(WebDriver driver)
        {
            this._driver = driver;
        }

         public void open()
        {
            _driver.Navigate().GoToUrl(FacebookPLMainPage.Url);
        }

        void clickCreatePostWindow()
        {
            var locatorPostWindow = By.XPath("//span[contains(text(), 'O czym myślisz')]");
             Utils.ClickElementWithLocator(locatorPostWindow, this._driver, false);
        }

        void enterTextInPost(string text)
        {
            var locatorPostWindowExpanded = By.CssSelector("div[aria-label*='O czym myślisz']");
            var element =  Utils.GetElement(locatorPostWindowExpanded, this._driver);
             element
                .FindElement(By.CssSelector("p"))
                .SendKeys(text);
        }

         void clickPublish()
        {
            var locatorPublishButton = By.XPath("//span[text()='Opublikuj']");
             Utils.ClickElementWithLocator(locatorPublishButton, this._driver, true);
        }

         public void makePost(string content)
        {
            clickCreatePostWindow();
            enterTextInPost(content);
            clickPublish();
        }

         IWebElement findMostCurrentPost()
        {
            var locatormostCurrentPost = By.CssSelector("div[data-pagevar='FeedUnit_0']");
            return  Utils.GetElement(locatormostCurrentPost, this._driver);
        }

         bool checkIfPostHasCertainText(string text)
        {
            var mostCurrentPost = findMostCurrentPost();
            var textInPostElement =  mostCurrentPost.Text;
            return textInPostElement.Contains(text);
        }

         bool waitForPostWithCertainText(string text)
        {
            var wait = new WebDriverWait(_driver, Utils.defaultTimeOut);
            wait.Until( (driver) => checkIfPostHasCertainText(text));
            return true;
        }

         void openPostMenu(IWebElement post)
        {
            var locatorPostMenu = By.CssSelector("div[aria-label='Działania dla tego posta']");
            Utils.ClickElementWithLocator(locatorPostMenu, _driver, false);
        }

         void deleteMostCurrentPost()
        {
            var mostCurrentPost = findMostCurrentPost();
            deletePost(mostCurrentPost);
        }

         void deletePost(IWebElement post)
        {
            openPostMenu(post);
            var locatorDevareButton = By.XPath("//span[text()='Przenieś do kosza']");
            Utils.ClickElementWithLocator(locatorDevareButton, this._driver, true);
            confirmDelete();
        }

         void confirmDelete()
        {
            var locatorConfirm = By.XPath("//span[text()='Przenieś']");
            Utils.ClickElementWithLocator(locatorConfirm, this._driver, true);
        }

        private WebDriver _driver;
        public static readonly string Url = "https://www.facebook.com/";
    }
}
