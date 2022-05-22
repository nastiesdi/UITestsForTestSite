using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenQA.Selenium;
using WebDriverFramework.Util;

namespace WebDriverFramework.WebDriver.Elements
{
    public class Table : BaseElement
    {
        public Table(By locator, string name) : base(locator, name)
        { }

        protected override string GetElementType()
        {
            return "Table";
        }

        public Dictionary<string, int> GetColumnIndex(Type ColumnsEnum, string AllHeadersLocator)
        {
            var allHeaderElements = GetAllCilumnElements(AllHeadersLocator);
            var IndexesDictionary = new Dictionary<string, int>();
            for (int i = 1; i <= allHeaderElements.Count; i++)
            {
                string ColumnText = allHeaderElements[i-1].Text;
                //GetAllItems(ColumnsEnum);
                foreach (Enum item in Enum.GetValues(ColumnsEnum))
                {
                    if (EnumExtensions.GetStringMapping(item) == ColumnText)
                    {
                        IndexesDictionary.Add(ColumnText, i);
                        break;
                    }
                }
            }
            return IndexesDictionary;
        }

        //public static IEnumerable<T> GetAllItems<T>(Enum value)
        //{
        //    IEnumerable<T> t = new();
        //    foreach (object item in Enum.GetValues(typeof(T)))
        //    {
        //        (T)item;
        //    }
        //}

        public ReadOnlyCollection<IWebElement> GetAllCilumnElements(string AllHeadersLocator)
        {
            return Driver.FindElements(By.XPath(AllHeadersLocator));
        }

    }
}
