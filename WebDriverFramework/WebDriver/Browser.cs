using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using System.Threading;
using WebdriverFramework.Framework.WebDriver;

namespace WebDriverFramework.WebDriver
{
    public static class Browser
    {
        private static IWebDriver _currentDriver;
        private static BrowserEnum _currentBrowser;
        public static int TimeoutForElementWaiting = 2000;
        public static string CurrentBrowser = ConfigManager.Browser;

        public static BrowserFactory instance = BrowserFactory._instance;

        public static IWebDriver GetDriver()
        {
            InitDriver();
            return _currentDriver;
        }

        public static void InitDriver()
        {
            if (_currentDriver != null)
            {
                return;
            }

            //Logger.Instance.Info($"Start of '{_propBrowser}' browser instance construction");
            _currentDriver = instance.SetUp(out _currentBrowser);
            _currentDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(instance.ImplWait);
            //Logger.Instance.Info($"End of '{CurrentBrowser}' browser instance construction");
        }

        public static void NavigateToStartPage()
        {
            _currentDriver.Navigate().GoToUrl(instance.LoginPage);
            WaitForPageToLoad();
        }

        public static void WaitForPageToLoad()
        {
            var wait = new WebDriverWait(_currentDriver, TimeSpan.FromSeconds(Convert.ToDouble(instance._timeoutForPageToLoad)));
            try
            {
                wait.Until(waiting =>
                {
                    var result =
                        ((IJavaScriptExecutor)_currentDriver).ExecuteScript(
                            "return document['readyState'] ? 'complete' == document.readyState : true");
                    return result is bool && (Boolean)result;
                });
            }
            catch (Exception e)
            {
                Logger.Instance.Debug(e.StackTrace);
            }
        }

        public static void Refresh()
        {
            try
            {
                GetDriver().Navigate().Refresh();
            }
            catch (Exception e)
            {
                Logger.Instance.Debug("Error while page was refreshed.\r\n Exception: " + e.Message);
            }
        }

        public static void Quit()
        {
            if (_currentDriver != null || instance != null)
            {
                instance.CloseDriver();
                _currentDriver = null;
            }
        }

        public static void WindowMaximise()
        {
            _currentDriver.Manage().Window.Maximize();
        }

        public static void NavigateTo(string url)
        {
            //Logger.Instance.Info("Navigate to url:" + url);
            try
            {
                _currentDriver.Navigate().GoToUrl(url);
            }
            catch (WebDriverException e)
            {
                Logger.Instance.Info($"page with url '{url}' causes error: '{e}'");
                Refresh();
                _currentDriver.Navigate().GoToUrl(url);
            }
        }

        public static TimeSpan GetElementTimeoutInSeconds()
        {
            return GetElementTimeoutInSeconds(1);
        }

        public static int WindowCount()
        {
            return GetDriver().WindowHandles.Count;
        }

        /// waiting, while number of open windows will be more than previous
        public static void WaitForNewWindow(int prevWndCount)
        {
            int wndCount = prevWndCount;
            var wait = new WebDriverWait(GetDriver(), TimeSpan.FromSeconds(TimeoutForElementWaiting));
            wait.Until(d => d.WindowHandles.Count > wndCount);
        }

        /// Switch to the window with index
        public static void SwitchWindow(int index)
        {
            GetDriver().SwitchTo().Window(GetDriver().WindowHandles.ToArray()[index]);
        }

        public static TimeSpan GetElementTimeoutInSeconds(int multyplyerForTimeout)
        {
            return TimeSpan.FromSeconds(multyplyerForTimeout * Convert.ToDouble(ConfigManager.ElementTimeout));
        }

        /// sleep process
        public static void Sleep(int mSeconds)
        {
            Thread.Sleep(mSeconds);
        }
    }
}
