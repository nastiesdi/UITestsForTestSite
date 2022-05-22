using OpenQA.Selenium;
using WebDriverFramework.WebDriver.Elements;

namespace PageObjects
{
    public class MainPage: WebDriverFramework.WebDriver.BaseForm
    {
        private static readonly By titleLocator = By.CssSelector("img.banner-image");
        private static readonly string NavigationButtonLocator = "//div[contains(@class, 'top-card')][.//div[contains(@class, 'card-body')]//h5[contains(text(), '{0}')]]";

        public MainPage() : base(titleLocator, "Home Page")
        {
        }
         
        public void ClickOnElementsButton()
        {
            ClickOnNavigationButton(HomeCardsEnum.Elements);
        }

        public void ClickOnFormsButton()
        {
            ClickOnNavigationButton(HomeCardsEnum.Forms);
        }

        public void ClickAlersCard()
        {
            ClickOnNavigationButton(HomeCardsEnum.Alerts);
        }

        public void ClickOnWidgetsButton()
        {
            ClickOnNavigationButton(HomeCardsEnum.Widgets);
        }

        public void ClickOnInteractionsButton()
        {
            ClickOnNavigationButton(HomeCardsEnum.Interactions);
        }

        public void ClickOnBookStoreButton()
        {
            ClickOnNavigationButton(HomeCardsEnum.Book);
        }

        public void ClickOnNavigationButton(HomeCardsEnum type)
        {
            GetNavigationButton(type).ClickAndWaitForLoading();
        }

        private Button GetNavigationButton(HomeCardsEnum type)
        {
            return new Button(By.XPath(string.Format(NavigationButtonLocator, type.ToString())), $"Navigation button {type}");
        }

    }
}