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

        public void open()
        {
             _driver.Navigate().GoToUrl(FacebookStartPage.Url);
        }

        public void acceptOnlyEssentialCookiesBeforeLogin()
        {
            var locatorButtonEssentialCookiesAccept = By.CssSelector("button[data-testid='cookie-policy-manage-dialog-accept-button']");
            Utils.ClickElementWithLocator(locatorButtonEssentialCookiesAccept, _driver, true);
        }

        public void acceptOnlyEssentialCookiesAfterLogin()
        {
            string urlCookies = "https://www.facebook.com/privacy/consent/user_cookie_choice/?source=pft_user_cookie_choice";
            bool isRequestToAcceptCookies = Utils.CheckIfUrlIsTheSame(urlCookies, _driver);
            if (!isRequestToAcceptCookies) return;

            var locatorButtonEssentialCookiesAccept = By.XPath("//span[text()='Zezwól tylko na niezbędne pliki cookie']");
            Utils.ClickElementWithLocator(locatorButtonEssentialCookiesAccept, _driver, true);
            WebDriverWait wait = new WebDriverWait(_driver, Utils.defaultTimeOut);
            wait.Until(async (driver) => driver.Url != urlCookies);
        }

        public void login()
        {
            enterEmail();
            enterPassword();
        }

        void enterEmail()
        {
            var locatorIdEmail = By.Id("email");
            Utils.EnterTextInElementWithLocator(locatorIdEmail, _driver, _user.Email, false, false);
        }

        void enterPassword()
        {
            var locatorIdPassword = By.Id("pass");
            Utils.EnterTextInElementWithLocator(locatorIdPassword, _driver, _user.Password, true, true);
        }

        public void prepareToTestsOnUserAccount()
        {
            open();
            acceptOnlyEssentialCookiesBeforeLogin();
            login();
            acceptOnlyEssentialCookiesAfterLogin();
        }

    private User _user;
    private IWebDriver _driver;
    public static readonly string Url = "https://pl-pl.facebook.com/";
    }
}
