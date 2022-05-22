using System;
using System.Threading;
using NLog;

namespace WebDriverFramework.WebDriver
{
    public class Logger
    {
        private readonly NLog.Logger _log;
        public static readonly string LogDelimiter = "::";
        private int _lastExecutedStep;
        private readonly string _testName = BaseTest.testName;
        private static readonly string SCREENSHOT = "<!({0}";
        private readonly string patternForLogTime = " MM/dd/yyyy hh:mm:ss.fff tt";
        private static Logger _instance = new Logger();


        public Logger()
        {
            _log = LogManager.GetCurrentClassLogger();
        }

        public static Logger Instance => _instance ?? new Logger();

        public string LastExecutedStepMessage { get; set; } = "";

        public void Fail(string message)
        {
            _log.Info($"{_testName}{DateTime.Now.ToString(patternForLogTime)} - {message}");
            _log.Fatal($"{_testName}{DateTime.Now.ToString(patternForLogTime)} - {message}");
        }

        public void Info(string message)
        {
            _log.Info($"{_testName}{DateTime.Now.ToString(patternForLogTime)} - {message}");
        }

        public void InfoWithoutTimeStamp(string message)
        {
            _log.Info($"{_testName}{message}");
        }

        public void PreconditionStart(String title)
        {
            //SetPreconditionMessage();
            string formattedName = $"===== CommandLineExecutor: '{title}' =====";
            string delims = "";
            int nChars = formattedName.Length;
            for (int i = 0; i < nChars; i++)
            {
                delims += "+";
            }
            Info(delims);
            Info(formattedName);
            Info(delims);
        }

        public void PreconditionFailed(String title)
        {

            string formattedName = $"===== CommandLineExecutor Failed: '{title}' =====";
            string delims = "";
            int nChars = formattedName.Length;
            for (int i = 0; i < nChars; i++)
            {
                delims += "#";
            }
            Info(delims);
            Info(formattedName);
            Info(delims);
        }

        public void PreconditionPassed(String title)
        {
            string formattedName = $"===== CommandLineExecutor Passed: '{title}' =====";
            string delims = "";
            int nChars = formattedName.Length;
            for (int i = 0; i < nChars; i++)
            {
                delims += "+";
            }
            Info(delims);
            Info(formattedName);
            Info(delims);
        }

        public void ReportScreenshot(string path)
        {
            InfoWithoutTimeStamp(string.Format(SCREENSHOT, path));
        }

        public void LogTreadId()
        {
            var managedThreadId = Thread.CurrentThread.ManagedThreadId;
            Instance.Warn($"class is executed in '{managedThreadId}' managedThreadId");
        }
        
        public void LogStep(int step, int toStep)
        {
            StoreLastStepInfo(step);
            Info(
                $"[Steps {step} - {toStep}]");
        }

        public void LogStep(int step)
        {
            StoreLastStepInfo(step);
            Info(
                $"[ Step {step} ]");
        }

        public void LogStep(int step, string message)
        {
            StoreLastStepInfo(step);
            Info(
                $"[ Step {step} ]: {message}");
        }

        private void StoreLastStepInfo(int step)
        {
            _lastExecutedStep = step;
            LastExecutedStepMessage = $" [step #'{_lastExecutedStep}']";
        }

        public void LogStep(int step, int toStep, string message)
        {
            LogStep(step, toStep);
            Info(message);
            Info("----------------------------------------------------------------------------------------------");
        }

        public void Passed(String title)
        {
            string formattedName = $"******* Test Case: '{title}' Passed! *******";
            string delims = "";
            int nChars = formattedName.Length;
            for (int i = 0; i < nChars; i++)
            {
                delims += "*";
            }
            Info(delims);
            Info(formattedName);
            Info(delims);
        }

        public void Debug(string format)
        {
            _log.Debug($"{_testName}{DateTime.Now.ToString(patternForLogTime)} - {format}");
        }

        public void Warn(string formatLogMsg)
        {
            _log.Warn($"{_testName}{DateTime.Now.ToString(patternForLogTime)} - {formatLogMsg}");
        }

        public void Error(string formatLogMsg)
        {
            _log.Error($"{_testName}{DateTime.Now.ToString(patternForLogTime)} - {formatLogMsg}");
        }

        public static void Dispose()
        {
            _instance = null;
        }
    }
}
