using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace Tests
{
    public class TestFacebookStartPage
    {
        private IWebDriver driver;
        private User user;
        private FacebookStartPage startPagePL;
        private FacebookMenuBar menuBar;
        By locatorButtonLogout = By.XPath("//span[contains(text(),'Wyloguj')]");

        [SetUp]
        public void Setup()
        {
            driver = Utils.CreateDriver();
            //language of user should be set to polish
            user = new User("x", "x");
            startPagePL = new FacebookStartPage(driver, user);
            menuBar = new FacebookMenuBar(driver);
            startPagePL.PrepareToTestsOnUserAccount();
        }

        [Test]
        public void CheckIfAfterLogInButtonLogOutIsAvaible()
        {

            menuBar.ClickAccountSymbol();
            try
            {
                var buttonLogOut = Utils.GetElement(locatorButtonLogout, driver);
                Assert.That(buttonLogOut, Is.Not.EqualTo(null));
            }
            catch (Exception error)
            {
                Assert.That(error, Is.EqualTo(null));
            }
        }

    [TearDown]
        public void TearDown()
        {
            Utils.ClickElementWithLocator(locatorButtonLogout, driver, false);
            driver.Quit();
        }

    }
}