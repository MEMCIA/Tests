﻿using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests
{
    static class Utils
    {
        public static TimeSpan defaultTimeOut { get; private set; }  = new TimeSpan(0, 0, 30);

        public static IWebElement GetElement(By locator, IWebDriver driver)
        {
            var element = new WebDriverWait(driver, defaultTimeOut)
                        .Until(drv => FindDisplayedElement(locator, drv));
            return element;
        }

        static IWebElement FindDisplayedElement(By locator, IWebDriver driver)
        {
            var elements = driver.FindElements(locator);
            return elements.FirstOrDefault(element => element.Displayed);
        }

        public static IWebElement ClickElementWithLocator(By locator, IWebDriver driver, bool afterWaitForElementToDisappear)
        {
            var element = GetElement(locator, driver);
            element.Click();
            if (!afterWaitForElementToDisappear) return element;
            WaitForElementToDisappear(locator, driver);
            return element;
        }

        public static void EnterTextInElementWithLocator(By locator, IWebDriver driver, string text, bool withReturnKey, bool afterWaitForElementToDisappear)
        {
            var element = GetElement(locator, driver);
            element.SendKeys(text);
            if (withReturnKey) element.SendKeys(Keys.Return);
            if (!afterWaitForElementToDisappear) return;
            WaitForElementToDisappear(locator, driver);
        }

        public static void WaitForElementToDisappear(By locator, IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, defaultTimeOut);
            wait.Until(driver => (CheckIfElementDisappeared(locator, driver)));
        }

        static bool CheckIfElementDisappeared(By locator, IWebDriver driver)
        {
            var elements = (driver.FindElements(locator));
            return elements.Count == 0;
        }

        public static void WaitUntilUrlIsTheSame(string url, IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, defaultTimeOut);
            wait.Until(ExpectedConditions.UrlToBe(url));
        }

        public static bool CheckIfUrlIsTheSame(string url, IWebDriver driver)
        {
            return  driver.Url == url;
        }

        static bool CheckIfElementsExist(By locator, IWebDriver driver)
        {
            var elements =  driver.FindElements(locator);
            return elements.Count != 0;
        }

        static void WaitToFindElement(By locator, IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, defaultTimeOut);
            wait.Until(ExpectedConditions.ElementExists(locator));
        }

        public static string GetRandomText(int length)
        {
            var result = "";
            Random random = new Random();
            var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var charactersLength = characters.Length;
            for (var i = 0; i < length; i++)
            {
                result += characters[random.Next(charactersLength)];
            }
            return result;
        }

        public static int GetRandomNumber(int length)
        {
            var result = "";
            Random random = new Random();

            for (var i = 0; i < length; i++)
            {
                result += random.Next(10);
            }

            return Int32.Parse(result);
        }

        public static IWebDriver CreateDriver()
        {
            var options = new ChromeOptions();
            //disabled to prevent site from asking permission to show notifications 
            options.AddArguments("--disable-notifications");
            var driver = new ChromeDriver(options);
            return driver;
        }
    }
}
