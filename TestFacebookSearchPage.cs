using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Tests
{
    public class TestFacebookSearchPage
    {
        WebDriver driver;
        FacebookStartPage startPage;
        FacebookWelcomePage welcomePage;
        User user;
        FacebookMenuBar menuBar ;
        FacebookSearchPage searchPage ;



        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            //language of user should be set to polish
            user = new User("styuurowsz_1666447403@tfbnw.net", "12345T");
            startPage = new FacebookStartPage(driver, user);
            welcomePage = new FacebookWelcomePage(driver);
            menuBar = new FacebookMenuBar(driver);
            searchPage = new FacebookSearchPage(driver);
            startPage.prepareToTestsOnUserAccount(FacebookWelcomePage.Url);
        }

        [Test]
        public void CheckIfLoginTakesUserToRightPage()
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