using System;
using OpenQA.Selenium;
using PageObjects.Enum;
using WebDriverFramework.Util;
using WebDriverFramework.WebDriver.Elements;

namespace PageObjects
{
    public class BrowserWindowsPage : NavigationPage
    {
        private static readonly By titleLocator = By.XPath(string.Format(parrentLocator, EnumExtensions.GetStringMapping(NavigationItems.BrowserWindows)));
        public Button tabButton = new(By.Id("tabButton"), "Tab button");
        public Button windowButtonWrapper = new(By.Id("windowButtonWrapper"), "Tab button");
        public Button msgWindowButtonWrapper = new(By.Id("tabButton"), "Tab button");
        public Label titleForNewTab = new(By.Id("sampleHeading"), "New page header title");

        public BrowserWindowsPage() : base(titleLocator, "Browser Windows Page")
        { 
        }
    }
}
