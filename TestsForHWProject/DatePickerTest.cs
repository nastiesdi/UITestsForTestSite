using System;
using NUnit.Framework;
using PageObjects;
using PageObjects.Enum;
using WebDriverFramework.WebDriver;

namespace TestsForHWProject
{
    [TestFixture]
    public class DatePickerTest : BaseTest
    {
        private MainPage mainPage;
        private string expectedTextFromNewPage = "This is a sample page";

        [SetUp]
        public void SetUp()
        {
            mainPage = new();
        }

        [Test]
        public override void RunTest()
        {
            mainPage.AssertIsOpen();
            LogStep(1, "Click Alerts, Frame and Window button");
            mainPage.ClickOnWidgetsButton();
            WidgetsPage widgetPage = new();
            widgetPage.AssertIsOpen();

            LogStep(2, "Click Button to see Date Picker page");
            widgetPage.ClickNavigationButton(NavigationItems.DatePicker);
            DatePickerPage datePickerPage = new();
            datePickerPage.AssertIsOpen();
            string dateFromPicker = datePickerPage.GetParsedDate().ToString("MM/dd/yyyy");
            string currentDate = DateTime.Now.ToString("MM/dd/yyyy");
            Assert.AreEqual(dateFromPicker, currentDate);

            string timeFromPicker = datePickerPage.GetParsedTime().ToString("HHmm"); ;
            string currentTime = DateTime.Now.ToString("HHmm");
            Assert.AreEqual(timeFromPicker, currentTime);

            LogStep(3, "Verify Dates are equal");
            var searchedDate = datePickerPage.FindClosestDay(DateTime.Now);
            datePickerPage.datePicker.Click();
            datePickerPage.datePicker.SelectDate(searchedDate);
            dateFromPicker = datePickerPage.GetParsedDate().ToString("MM/dd/yyyy");
            Assert.AreEqual(dateFromPicker, searchedDate.ToString("MM/dd/yyyy"));
        }
    }
}