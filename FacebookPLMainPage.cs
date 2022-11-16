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
        public FacebookPLMainPage(IWebDriver driver)
        {
            this._driver = driver;
        }

         public void Open()
        {
            _driver.Navigate().GoToUrl(FacebookPLMainPage.Url);
        }

        void ClickCreatePostWindow()
        {
            var locatorPostWindow = By.XPath("//span[contains(text(), 'O czym myślisz')]");
             Utils.ClickElementWithLocator(locatorPostWindow, this._driver, false);
        }

        void EnterTextInPost(string text)
        {
            var locatorPostWindowExpanded = By.CssSelector("div[aria-label*='O czym myślisz']");
            var element =  Utils.GetElement(locatorPostWindowExpanded, this._driver);
             element.SendKeys(text);
        }

         void ClickPublish()
        {
            var locatorPublishButton = By.XPath("//span[text()='Opublikuj']");
             Utils.ClickElementWithLocator(locatorPublishButton, this._driver, true);
        }

         public void makePost(string content)
        {
            ClickCreatePostWindow();
            EnterTextInPost(content);
            ClickPublish();
        }

         IWebElement FindMostCurrentPost()
        {
            var locatormostCurrentPost = By.CssSelector("div[data-pagelet='FeedUnit_0']");
            return  Utils.GetElement(locatormostCurrentPost, this._driver);
        }

         bool CheckIfPostHasCertainText(string text)
        {
            var mostCurrentPost = FindMostCurrentPost();
            var textInPostElement =  mostCurrentPost.Text;
            return textInPostElement.Contains(text);
        }

         public bool WaitForPostWithCertainText(string text)
        {
            var wait = new WebDriverWait(_driver, Utils.defaultTimeOut);
            wait.Until( (driver) => CheckIfPostHasCertainText(text));
            return true;
        }

         void OpenPostMenu(IWebElement post)
        {
            var locatorPostMenu = By.CssSelector("div[aria-label='Działania dla tego posta']");
            var postMenu = post.FindElement(locatorPostMenu);
            WebDriverWait wait = new WebDriverWait(_driver, Utils.defaultTimeOut);
            wait.Until(ExpectedConditions.ElementToBeClickable(postMenu));
            postMenu.Click();
        }

         public void DeleteMostCurrentPost()
        {
            var mostCurrentPost = FindMostCurrentPost();
            DeletePost(mostCurrentPost);
        }

         void DeletePost(IWebElement post)
        {
            OpenPostMenu(post);
            var locatorDevareButton = By.XPath("//span[text()='Przenieś do kosza']");
            Utils.ClickElementWithLocator(locatorDevareButton, this._driver, true);
            ConfirmDelete();
        }

         void ConfirmDelete()
        {
            var locatorConfirm = By.XPath("//span[text()='Przenieś']");
            Utils.ClickElementWithLocator(locatorConfirm, this._driver, true);
        }

        private IWebDriver _driver;
        public static readonly string Url = "https://www.facebook.com/";
    }
}
