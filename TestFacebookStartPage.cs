using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Tests
{
    public class TestFacebookMainPage
    {
        private IWebDriver driver;
        private User user;
        private FacebookStartPage startPagePL;
        private FacebookMenuBar menuBar;


        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            //language of user should be set to polish
            user = new User("fezqbutwoa_1666692643@tfbnw.net", "12345T");
            startPagePL = new FacebookStartPage(driver, user);
            menuBar = new FacebookMenuBar(driver);
            startPagePL.open();
            startPagePL.acceptOnlyEssentialCookiesBeforeLogin();
        }

        [Test]
        public void CheckIfLoginTakesUserToRightPage()
        {
            startPagePL.login();
            startPagePL.acceptOnlyEssentialCookiesAfterLogin();
            string currentUrl = driver.Url;   
            Assert.That(currentUrl, Is.EqualTo(FacebookWelcomePage.Url));
        }

        [TearDown]
        public void TearDown()
        {
            menuBar.logout();
            driver.Quit();
        }

    }
}