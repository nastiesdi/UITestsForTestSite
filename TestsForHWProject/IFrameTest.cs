using NUnit.Framework;
using PageObjects;
using PageObjects.Enum;
using WebDriverFramework.WebDriver;

namespace TestsForHWProject
{
    [TestFixture]
    public class IFrameTest : BaseTest
    {
        private MainPage mainPage;

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

            LogStep(2, "Click Button to see Frame page");
            alertPage.ClickNavigationButton(NavigationItems.Frames);

            LogStep(3, "Verify 2 text Are equal");
            Browser.Sleep(2000);
            IFramePage frames = new IFramePage();
            string TestFromFirstFrame = frames.GetTextFromFirstFrame();
            string TestFromSecindFrame = frames.GetTextFromSecondFrame();
            Assert.AreEqual(TestFromFirstFrame, TestFromSecindFrame);
        }
    }
}
