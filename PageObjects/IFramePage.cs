using OpenQA.Selenium;
using PageObjects.Enum;

namespace PageObjects
{
    public class IFramePage : AllertsFrameAndWindowsPage
    {
        private static readonly By titleLocator = By.XPath(string.Format(parrentLocator, NavigationItems.Frames.ToString()));
        private static readonly By frame1 = By.Id("frame1");
        private static readonly By frame2 = By.Id("frame2");
        private static readonly string textFromFrameID = "sampleHeading";

        public IFramePage() : base(titleLocator)
        {
        }

        public string GetTextFromFirstFrame()
        {
            return GetTextFromIframe(frame1);
        }

        public string GetTextFromSecondFrame()
        {
            return GetTextFromIframe(frame2);
        }

        private string GetTextFromIframe(By frame)
        {
            Driver.SwitchTo().Frame(Driver.FindElement(frame));
            var textFromFrameElement = Driver.FindElement(By.Id(textFromFrameID));
            string text = textFromFrameElement.Text;
            Driver.SwitchTo().DefaultContent();
            return text;
        }
    }
}
