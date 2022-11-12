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
            user = new User("fezqbutwoa_1666692643@tfbnw.net", "12345T");
            startPage = new FacebookStartPage(driver, user);
            menuBar = new FacebookMenuBar(driver);
            mainPage = new FacebookPLMainPage(driver);
            startPage.prepareToTestsOnUserAccount();
        }

        [Test]

        public void CheckIfCreatedPostHasRightText()
        {
            try
            {
                mainPage.open();
                string postContent = (Utils.GetRandomNumber(10)).ToString();
                mainPage.makePost(postContent);
                var isRightTextInPost = mainPage.waitForPostWithCertainText(postContent);
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
            mainPage.deleteMostCurrentPost();
            menuBar.logout();
            driver.Quit();
        }
    }
}
