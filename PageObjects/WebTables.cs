using System.Collections.Generic;
using OpenQA.Selenium;
using PageObjects.Enum;
using PageObjects.Models;
using WebDriverFramework.Util;
using WebDriverFramework.WebDriver;
using WebDriverFramework.WebDriver.Elements;

namespace PageObjects
{
    public class WebTables : AllertsFrameAndWindowsPage
    {
        private static readonly By titleLocator = By.XPath(string.Format(parrentLocator, EnumExtensions.GetStringMapping(NavigationItems.WebTables)));
        public readonly Button AddButton = new Button(By.Id("addNewRecordButton"), "Add button");
        public readonly Button SubmitButton = new Button(By.Id("submit"), "Submot button");
        private readonly string AllHeadersLocator = "//div[contains(@role, 'columnheader')]";
        private string cellLocator = "//div[contains(@class, 'rt-tbody')]/div[{0}]//div[contains(@role, 'gridcell')][{1}]";
        private string deleteIcon = "//div[contains(@class, 'rt-tbody')]/div[{0}]//span[contains(@title, 'Delete')][1]";
        private string allrows = "//div[contains(@role, 'rowgroup')]/div[contains(@role, 'row') and not(contains(@class, 'padRow'))]";
        private Dictionary<string, int> allColumn;

        public WebTables() : base(titleLocator)
        {
        }

        public void SendKeyToField(FormFieldsEnum field, string value)
        {
            TextField textField = new TextField(By.Id(field.ToString()), "text field");
            textField.SendKeys(value);
        }

        public void SendKeyToField(FormFieldsEnum field, int value)
        {
            TextField textField = new TextField(By.Id(field.ToString()), "text field");
            textField.SendKeys(value.ToString());
        }
        
        public void DeleteRecord(int row)
        {
            var delIcon = new Button(By.XPath(string.Format(deleteIcon, row.ToString())), "Delete icon");
            delIcon.Click();
        }

        public int GetRowsCount()
        {
            return Driver.FindElements(By.XPath(allrows)).Count;
        }

        public Dictionary<string, int> GetAllColumnIndexes()
        {
            var newTable = new Table(By.XPath(AllHeadersLocator), "Table");
            return newTable.GetColumnIndex(typeof(FormFieldsEnum), AllHeadersLocator);
        }

        public Label GetCell(int column, int row)
        {
            var cell = new Label(By.XPath(string.Format(cellLocator, row , column)), "Cell is table");
            return cell;
        }

        public List<UserModel> GetAllUsers()
        {
            var listOfModels = new List<UserModel>();
            var rowsQty = GetRowsCount();
            for (int i = 1; i <= rowsQty; i++)
            {
                listOfModels.Add(GetUserModelByRowOrder(i));
            }

            return listOfModels;
        }

        public UserModel GetUserModelByRowOrder(int rowOrder)
        {
            var allColumnIndexes = this.GetAllColumnIndexes();
            var userModel = new UserModel()
            {
                FirstName = this.GetCell(allColumnIndexes[EnumExtensions.GetStringMapping(FormFieldsEnum.firstName)], rowOrder).GetText(),
                LastName = this.GetCell(allColumnIndexes[EnumExtensions.GetStringMapping(FormFieldsEnum.lastName)], rowOrder).GetText(),
                Email = this.GetCell(allColumnIndexes[EnumExtensions.GetStringMapping(FormFieldsEnum.userEmail)], rowOrder).GetText(),
                Age = int.Parse(this.GetCell(allColumnIndexes[EnumExtensions.GetStringMapping(FormFieldsEnum.age)], rowOrder).GetText()),
                Salary = int.Parse(this.GetCell(allColumnIndexes[EnumExtensions.GetStringMapping(FormFieldsEnum.salary)], rowOrder).GetText()),
                Department = this.GetCell(allColumnIndexes[EnumExtensions.GetStringMapping(FormFieldsEnum.department)], rowOrder).GetText()
            };

            return userModel;
        }

        public bool UserIsInList(UserModel User, List<UserModel> listOfUsers)
        {
            foreach (var item in listOfUsers)
            {
                if (User.Equals(item))
                {
                    return true;
                }
            }
            return false;
        }


    }

}
