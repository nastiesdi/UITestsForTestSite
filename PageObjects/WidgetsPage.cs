using System;
using OpenQA.Selenium;

namespace PageObjects
{
    public class WidgetsPage : NavigationPage
    {
        private static readonly By titleLocator = By.XPath(string.Format(parrentLocator, HomeCardsEnum.Widgets.ToString()));

        public WidgetsPage() : base(titleLocator, "Widget Page")
        {
        }
    }
}
