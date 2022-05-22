using PageObjects;
using NUnit.Framework;
using WebDriverFramework.WebDriver;
using PageObjects.Enum;

namespace TestsForHWProject
{
    [TestFixture]
    public class FileTest : BaseTest
    {
        private MainPage mainPage;
        private ElementsPage elementPage;
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
            mainPage.ClickOnElementsButton();
            elementPage = new();
            elementPage.AssertIsOpen();
            elementPage.ClickNavigationButton(NavigationItems.UploadAndDownload);
            DocumentDownloadingPage downloadAndUpload = new();
            downloadAndUpload.AssertIsOpen();
            //downloadAndUpload.DownloadButton.Click();
            //Browser.Sleep(2000);
            downloadAndUpload.DownloadFilec();


            //LogStep(2, "Click Button to see alert");
            //elementPage.ClickUsualAlertButton();
            //alertPage.VerifyUsualAlertIsOpened();
        }
    }
}