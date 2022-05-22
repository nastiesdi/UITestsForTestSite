using OpenQA.Selenium;
using PageObjects.Enum;
using WebDriverFramework.Util;
using WebDriverFramework.WebDriver.Elements;

namespace PageObjects
{
    public class LinksPage : NavigationPage
    {
        private static readonly By titleLocator = By.XPath(string.Format(parrentLocator, EnumExtensions.GetStringMapping(NavigationItems.Links)));
        public Link SimpleLink = new Link(By.Id("simpleLink"), "simpleLink");

        public LinksPage() : base(titleLocator, "Links Page")
        {
        }
    }
}
