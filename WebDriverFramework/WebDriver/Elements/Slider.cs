using System;
using OpenQA.Selenium;

namespace WebDriverFramework.WebDriver.Elements
{
    public class Slider : BaseElement
    {
        public Slider(By locator, string name) : base(locator, name)
        { }

        protected override string GetElementType()
        {
            return "Slider";
        }

        public void SlideToLeft()
        {
            var element = GetElement();
            element.SendKeys(Keys.ArrowLeft);
        }

        public void SlideToRight()
        {
            var element = GetElement();
            element.SendKeys(Keys.ArrowRight);
        }
    }
}