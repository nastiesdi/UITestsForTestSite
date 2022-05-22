using System;
using OpenQA.Selenium;

namespace WebDriverFramework.WebDriver.Elements
{
    /// class describes the interface element "DatePicker"
    public class DatePicker : BaseElement
    {
        private const string monthLocator = "//select[contains(@class, 'month-select')]";
        private readonly string monthOptionLocator = monthLocator + "/option[contains(text(), '{0}')]";
        private readonly string dayLocator = "//div[contains(@class, 'react-datepicker__day ') and contains (@aria-label, '{0}')]";
        private const string yearLocator = "//select[contains(@class, 'year-select')]";
        private readonly string yearOptionLocator = yearLocator + "/option[contains(text(), '{0}')]";
        private static readonly Button YearButton = new(By.XPath(yearLocator), "Year button");
        private static readonly Button monthButton = new(By.XPath(monthLocator), "Month button");
        //select[contains(@class, 'year-select')]/option[contains(text(), '1900')]

        /// constructor with two parameters
        /// <param name="locator">locator By of the DatePicker</param>
        /// <param name="name">name of the DatePicker</param>
        public DatePicker(By locator, string name) : base(locator, name)
        { }

        /// gets the type of the link 
        /// <returns>type of the link</returns>
        protected override string GetElementType()
        {
            return "DatePicker";
        }

        public static void SwitchMonth(IWebElement arrow)
        {
            arrow.Click();
        }

        public DateTime GetDate(string date)
        {
            return DateTime.Parse(date);
        }

       public void SelectDate(DateTime date)
        {
            YearButton.Click();
            Button yearOption = new(By.XPath(String.Format(yearOptionLocator, date.Year.ToString())), "Year element");
            yearOption.Click();
            monthButton.Click();
            Button monthOption = new(By.XPath(String.Format(monthOptionLocator, date.ToString("MMMM"))), "Year element");
            monthOption.Click();
            Button day = new(By.XPath(String.Format(dayLocator, date.ToString("MMMM dd"))), "day element");
            day.Click();
        }
    }
}
