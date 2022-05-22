using NUnit.Framework;
using PageObjects;
using PageObjects.Enum;
using WebDriverFramework.WebDriver;

namespace TestsForHWProject
{
    [TestFixture]
    public class SlidersProgressTest : BaseTest
    {
        private MainPage mainPage;

        [SetUp]
        public void SetUp()
        {
            mainPage = new();
        }

        [Test]
        public override void RunTest()
        {
            mainPage.AssertIsOpen();
            LogStep(1, "Click Browser Windows page");
            mainPage.ClickOnWidgetsButton();
            WidgetsPage widgetPage = new();
            widgetPage.AssertIsOpen();

            LogStep(2, "Click Button to see Browser Windows page");
            widgetPage.ClickNavigationButton(NavigationItems.Slider);
            SliderPage sliderPage = new();
            sliderPage.AssertIsOpen();

            LogStep(3, "Set slider to random value Slider");
            int randomValue = sliderPage.SlideForRundomValue();
            Assert.AreEqual(sliderPage.GetValueFromSlider(), randomValue.ToString());
        }
    }
}


//Test case 5.Slider, Progress bar

//1	Перейти на главную страницу	
//Главная страница открыта

//2	Кликнуть на кнопку Widgets. В левом меню выбрать пункт Slider	
//Открыта страница с формой Slider

//3	Установить слайдеру корректное случайно сгенерированное значение	
//Значение рядом со слайдером соответствует случайно сгенерированному

//4	В левом меню выбрать пункт Progress Bar	
//Открыта страница с формой Progress Bar

//5	Нажать на кнопку Start.
//6	Нажать на кнопку Stop, когда на полосе загрузки появится значение, равное возрасту инженера, выполняющего задание	
//Значение на полосе загрузке соответствует возрасту инженера (погрешность не превышает 2 %)