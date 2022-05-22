using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using WebdriverFramework.Framework.WebDriver;
using WebDriverFramework.WebDriver;

namespace WebDriverFramework.WebDriver
{
    public sealed class BrowserFactory
    {
        public static BrowserFactory _instance = _instance ?? new BrowserFactory();
        private static IWebDriver driver;
        public string LoginPage = "https://demoqa.com/";
        public int _timeoutForPageToLoad = 5;
        public int ImplWait =  44;
        public string currentBrowser = ConfigManager.Browser;

        private BrowserFactory()
        {
        }

        public IWebDriver SetUp(out BrowserEnum browserType)
        {

            if (Enum.TryParse(currentBrowser, true, out browserType))
            {
                InitBrowser(browserType);
                return driver;
            }
            Logger.Instance.Info(browserType.ToString());
            Logger.Instance.Fail("couldn't parse browser type");

            return null;
        }

        public static void LoadApplication(string url)
        {
            driver.Url = url;
        }

        public void CloseDriver()
        {
            driver.Close();
            driver.Quit();
            driver.Dispose();
            driver = null;
        }

        private static void InitBrowser(BrowserEnum browserName)
        {
            switch (browserName)
            {
                case BrowserEnum.Firefox:
                    if (driver == null)
                    {
                        driver = new FirefoxDriver();
                    }
                    break;

                case BrowserEnum.Chrome:
                    if (driver == null)
                    {
                        driver = new ChromeDriver();
                    }
                    break;
            }
        }
    }
}