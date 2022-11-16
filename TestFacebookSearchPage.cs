using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Tests
{
    public class TestFacebookSearchPage
    {
        IWebDriver driver;
        FacebookStartPage startPage;
        User user;
        FacebookMenuBar menuBar ;
        FacebookSearchPage searchPage ;

        [SetUp]
        public void Setup()
        {
            driver = Utils.CreateDriver();
            //language of user should be set to polish
            user = new User("x", "x");
            startPage = new FacebookStartPage(driver, user);
            menuBar = new FacebookMenuBar(driver);
            searchPage = new FacebookSearchPage(driver);
            startPage.PrepareToTestsOnUserAccount();
        }

        [Test]
        public void CheckIfHeaderShowsSearchedWord()
        {
            var wordToSearch = "McDonald";
            menuBar.ClickSearch();
            menuBar.EnterWordInSearch(wordToSearch);
            var wordInHeader =  searchPage.FindWordInResultHeader();
            Assert.That(wordInHeader, Is.EqualTo(wordToSearch));
        }

        [TearDown]
        public void TearDown()
        {
            menuBar.Logout();
            driver.Quit();
        }

    }
}