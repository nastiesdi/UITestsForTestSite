using System;
using OpenQA.Selenium;
using PageObjects.Enum;
using WebDriverFramework.Util;
using WebDriverFramework.WebDriver.Elements;

namespace PageObjects
{
    public class SliderPage : NavigationPage
    {
        private static readonly By titleLocator = By.XPath("//div[contains(@class, 'main-header')][contains(text(), 'Slider')]");

        private readonly TextField SliderInputTextBox = new TextField(By.Id("sliderValue"), "Slider text box");
        private readonly Slider SliderBar = new Slider(By.XPath("//input[contains(@class, 'slider--primary')]"), "Slider bar");

        public SliderPage() : base(titleLocator, "Web Tables")
        {
        }

        public int SlideForRundomValue()
        {
            var randomValue = RandomValue(1, 100);
            var currentValue = int.Parse(GetValueFromSlider());
            if (randomValue > currentValue)
            {
                while (!(randomValue == int.Parse(GetValueFromSlider())))
                {
                    SlideToRight();
                }
            }
            else if (randomValue < currentValue)
            {
                while (!(randomValue == int.Parse(GetValueFromSlider())))
                {
                    SlideToLeft();
                }
            }

            return randomValue;
        }

        public string GetValueFromSlider()
        {
            return SliderInputTextBox.GetValue();
        }

        public void SlideToLeft()
        {
            SliderBar.SlideToLeft();
        }

        public void SlideToRight()
        {
            SliderBar.SlideToRight();
        }
    }
}

