using System;
using System.Linq;
using System.Threading;
//using System.Reflection.Emit;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using WebDriverFramework.WebDriver.Elements;

namespace WebDriverFramework.WebDriver
{
    public class BaseForm : BaseEntity
    {
        /// Locator for indentification of the form
        protected By TitleLocator;

        /// Name of the form
        protected string Title;

        /// Name of the Form/Page
        /// Allow make log more readable
        public static string TitleForm;

        /// constructor with two parameters
        protected BaseForm(By titleLocator, string title)
        {
            TitleLocator = titleLocator;
            Title = TitleForm = title;
            AssertIsOpen();
        }

        /// wait until element is absent
        public void WaitForIsAbsent(double seconds)
        {
            try
            {
                var wait = new WebDriverWait(Browser.GetDriver(), TimeSpan.FromSeconds(seconds));
                wait.Until(d => (Browser.GetDriver().FindElements(TitleLocator).Count <= 0));
            }
            catch (TimeoutException ex)
            {
                Log.Warn(ex.Message);
                Log.Fail(Title + " is still present ");
            }
        }

        public static int RandomValue(int min, int max)
        {
            Random rnd = new Random();
            return rnd.Next(min, max);
        }

        /// Get Form Title
        public string GetFormTitle()
        {
            return Title;
        }

        /// awaits the locator form (timeoutForElementWaiting) completes the test or if the element is not found
        public void AssertIsOpen()
        {
            var label = new Label(TitleLocator, Title);
            try
            {
                label.WaitForElementIsPresent();
                label.WaitForIsEnabled(); //added 
                Info("appears");
            }
            catch (Exception ex)
            {
                if (!label.IsPresent())
                {
                    Fatal("doesn't appear \n" + ex.Message);
                    //Browser.SaveScreenShot(TitleForm + "_" + _counter++);
                }
            }
        }

        ///  assertion what page is closed
        public void AssertIsClosed()
        {
            //var label = new Label(TitleLocator, Title);
            //for (int i = 0; i < Configuration.HowManyTimesTryToInstanceBrowser; i++)
            //{
            //    if (label.IsPresent())
            //    {
            //        Thread.Sleep(int.Parse(Configuration.FileDownloadingTimeoutMs));
            //    }
            //}
            //label.AssertIsAbsent();
        }

        /// Wait And Assert until element is absent
        public void WaitAndAssertIsDisappeared(int seconds)
        {
            //var label = new Label(TitleLocator, Title);
            //for (int i = 0; i < seconds; i++)
            //{
            //    if (label.IsPresent())
            //    {
            //        Thread.Sleep(1000);
            //    }
            //}
            //label.AssertIsAbsent();
        }

        /// formats the value for logging "name page - log splitter - the message"
        protected override string FormatLogMsg(string message)
        {
            return $"Form '{Title}' {Logger.LogDelimiter} {message}";
        }

        public void SwitchToLastWindow()
        {
            var availableWindows = Driver.WindowHandles;
            if (availableWindows.Count > 1)
            {
                Driver.SwitchTo().Window(availableWindows[availableWindows.Count - 1]);
            }

        }

        public void CloseCurentTab ()
        {
            var availableWindows = Driver.WindowHandles;
            Driver.Close();
            Driver.SwitchTo().Window(availableWindows[availableWindows.Count - 2]);
        }

        public void SwitchToFirstWindow()
        {
            var availableWindows = Driver.WindowHandles;
            if (availableWindows.Count > 1)
            {
                Driver.SwitchTo().Window(availableWindows[0]);
            }
        }

        public Exception SwitchToPreviousTab()
        {
            throw new NotImplementedException();
            //var availableWindows = Driver.WindowHandles;
            //Driver.SwitchTo().Window(1)
            //var currentTab = Driver.CurrentWindowHandle;
            //if (availableWindows.Count > 1)
            //{
            //    var currentTabIndex = availableWindows.Select(x => x == currentTab).First();
            //    Driver.SwitchTo().Window(currentTabIndex - 1);
            //}
        }
    }
}