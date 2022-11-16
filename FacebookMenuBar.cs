using OpenQA.Selenium;

namespace Tests
{
    class FacebookMenuBar
    {

        public FacebookMenuBar(IWebDriver driver)
        {
            _driver = driver;
        }

        public void ClickSearch()
        {
            var locatorSearchButton = By.CssSelector("input[placeholder = 'Szukaj na Facebooku']");
            Utils.ClickElementWithLocator(locatorSearchButton, _driver, false);
        }

        public void EnterWordInSearch(string word)
        {
            var locatorFinder = By.CssSelector("input[placeholder = 'Szukaj na Facebooku']");
            Utils.EnterTextInElementWithLocator(locatorFinder, _driver, word, true, false);
        }

        public void ClickAccountSymbol()
        {
            var locatorAccountSymbol = By.CssSelector("svg[aria-label='Twój profil']");
            Utils.ClickElementWithLocator(locatorAccountSymbol, _driver, false);
        }

        public void Logout()
        {
            ClickAccountSymbol();
            var locatorButtonLogout = By.XPath("//span[contains(text(),'Wyloguj')]");
            Utils.ClickElementWithLocator(locatorButtonLogout, _driver, false);
        }

        private IWebDriver _driver;

    }
}
