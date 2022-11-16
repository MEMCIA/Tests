using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Tests
{
    class FacebookStartPage
    {

        public FacebookStartPage(IWebDriver driver, User user)
        {
            _driver = driver;
            _user = user;

        }

        public void Open()
        {
             _driver.Navigate().GoToUrl(FacebookStartPage.Url);
        }

        public void AcceptOnlyEssentialCookiesBeforeLogin()
        {
            var locatorButtonEssentialCookiesAccept = By.CssSelector("button[data-testid='cookie-policy-manage-dialog-accept-button']");
            Utils.ClickElementWithLocator(locatorButtonEssentialCookiesAccept, _driver, true);
        }

        public void AcceptOnlyEssentialCookiesAfterLogin()
        {
            string urlCookies = "https://www.facebook.com/privacy/consent/user_cookie_choice/?source=pft_user_cookie_choice";
            bool isRequestToAcceptCookies = Utils.CheckIfUrlIsTheSame(urlCookies, _driver);
            if (!isRequestToAcceptCookies) return;

            var locatorButtonEssentialCookiesAccept = By.XPath("//span[text()='Zezwól tylko na niezbędne pliki cookie']");
            Utils.ClickElementWithLocator(locatorButtonEssentialCookiesAccept, _driver, true);
            WebDriverWait wait = new WebDriverWait(_driver, Utils.defaultTimeOut);
            wait.Until(async (driver) => driver.Url != urlCookies);
        }

        public void Login()
        {
            EnterEmail();
            EnterPassword();
        }

        void EnterEmail()
        {
            var locatorIdEmail = By.Id("email");
            Utils.EnterTextInElementWithLocator(locatorIdEmail, _driver, _user.Email, false, false);
        }

        void EnterPassword()
        {
            var locatorIdPassword = By.Id("pass");
            Utils.EnterTextInElementWithLocator(locatorIdPassword, _driver, _user.Password, true, true);
        }

        public void PrepareToTestsOnUserAccount()
        {
            Open();
            AcceptOnlyEssentialCookiesBeforeLogin();
            Login();
            AcceptOnlyEssentialCookiesAfterLogin();
        }

    private User _user;
    private IWebDriver _driver;
    public static readonly string Url = "https://pl-pl.facebook.com/";
    }
}
