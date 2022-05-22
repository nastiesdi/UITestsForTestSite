//div[contains(@class, 'main-header') and contains(text(), 'Elements')]
using OpenQA.Selenium;
using PageObjects.Enum;
using WebDriverFramework.Util;
using WebDriverFramework.WebDriver;
using WebDriverFramework.WebDriver.Elements;

namespace PageObjects
{
    public class NavigationPage : BaseForm
    {
        protected static readonly string parrentLocator = "//div[contains(@class, 'main-header') and contains(text(), '{0}')]";
        protected static readonly string navigationItemLocator = "//li[contains(@class, 'btn-light')][.//span[contains(@class, 'text') and text()='{0}']]";
        protected static readonly string groupHeader = "//span[contains(@class, 'group-header')][.//div[contains(@class, 'text') and text()='{0}']]";

        public NavigationPage(By titleLocator, string title) : base(titleLocator, "Navigation Page")
        {
        }

        public void ClickNavigationButton(NavigationItems navigationItem)
        {
            Button button = GetNavigationButton(EnumExtensions.GetStringMapping(navigationItem), navigationItemLocator);
            button.ScrollIntoViewWithCenterAligning();
            button.Click();
        }

        public void ClickHeaderNavigationButton(HomeCardsEnum navigationItem)
        {
            Button button = GetNavigationButton(EnumExtensions.GetStringMapping(navigationItem), groupHeader);
            button.ScrollIntoViewWithCenterAligning();
            button.Click();
        }

        private Button GetNavigationButton(string textFromLocator, string locator)
        {
            return new Button(By.XPath(string.Format(locator, textFromLocator)), $"Navigation button {textFromLocator}");
        }
    }
}

