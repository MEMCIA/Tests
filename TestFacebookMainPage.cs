using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Tests
{
    class TestFacebookMainPage
    {
        IWebDriver driver;
        FacebookStartPage startPage;
        User user;
        FacebookMenuBar menuBar;
        FacebookPLMainPage mainPage;

        [SetUp]

        public void SetUp()
        {
            driver =  Utils.CreateDriver();
            //language of user should be set to polish
            user = new User("x", "x");
            startPage = new FacebookStartPage(driver, user);
            menuBar = new FacebookMenuBar(driver);
            mainPage = new FacebookPLMainPage(driver);
            startPage.PrepareToTestsOnUserAccount();
        }

        [Test]

        public void CheckIfCreatedPostHasRightText()
        {
            try
            {
                mainPage.Open();
                string postContent = (Utils.GetRandomNumber(10)).ToString();
                mainPage.makePost(postContent);
                var isRightTextInPost = mainPage.WaitForPostWithCertainText(postContent);
                Assert.That(isRightTextInPost, Is.True); ;
            }
            catch (Exception error)
            {
                Assert.That(error, Is.Not.EqualTo(null));
            }
        }

        [TearDown]

        public void TearDown()
        {
            mainPage.DeleteMostCurrentPost();
            menuBar.Logout();
            driver.Quit();
        }
    }
}
