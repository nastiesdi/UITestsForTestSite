using System;
using System.IO;
using Microsoft.Extensions.Configuration;

using WebDriverFramework.WebDriver;

namespace WebdriverFramework.Framework.WebDriver
{
    /// <summary>
    /// class with settings for the solution
    /// </summary>
    public class ConfigManager
    {
        protected static IConfigurationRoot ConfigurationData => new ConfigurationBuilder()
            .AddJsonFile("appsettings.json").Build();
            //.AddXmlFile("log4net.config").Build();
        /// <summary>
        /// get from app.config Remote WebDriver default waiting
        /// </summary>
        public static int RemoteWebDriverWait => int.Parse(GetClientSettingsValue(nameof(RemoteWebDriverWait)));

        /// <summary>
        /// get from app.config Remote WebDriver default waiting
        /// </summary>
        public static int LocalWebDriverWait => int.Parse(GetClientSettingsValue(nameof(LocalWebDriverWait)));

        /// <summary>
        /// get from app.config field LoginURL
        /// </summary>
        public static string LoginUrl => GetWebSettingsValue(nameof(LoginUrl));

        /// <summary>
        /// get from app.config field LoginURL
        /// </summary>
        public static string AdminLoginUrl => GetWebSettingsValue(nameof(AdminLoginUrl));

        /// <summary>
        /// get from app.config field Selenium Grid URL
        /// </summary>
        public static string SeleniumGridUrl => GetWebSettingsValue(nameof(SeleniumGridUrl));

        /// <summary>
        /// get from app.config field UseSeleniumGrid
        /// </summary>
        public static bool UseSeleniumGrid => bool.Parse(GetClientSettingsValue(nameof(UseSeleniumGrid)));

        /// <summary>
        /// get from app.config field UseLocalFileDetector
        /// </summary>
        public static bool UseLocalFileDetector => bool.Parse(GetClientSettingsValue(nameof(UseLocalFileDetector)));

        /// <summary>
        /// get from app.config field HowManyTimesTryToInstanceBrowser
        /// </summary>
        public static int HowManyTimesTryToInstanceBrowser => int.Parse(GetClientSettingsValue(nameof(HowManyTimesTryToInstanceBrowser)));

        /// <summary>
        /// get from app.config field element_timeout
        /// </summary>
        public static string ElementTimeout => GetClientSettingsValue(nameof(ElementTimeout));

        /// <summary>
        /// get from app.config field page_timeout
        /// </summary>
        public static string PageTimeout => GetClientSettingsValue(nameof(PageTimeout));

        /// <summary>
        /// get from app.config field page_timeout
        /// </summary>
        public static string QueryTimeout => GetClientSettingsValue(nameof(QueryTimeout));

        /// <summary>
        /// get from app.config field login_request_timeout_ms to validate login process in DB
        /// </summary>
        public static string LoginRequestTimeOut => GetClientSettingsValue(nameof(LoginRequestTimeOut));

        /// <summary>
        /// get from app.config field UseSystemBrowserParameter
        /// </summary>
        public static bool UseSystemBrowserParameter => bool.Parse(GetClientSettingsValue(nameof(UseSystemBrowserParameter)));

        /// <summary>
        /// get from app.config field Browser
        /// </summary>
        public static string Browser => UseSystemBrowserParameter
            ? Environment.GetEnvironmentVariable("BROWSER")
            : GetClientSettingsValue(nameof(Browser));

        /// <summary>
        ///  get boolean value for the field MobileTesting from environment or from app.config
        /// </summary>
        public static bool MobileTesting => bool.Parse(GetClientSettingsValue(nameof(MobileTesting)));

        /// <summary>
        /// get from app.config field file_downloading_timeout_ms
        /// </summary>
        public static string FileDownloadingTimeoutMs => GetClientSettingsValue(nameof(FileDownloadingTimeoutMs));

        /// <summary>
        /// described preferences for database
        /// </summary>
        public static class Database
        {
            /// <summary>
            /// get from app.config field ProviderInvariantName
            /// </summary>
            public static string ProviderInvariantName => GetDatabaseValue(nameof(ProviderInvariantName));

            /// <summary>
            /// get from app.config field ConnectionString
            /// </summary>
            public static string ConnectionString => GetDatabaseValue(nameof(ConnectionString));

            /// <summary>
            /// get alternative string from app.config field ConnectionString
            /// </summary>
            public static string ConnectionStringEmailService => GetDatabaseValue(nameof(ConnectionStringEmailService));
        }

        /// <summary>
        /// described preferences of the launching with browserstack
        /// </summary>
        public static class BrowserStack
        {
            /// <summary>
            /// browserstack hub
            /// </summary>
            public static string Hub => GetBrowserStackValue(nameof(Hub));

            /// <summary>
            /// user to connect to browserstack environment
            /// </summary>
            public static string User => GetBrowserStackValue(nameof(User));

            /// <summary>
            /// key to connect to browserstack environment
            /// </summary>
            public static string Key => GetBrowserStackValue(nameof(Key));

            /// <summary>
            /// browser name according to the browserstack specification
            /// </summary>
            public static string BrowserType => GetBrowserStackValue(nameof(BrowserType));

            /// <summary>
            /// browser version according to the browserstack specification
            /// </summary>
            public static string BrowserVersion => GetBrowserStackValue(nameof(BrowserVersion));

            /// <summary>
            /// OS name according to the browserstack specification
            /// </summary>
            public static string OperationSystem => GetBrowserStackValue(nameof(OperationSystem));

            /// <summary>
            /// OS version according to the browserstack specification
            /// </summary>
            public static string OsVersion => GetBrowserStackValue(nameof(OsVersion));

            /// <summary>
            /// screen resolution according to the browserstack specification
            /// </summary>
            public static string Resolution => GetBrowserStackValue(nameof(Resolution));

            /// <summary>
            /// value run or not (true/false) local browserstack 
            /// </summary>
            public static string Local => GetBrowserStackValue(nameof(Local));

            /// <summary>
            /// value run or not (true/false) debug browserstack 
            /// </summary>
            public static string Debug => GetBrowserStackValue(nameof(Debug));
            //======Mobile browserstack testing=======

            /// <summary>
            /// browserName (for mobile testing) according to the browserstack specification
            /// </summary>
            public static string BrowserName => GetBrowserStackValue(nameof(BrowserName));

            /// <summary>
            /// platform name (for mobile testing) according to the browserstack specification
            /// </summary>
            public static string Platform => GetBrowserStackValue(nameof(Platform));

            /// <summary>
            /// device name (for mobile testing) according to the browserstack specification
            /// </summary>
            public static string Device => GetBrowserStackValue(nameof(Device));

            /// <summary>
            /// device orientation (for mobile testing) according to the browserstack specification
            /// </summary>
            public static string DeviceOrientation => GetBrowserStackValue(nameof(DeviceOrientation));

            /// <summary>
            /// emulator usage (for mobile testing) according to the browserstack specification
            /// value must be true/false
            /// </summary>
            public static bool Emulator => bool.Parse(GetBrowserStackValue(nameof(Emulator)));

            /////=====================================
        }

        /// <summary>
        /// described preferences of the launching with chrome mobile emulator
        /// </summary>
        public static class ChromeMobileEmulator
        {
            /// <summary>
            /// device name
            /// </summary>
            public static string DeviceName => GetBrowserStackValue(nameof(DeviceName));
        }

        /// <summary>
        /// returns value of environment variable
        /// </summary>
        /// <param name="var">variable's name</param>
        /// <param name="defaultValue">default value, will be returned if env var was not setted</param>
        /// <returns>value of environment variable</returns>
        public static string GetEnviromentVar(string var, string defaultValue)
        {
            return Environment.GetEnvironmentVariable(var) ?? defaultValue;
        }

        /// <summary>
        /// get DownloadDirectory browser depended
        /// </summary>
        /// 
        //public static string DownloadDirectory
        //{
        //    get
        //    {
        //        if (Browser.Equals(BrowserEnum.Edge.ToString()))
        //        {
        //            var pathUser = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        //            return Path.Combine(pathUser, "downloads");
        //        }

        //        return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "downloads", Thread.CurrentThread.ManagedThreadId.ToString());
        //    }
        //}

        /// <summary>
        /// get from app.config field and converts into string
        /// </summary>
        protected static string GetClientSettingsValue(string key)
        {
            return GetEnviromentVar(key, ConfigurationData.GetSection("ClientSettings")?[key]);
        }

        /// <summary>
        /// get from app.config-ApiSettings field and converts into string
        /// </summary>
        protected static string GetApiSettingsValue(string key)
        {
            return ConfigurationData.GetSection("ApiClientSettings")?[key];
        }

        /// <summary>
        /// get from app.config-WebSettings field and converts into string
        /// </summary>
        protected static string GetWebSettingsValue(string key)
        {
            return ConfigurationData.GetSection("WebSettings")?[key];
        }

        /// <summary>
        /// get from app.config-BrowserStack field and converts into string
        /// </summary>
        private static string GetBrowserStackValue(string key)
        {
            return ConfigurationData.GetSection("BrowserStackSettings")?[key];
        }

        /// <summary>
        /// get from app.config-Database field and converts into string
        /// </summary>
        private static string GetDatabaseValue(string key)
        {
            return ConfigurationData.GetSection("ConnectionStrings")?[key];
        }
    }
}