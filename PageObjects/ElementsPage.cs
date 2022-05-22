using OpenQA.Selenium;

namespace PageObjects
{
    public class ElementsPage : NavigationPage
    {
       private static readonly By titleLocator = By.XPath(string.Format(parrentLocator, HomeCardsEnum.Elements.ToString()));

        public ElementsPage() : base(titleLocator, "Elements Page")
        {
        }
    }
}
