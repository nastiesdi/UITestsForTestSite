using OpenQA.Selenium;

namespace WebDriverFramework.WebDriver.Elements
{
    /// class describes the interface element "link"
    public class Link : BaseElement
    {
        /// constructor with two parameters
        /// <param name="locator">locator By of the link</param>
        /// <param name="name">name of the link</param>
        public Link(By locator, string name) : base(locator, name)
        { }

        /// gets the type of the link 
        /// <returns>type of the link</returns>
        protected override string GetElementType()
        {
            return "Link";
        }
    }
}
