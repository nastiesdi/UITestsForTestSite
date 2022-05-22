using System;
using OpenQA.Selenium;

namespace WebDriverFramework.WebDriver.Elements
{
    /// class describes the interface element "button"
    public class Button : BaseElement
    {
        /// constructor with two parameters
        /// <param name="locator">locator By of the button</param>
        /// <param name="name">name of the button</param>
        public Button(By locator, string name) : base(locator, name)
        { }

        /// gets the type of the button 
        /// <returns>type of the button</returns>
        protected override string GetElementType()
        {
            return "Button";
        }
    }
}
