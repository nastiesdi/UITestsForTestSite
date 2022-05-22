using System.Collections.Generic;
using OpenQA.Selenium;

namespace WebDriverFramework.WebDriver.Elements
{
    /// class describes the interface element "label"
    public class Label : BaseElement
    {
        private By by;

        public Label(By locator, string name) : base(locator, name)
        { }

        protected override string GetElementType()
        {
            return "Label";
        }

        /// gets all labels
        /// <param name="locator">locator By of labels</param>
        /// <param name="name">name of labels</param>
        /// <returns>all labels on the page</returns>
        public Label[] GetAllLabels(string locator, string name)
        {
            int count = Browser.GetDriver().FindElements(By.XPath(locator)).Count;
            var results = new Label[count];
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    results[i] = new Label(By.XPath(locator + "[" + (i + 1) + "]"), name + (i + 1));
                }
            }
            return results;
        }

        /// gets all labels
        /// <param name="locator">locator By of labels</param>
        /// <returns>all labels on the page</returns>
        public Label[] GetAllLabels(string locator)
        {

            int count = Browser.GetDriver().FindElements(By.XPath(locator)).Count;
            var results = new Label[count];
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    results[i] = new Label(By.XPath(locator + "[" + (i + 1) + "]"), "");
                }
            }
            return results;
        }

        /// gets all label's text as collection
        public List<string> GetAllLabelsText()
        {
            var elements = Browser.GetDriver().FindElements(Locator);
            var result = new List<string>();
            foreach (var element in elements)
            {
                try
                {
                    result.Add(element.Text);
                }
                catch (StaleElementReferenceException e)
                {
                    Logger.Instance.Info($"StaleElementReferenceException error '{e}'");
                }
            }

            return result;
        }

        /// gets attribute href
        public string GetHref()
        {
            return GetAttribute("href");
        }

        /// gets attribute checked
        public string GetChecked()
        {
            return GetAttribute("checked");
        }
    }
}

