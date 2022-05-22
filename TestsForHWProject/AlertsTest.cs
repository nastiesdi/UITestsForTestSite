using PageObjects;
using NUnit.Framework;
using WebDriverFramework.WebDriver;
using PageObjects.Enum;

namespace TestsForHWProject
{
    [TestFixture]
    public class AlertsTest : BaseTest
    {
        private MainPage mainPage;
        private AllertsPage alertPage;
        private string textForTestingPrompt = "My super Test text";


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
            alertPage = new();
            alertPage.AssertIsOpen();
            alertPage.ClickNavigationButton(NavigationItems.Alerts);
            alertPage.AssertIsOpen();

            LogStep(2, "Click Button to see alert");
            alertPage.ClickUsualAlertButton();
            alertPage.VerifyUsualAlertIsOpened();

            LogStep(3, "Click 'OK' button");
            alertPage.CloseAlert();

            LogStep(4, "Click Button to see Confirmation Alertalert");
            alertPage.ClickConfirmAlertButton();
            alertPage.VerifyConfirmAlertIsOpened();

            LogStep(5, "Click 'OK' button");
            alertPage.CloseAlert();
            alertPage.VerifyAlertIsConfirmed();

            LogStep(6, "Click Button to see  prompt box will appear");
            alertPage.ClickPromptAlertButton();
            alertPage.VerifyPromtAlertIsOpened();

            LogStep(7, "Enter Text and click  'OK' button");
            alertPage.SendText(textForTestingPrompt);
            alertPage.CloseAlert();
            alertPage.VerifyAlertWithTextIsComplited(textForTestingPrompt);
        }
    }
}