using System;
using OpenQA.Selenium;

namespace WebDriverFramework.WebDriver
{

    public abstract class BaseEntity
    {
        /// Logger singletone instance
        protected Logger Log = Logger.Instance;

        /// Driver singletone instance
        protected  IWebDriver Driver = Browser.GetDriver();

        protected abstract String FormatLogMsg(string message);

        /// debug in log
        protected void Debug(String message)
        {
            Log.Debug(FormatLogMsg(message));
        }

        /// info in log
        protected void Info(String message)
        {
            Log.Info(FormatLogMsg(message));
        }

        /// warn in log
        protected void Warn(String message)
        {
            Log.Warn(FormatLogMsg(message));
        }

        /// error in log
        protected void Error(String message)
        {
            Log.Error(FormatLogMsg(message));
        }

        /// fatal in log
        protected void Fatal(String message)
        {
            Log.Fail(FormatLogMsg(message));
        }

        /// log step in log
        protected void LogStep(int step, String message)
        {
            Log.LogStep(step, message);
        }

        /// log step information starts from NOT IMPLEMENTED
        protected void LogNotImplementedStep(int step, String message)
        {
            LogStep(step, "NOT IMPLEMENTTED::" + message);
        }

        /// log step without messsage in log
        protected void LogStep(int step)
        {
            Log.LogStep(step);
        }

        /// log step range of steps with action message
        protected void LogStep(int step, int toStep, string message)
        {
            Log.LogStep(step, toStep, message);
        }
    }
}
