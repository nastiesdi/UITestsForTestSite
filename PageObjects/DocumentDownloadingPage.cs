using System;
using System.Threading.Tasks;
using OpenQA.Selenium;
using PageObjects.Enum;
using WebDriverFramework.Util;
using WebDriverFramework.WebDriver.Elements;

namespace PageObjects
{
    public class DocumentDownloadingPage : NavigationPage
    {
        private static readonly By titleLocator = By.XPath($"//div[contains(@class, 'main-header')][contains(text(), '{EnumExtensions.GetStringMapping(NavigationItems.UploadAndDownload)}')]");
        public readonly By DownloadButton = By.XPath("//a[@id='downloadButton']");

        public DocumentDownloadingPage() : base(titleLocator, "Web Tables")
        {
        }

        public void DownloadFilec()
        {
            //Button DownloadButton = new(By.Id("downloadButton"), "Download");
            var downloadBtn = Driver.FindElement(DownloadButton);
            FileExtensions.DownloadFile(downloadBtn, "DownloadedDoc.jpeg");
            //task.Start();
        }
    }
}

