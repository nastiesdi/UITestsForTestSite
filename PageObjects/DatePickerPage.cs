using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using PageObjects.Enum;
using WebDriverFramework.Util;
using WebDriverFramework.WebDriver.Elements;

namespace PageObjects
{
    public class DatePickerPage : NavigationPage
    {
        private static readonly By titleLocator = By.XPath(string.Format(parrentLocator, EnumExtensions.GetStringMapping(NavigationItems.DatePicker)));
        //public Link SimpleLink = new Link(By.Id("simpleLink"), "simpleLink");
        public readonly DatePicker datePicker = new(By.Id("datePickerMonthYearInput"), "Date Picker");
        public readonly Button previousMonth = new(By.XPath("//button[contains(text(), 'Previous Month')]"), "Previous Month");
        public readonly Button nextMonth = new(By.XPath("//button[contains(text(), 'Next Month')]"), "Previous Month");
        public readonly DatePicker timePicker = new(By.Id("dateAndTimePickerInput"), "Time Picker");

        public DatePickerPage() : base(titleLocator, "Date Picker Page")
        {
        }

        public DateTime GetParsedDate()
        {
            return datePicker.GetDate(datePicker.GetValue());
        }

        public DateTime GetParsedTime()
        {
            return DateTime.Parse(timePicker.GetValue()); ;
        }

        public DateTime FindClosestDay(DateTime currentDate)
        {
            List<DateTime> nearestDates = GetNearestLeapYears(currentDate);
            DateTime currentdate = this.GetParsedDate();
            DateTime searchedDate = nearestDates[0];
            foreach (var item in nearestDates)
            {
                TimeSpan currentDifference = currentdate.Subtract(searchedDate);
                var newDifference = currentdate.Subtract(item);
                if (newDifference < currentDifference)
                {
                    searchedDate = item;
                }
            }
            return searchedDate;
        }

        public void selectDate(DateTime date)
        {

        }

        private List<DateTime> GetNearestLeapYears(DateTime currentDate)
        {
            List<DateTime> nearestDates = new();
            for (int year = currentDate.Year - 4; year <= currentDate.Year + 4; year++)
            {
                if (DateTime.IsLeapYear(year))
                {
                    nearestDates.Add(DateTime.Parse($"02/29/{year}"));
                }
            }
            return nearestDates;

        }
    }
}
