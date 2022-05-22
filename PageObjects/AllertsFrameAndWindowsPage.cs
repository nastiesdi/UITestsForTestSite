using OpenQA.Selenium;

namespace PageObjects
{
    public class AllertsFrameAndWindowsPage : NavigationPage
    {
        private static readonly By titleLocator = By.XPath(string.Format(parrentLocator, HomeCardsEnum.Alerts.ToString()));

        public AllertsFrameAndWindowsPage(By titleLocator) : base(titleLocator, "Allerts Page")
        {
        }
    }
}
