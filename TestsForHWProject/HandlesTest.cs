using NUnit.Framework;
using PageObjects;
using PageObjects.Enum;
using WebDriverFramework.WebDriver;

namespace TestsForHWProject
{
    [TestFixture]
    public class HandlesTest : BaseTest
    {
        private MainPage mainPage;
        private string expectedTextFromNewPage = "This is a sample page";

        [SetUp]
        public void SetUp()
        {
            mainPage = new();
        }

        [Test]
        public override void RunTest()
        {
            mainPage.AssertIsOpen();
            LogStep(1, "Click Alerts, Frame and Window button");
            mainPage.ClickAlersCard();
            AllertsPage alertPage = new();
            alertPage.AssertIsOpen();

            LogStep(2, "Click Button to see Browser Windows page");
            alertPage.ClickNavigationButton(NavigationItems.BrowserWindows);
            BrowserWindowsPage browserWindowPage = new();

            LogStep(3, "Click Button New Tab");
            browserWindowPage.tabButton.Click();
            browserWindowPage.SwitchToLastWindow();
            Assert.AreEqual(expectedTextFromNewPage, browserWindowPage.titleForNewTab.GetText());
            Assert.True(Driver.Url.Contains("/sample"));

            LogStep(4, "Close  window");
            browserWindowPage.CloseCurentTab();
            browserWindowPage.AssertIsOpen();

            LogStep(5, "Select Element links in left menu");
            browserWindowPage.ClickHeaderNavigationButton(HomeCardsEnum.Elements);
            browserWindowPage.ClickNavigationButton(NavigationItems.Links);
            LinksPage linkPage = new();
            linkPage.AssertIsOpen();

            LogStep(6, "Go to link Home");
            linkPage.SimpleLink.Click();
            linkPage.SwitchToLastWindow();
            mainPage.AssertIsOpen();

            LogStep(7, "Switch to previous tab");
            mainPage.SwitchToFirstWindow();
            linkPage.AssertIsOpen();
        }
    }
}
