using NUnit.Framework;
using OpenQA.Selenium;
using PageObjects.Enum;
using WebDriverFramework.Util;
using WebDriverFramework.WebDriver;
using WebDriverFramework.WebDriver.Elements;

namespace PageObjects
{
    public class AllertsPage : AllertsFrameAndWindowsPage
    {
        private static readonly By titleLocator = By.XPath(string.Format(parrentLocator, HomeCardsEnum.Alerts.ToString()));
        private static readonly Label confirmResult = new Label(By.Id("confirmResult"), "Label confirmResult is created");
        private static readonly Label promptResult = new Label(By.Id("promptResult"), "Label promptResult is created");
        private IAlert alert_win;

        public AllertsPage() : base(titleLocator)
        {
        }

        public void ClickUsualAlertButton()
        {
            ClickAlertButton(AlertsEnum.alertButton);
        }

        public void ClickTimerAlertButton()
        {
            ClickAlertButton(AlertsEnum.timerAlertButton);
        }

        public void ClickConfirmAlertButton()
        {
            ClickAlertButton(AlertsEnum.confirmButton);
        }

        public void ClickPromptAlertButton()
        {
            ClickAlertButton(AlertsEnum.promtButton);
        }

        private void ClickAlertButton(AlertsEnum alertId)
        {
            var alertButton = GetAlertButton(alertId.ToString());
            alertButton.Click();
        }

        public void VerifyUsualAlertIsOpened()
        {
            VerifyAlertIsOpened(EnumExtensions.GetDescription(AlertsEnum.alertButton));
        }
     
        public void VerifyConfirmAlertIsOpened()
        {
            VerifyAlertIsOpened(EnumExtensions.GetDescription(AlertsEnum.confirmButton));
        }

        public void VerifyAlertIsConfirmed()
        {
            string ActualText = confirmResult.GetText();
            Assert.AreEqual(ActualText, EnumExtensions.GetDescription(AlertResultEnum.OkSelected));
        }

        public void VerifyAlertWithTextIsComplited(string enteredText)
        {
            string ActualText = promptResult.GetText();
            Assert.AreEqual(ActualText, $"You entered {enteredText}");
        }

        public void VerifyPromtAlertIsOpened()
        {
            VerifyAlertIsOpened(EnumExtensions.GetDescription(AlertsEnum.promtButton));
        }

        public void VerifyTimerAlertIsOpened()
        {
            VerifyAlertIsOpened(EnumExtensions.GetDescription(AlertsEnum.timerAlertButton));
        }

        private void VerifyAlertIsOpened(string expectedAlertText)
        {
            alert_win = Driver.SwitchTo().Alert();
            Browser.Sleep(Browser.TimeoutForElementWaiting);
            Assert.AreEqual(expectedAlertText, alert_win.Text, $"Alert doesn't contain {expectedAlertText}");
            Log.Debug($"Expected alert text : {expectedAlertText}, Actual alert test: {alert_win.Text}");
        }

        public void SendText(string text)
        {
            alert_win.SendKeys(text);
            Browser.Sleep(Browser.TimeoutForElementWaiting);
        }

        public void CloseAlert()
        {
            alert_win.Accept();
            Browser.Sleep(Browser.TimeoutForElementWaiting);
            alert_win = null;
        }

        private Button GetAlertButton(string id)
        {
            return new Button(By.Id(id), $"Allert button with id {id}");
        }


    }
}
