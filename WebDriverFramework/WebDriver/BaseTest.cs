using System;
using NUnit.Framework;
namespace WebDriverFramework.WebDriver
{
    [TestFixture]
    public abstract class BaseTest : BaseEntity
    {
        public static string testName;


        [SetUp]
        public virtual void InitBeforeTest()
        {
            Browser.GetDriver();
            Browser.NavigateToStartPage();
            Browser.WindowMaximise();
            testName = this.GetType().Name;
        }

        public virtual void RunTest()
        {

        }

        public virtual void RunTest(int id)
        {

        }


        [TearDown]
        public virtual void CleanAfterTest()
        {
            try
            {
                Logger.Instance.Info("Close all instanse and browser");
            }
            catch (Exception e)
            {
                Logger.Instance.Fail(
                    "Test was finished but there are some issues with result analyzing. Please check that all right configured\r\n" +
                    "Exception:" + e.Message);
            }
            finally
            {
                Browser.Quit();
                Logger.Dispose();
            }
        }

        protected override string FormatLogMsg(string message)
        {
            return $"{Logger.LogDelimiter} {message}";
        }
    }
}