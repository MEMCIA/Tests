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
            user = new User("styuurowsz_1666447403@tfbnw.net", "12345T");
            startPage = new FacebookStartPage(driver, user);
            menuBar = new FacebookMenuBar(driver);
            searchPage = new FacebookSearchPage(driver);
            startPage.prepareToTestsOnUserAccount();
        }

        [Test]
        public void CheckIfHeaderShowsSearchedWord()
        {
            var wordToSearch = "McDonald";
            menuBar.clickSearch();
            menuBar.enterWordInSearch(wordToSearch);
            var wordInHeader =  searchPage.findWordInResultHeader();
            Assert.That(wordInHeader, Is.EqualTo(wordToSearch));
        }

        [TearDown]
        public void TearDown()
        {
            menuBar.logout();
            driver.Quit();
        }

    }
}