using NUnit.Framework;
using PageObjects;
using PageObjects.Enum;
using PageObjects.Models;
using WebDriverFramework.WebDriver;

namespace TestsForHWProject
{
    [TestFixture]
    public class TableTest : BaseTest
    {
        private MainPage mainPage;
        private readonly UserModel[] testCaseInput =
        {
            new UserModel { FirstName = "Jon", LastName = "Snow", Email = "knownothing@gmail.com", Age = 30, Salary = 3000, Department = "alpha" },
            new UserModel { FirstName = "Buttercup", LastName = "Cumbersnatch",Email = "BudapestCandygram@mail.ru", Age = 41, Salary = 2000, Department = "beta" }
        };

        private UserModel GetObjectForTest(int id)
        {
            if(id ==1)
            {
                return testCaseInput[0];
            }
            return testCaseInput[1];
        }

        [SetUp]
        public void SetUp()
        {
            mainPage = new();
        }

        private static object[] DivideCases()
        {
            var amountOfSamples = 2;
            var result = new object[amountOfSamples];

            for (var i = 0; i < amountOfSamples; i++)
            {
                result[i] = new object[] { i };
            }

            return result;
        }

        [Test, TestCaseSource(nameof(DivideCases))]
        public override void RunTest(int samples)
        {
            mainPage.AssertIsOpen();
            LogStep(1, "Click Elements");
            mainPage.ClickOnElementsButton();
            ElementsPage elementsPage = new();
            elementsPage.AssertIsOpen();

            LogStep(2, "Click Web Tables");
            elementsPage.ClickNavigationButton(NavigationItems.WebTables);

            LogStep(3, "Fill all fields for User and click Submit");
            WebTables webTablesPage = new WebTables();
            webTablesPage.AssertIsOpen();
            webTablesPage.AddButton.Click();
            UserModel objectForTest = GetObjectForTest(samples);
            webTablesPage.SendKeyToField(FormFieldsEnum.firstName, objectForTest.FirstName);
            webTablesPage.SendKeyToField(FormFieldsEnum.lastName, objectForTest.LastName);
            webTablesPage.SendKeyToField(FormFieldsEnum.userEmail, objectForTest.Email);
            webTablesPage.SendKeyToField(FormFieldsEnum.age, objectForTest.Age);
            webTablesPage.SendKeyToField(FormFieldsEnum.salary, objectForTest.Salary);
            webTablesPage.SendKeyToField(FormFieldsEnum.department, objectForTest.Department);
            webTablesPage.SubmitButton.Click();
            var AllUsers = webTablesPage.GetAllUsers();
            Assert.IsTrue(webTablesPage.UserIsInList(objectForTest, AllUsers));

            LogStep(4, "Delete user");
            webTablesPage.DeleteRecord(4);
            AllUsers = webTablesPage.GetAllUsers();
            Assert.IsFalse(webTablesPage.UserIsInList(objectForTest, AllUsers));
        }
    }
}
