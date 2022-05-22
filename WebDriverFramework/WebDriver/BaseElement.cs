using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace WebDriverFramework.WebDriver
{
    /// Abstract class for any elements in the application
    /// All classes that described elements such as Button, TextBox and etc.
    /// should inherit of this class
    public abstract class BaseElement : BaseEntity
    {
        /// Name of elements (usually used for logging)
        protected readonly string Name;

        /// locator to element
        protected By Locator;

        /// IWebElement instance
        protected IWebElement Element;

        /// Timeout ToDelay Char Typing in Ms
        private const int TimeoutToDelayCharMs = 35;

        /// constructor with two parameters
        protected BaseElement(By locator, string name)
        {
            Locator = locator;
            Name = name == "" ? GetText() : name;
        }

        /// return IWeb-Element
        public IWebElement GetElement()
        {
            try
            {
                Element = Browser.GetDriver().FindElement(Locator);
            }
            catch (NoSuchElementException)
            {
                Assert.Fail("Element not found");
            }
            return Element;
        }

        /// returns elements, currently displayed on page
        public List<IWebElement> GetDisplayedElements(By locator)
        {
            var resultList = new List<IWebElement>();
            try
            {
                var wait = new WebDriverWait(Browser.GetDriver(), TimeSpan.FromSeconds(Browser.TimeoutForElementWaiting));
                IWebElement element;
                element = wait.Until(d =>
                {
                    element = d.FindElement(locator);
                    return element.Displayed ? element : null;
                });
                resultList.AddRange(Browser.GetDriver().FindElements(locator).Where(e => e.Displayed));
            }
            catch (TimeoutException ex)
            {
                Browser.GetDriver().Navigate().Refresh();
                if (resultList.Count == 0)
                {
                    Logger.Instance.Fail(ex.Message + "\n" + ("Elements not found; timeout extended"));
                }
            }
            catch (NoSuchElementException)
            {
                Assert.Fail("Elements not found");
            }
            return resultList;
        }

        /// get locator By
        public By GetLocator()
        {
            return Locator;
        }

        /// method gets the name of element
        public string GetName()
        {
            return Name;
        }

        /// formats the value for logging "element type - name - log splitter - the message"
        protected override string FormatLogMsg(string message)
        {
            return $"{GetElementType()} '{Name}' {Logger.LogDelimiter} {message}";
        }

        /// assertion of the presence of the element on the page
        public void AssertIsPresent()
        {
            if (!IsPresent())
            {
                Log.Fail(FormatLogMsg("is not present"));
            }
            else
            {
                Log.Info(FormatLogMsg("is present"));
            }
        }

        ///  wait until element is absent
        public bool IsAbsent()
        {
            return IsAbsent(5);
        }

        ///  wait until element is absent
        public bool IsAbsent(int seconds)
        {
            var wait = new WebDriverWait(Browser.GetDriver(), TimeSpan.FromSeconds(Convert.ToDouble(seconds)));
            try
            {
                wait.Until(waiting =>
                {
                    var elements = Browser.GetDriver().FindElements(Locator);
                    Browser.Refresh();
                    Browser.WaitForPageToLoad();
                    if (elements.Count == 0)
                    {
                        return true;
                    }
                    return false;
                });
            }
            catch (WebDriverTimeoutException e)
            {
                Logger.Instance.Debug("Element is present: " + GetLocator() + "\r\n" + e.StackTrace);
                return false;
            }
            return true;
        }

        ///  assertion of the absence of the element on the page
        public void AssertIsAbsent()
        {
            if (IsPresent())
            {
                Log.Fail(FormatLogMsg("is present"));
            }
            else
            {
                Log.Info(FormatLogMsg("is absent"));
            }
        }

        ///  assertion of the disable element on the page
        public void AssertIsDisable()
        {
            if (!IsDisabled())
            {
                Log.Fail(FormatLogMsg("is enabled"));
            }
            else
            {
                Log.Info(FormatLogMsg("is disabled"));
            }
        }

        /// check that the element is enabled (performed by a class member)
        public bool IsEnabled()
        {
            return !GetClass().ToLower().Contains("disabled") && Element.Enabled;
        }

        /// workaround for AJAX 
        /// <returns>element if element is enabled else null</returns>
        public void WaitForIsEnabled()
        {
            try
            {
                var wait = new WebDriverWait(Browser.GetDriver(), TimeSpan.FromSeconds(Browser.TimeoutForElementWaiting));

                Element = wait.Until(d =>
                {
                    try
                    {
                        Element = d.FindElement(Locator);
                        return Element.Enabled ? Element : null;
                    }
                    catch (NoSuchElementException)
                    {
                        return null;
                    }
                    catch (InvalidElementStateException)
                    {
                        return null;
                    }
                    catch (StaleElementReferenceException e)
                    {
                        Logger.Instance.Debug(e.Message + "\n" + FormatLogMsg(" stale element"));
                        return null;
                    }
                });
            }
            catch (TimeoutException ex)
            {
                Logger.Instance.Fail(ex.Message + "\n" + FormatLogMsg(" is not enabled"));
            }
            catch (Exception exec)
            {
                Logger.Instance.Fail(exec.Message + "\n" + FormatLogMsg(" bad situation"));
            }
        }

        /// workaround for AJAX
        /// <returns>element if element is enabled else null</returns>
        public void WaitForIsDisabled()
        {
            try
            {
                var wait = new WebDriverWait(Browser.GetDriver(), TimeSpan.FromSeconds(Browser.TimeoutForElementWaiting));
                Element = wait.Until(d =>
                {
                    Element = d.FindElement(Locator);
                    return Element.Enabled ? null : Element;
                });
            }
            catch (TimeoutException ex)
            {
                Logger.Instance.Fail(ex.Message + "\n" + FormatLogMsg(" is not Disabled"));
            }
        }

        /// wait until element is presence
        /// <returns>element if element is displayed else null</returns>
        public void WaitForElementIsPresent()
        {
            WaitForElementIsPresent(1);
        }

        /// wait until element is presence
        /// <returns>element if element is displayed else null</returns>
        public void WaitForElementIsPresent(int multyplyerForBrowserTimeout)
        {
            try
            {
                var wait = new WebDriverWait(Browser.GetDriver(), Browser.GetElementTimeoutInSeconds(multyplyerForBrowserTimeout));
                Element = wait.Until(d =>
                {
                    try
                    {
                        try
                        {
                            var elements = d.FindElements(Locator);
                            return elements.FirstOrDefault(webElement => webElement.Displayed);
                        }
                        catch (NoSuchElementException)
                        {
                            return null;
                        }
                    }
                    catch (StaleElementReferenceException e)
                    {
                        Logger.Instance.Debug(e.StackTrace);
                        return null;
                    }
                });
            }
            catch (TimeoutException ex)
            {
                Browser.GetDriver().Navigate().Refresh();
                if (!IsPresent())
                {
                    Logger.Instance.Fail(ex.Message + "\n" + FormatLogMsg(" is not Present" + " by locator: " + Locator));
                }
            }
            catch (WebDriverTimeoutException ex)
            {
                if (!IsPresent())
                {
                    Logger.Instance.Fail(ex.Message + "\n" + FormatLogMsg(" is not Present" + " by locator: " + Locator));
                }
            }
        }

        /// wait until element exists
        /// initialize Element var if element exist
        public void WaitForElementExists()
        {
            try
            {
                var wait = new WebDriverWait(Browser.GetDriver(), TimeSpan.FromSeconds(Browser.TimeoutForElementWaiting));
                Element = wait.Until(d =>
                {
                    try
                    {
                        var elements = d.FindElements(Locator);
                        var count = elements.Count;
                        if (count > 0)
                        {
                            Logger.Instance.Debug(count + " elements was found by locator " + GetLocator());
                            Element = elements[0];
                            return Element;
                        }
                        return null;
                    }
                    catch (StaleElementReferenceException e)
                    {
                        Logger.Instance.Debug(e.StackTrace);
                        return null;
                    }
                });
            }
            catch (WebDriverTimeoutException ex)
            {
                if (!IsPresent())
                {
                    Logger.Instance.Fail(ex.Message + "\n" + FormatLogMsg(" is not Exists" + " by locator: " + Locator));
                }
            }
        }

        ///  wait until element is presence updating each time the page
        /// <returns>element if element is displayed else null</returns>
        public void WaitForIsElementPresentReloadingPage(string url)
        {
            try
            {
                var wait = new WebDriverWait(Browser.GetDriver(), TimeSpan.FromSeconds(Browser.TimeoutForElementWaiting));
                Element = wait.Until(d =>
                {
                    Browser.NavigateTo(url);
                    Element = d.FindElement(Locator);
                    return Element.Displayed ? Element : null;
                });
            }
            catch (TimeoutException ex)
            {
                Browser.GetDriver().Navigate().Refresh();
                if (!IsPresent())
                {
                    Logger.Instance.Fail(ex.Message + "\n" + FormatLogMsg(" is not Present"));
                }
            }
        }

        /// wait until element is absent
        public void WaitForIsAbsent()
        {
            try
            {
                if (IsPresent(3))
                {
                    var wait = new WebDriverWait(Browser.GetDriver(),
                        TimeSpan.FromSeconds(Browser.TimeoutForElementWaiting));
                    wait.Until(d => (Browser.GetDriver().FindElements(Locator).Count <= 0));
                }
            }
            catch (StaleElementReferenceException)
            {
                Log.Info(FormatLogMsg(" is already absent"));
            }
            catch (WebDriverException)
            {
                Log.Info(FormatLogMsg(" is already absent"));
            }
            catch (InvalidOperationException)
            {
                Log.Info(FormatLogMsg(" is already absent"));
            }
        }

        /// wait until element present and get it
        /// initialize Element var if element present
        /// <returns>IWeb-Element</returns>
        public IWebElement WaitForElementIsPresentAndGetIt()
        {
            WaitForElementIsPresent();
            return Element;
        }

        /// wait until element exists and get it
        /// initialize Element var if element exist
        /// <returns>IWeb-Element</returns>
        public IWebElement WaitForElementExistsAndGetIt()
        {
            WaitForElementExists();
            return Element;
        }

        /// checks the presence of an element on the page
        public bool IsPresent()
        {
            return IsPresent(5);
        }

        /// check if element exists on the page
        public bool IsExists()
        {
            return IsExists(5);
        }

        /// check if element active on the page by finding 'active' in class attribute
        public bool IsActive()
        {
            return GetClass().Contains("active");
        }

        /// get value of data-index attribute of the element
        public string GetDataIndex()
        {
            return GetAttribute("data-index");
        }

        /// gets attribute disabled
        /// <returns>attribute disabled of the element. Empty string if attribute is absent</returns>
        public string GetDisabledAttribute()
        {
            return GetAttribute("disabled") ?? String.Empty;
        }

        /// gets attribute disabled
        /// <returns>attribute disabled of the element. Empty string if attribute is absent</returns>
        public string GetDisabledAttributeInvisible()
        {
            return GetAttributeInvisible("disabled") ?? String.Empty;
        }

        /// gets Css Value Cursor
        /// <returns>Cursor css value for enabling action. Empty string if attribute is absent</returns>
        public string GetCssValueCursor()
        {
            return WaitForElementIsPresentAndGetIt().GetCssValue("cursor") ?? String.Empty;
        }

        /// check if element active on the page by finding 'checked' in class attribute
        public bool IsChecked()
        {
            return GetClass().Contains("checked");
        }

        /// check if element active on the page by finding 'selected' in class attribute
        public bool IsSelected()
        {
            return GetClass().Contains("selected");
        }


        /// check for is element present on the page
        /// <param name="sec">count of seconds to wait for the absence of an element on the page</param>
        /// <returns>true if element is present</returns>
        public bool IsPresent(int sec)
        {
            var wait = new WebDriverWait(Browser.GetDriver(), TimeSpan.FromSeconds(Convert.ToDouble(sec)));
            try
            {
                wait.Until(waiting =>
                {
                    var elements = Browser.GetDriver().FindElements(Locator);
                    var result = false;
                    try
                    {
                        foreach (var webElement in elements.Where(webElement => webElement.Displayed))
                        {
                            Element = webElement;
                            result = true;
                        }
                    }
                    catch (StaleElementReferenceException)
                    {
                        result = false;
                    }
                    return result;
                });
            }
            catch (WebDriverTimeoutException e)
            {
                Logger.Instance.Debug("Element is not present: " + GetLocator() + "\r\n" + e.StackTrace);
                return false;
            }
            return true;
        }

        /// check for is element exists on the page
        /// <param name="sec">wait in seconds until element is not exists</param>
        /// <returns>true if exists</returns>
        public bool IsExists(int sec)
        {
            var wait = new WebDriverWait(Browser.GetDriver(), TimeSpan.FromSeconds(Convert.ToDouble(sec)));
            try
            {
                wait.Until(waiting =>
                {
                    var elements = Browser.GetDriver().FindElements(Locator);
                    if (elements.Count > 0)
                    {
                        Element = elements[0];
                        return true;
                    }
                    return false;
                });
            }
            catch (WebDriverTimeoutException e)
            {
                Logger.Instance.Debug("Element is not exists: " + GetLocator() + "\r\n" + e.StackTrace);
                return false;

            }
            return true;
        }


        /// check for is element disabled on the page
        public bool IsDisabled()
        {
            try
            {
                Element = Browser.GetDriver().FindElement(Locator);
            }
            catch (Exception)
            {
                Log.Info(FormatLogMsg(" is not present"));
                return false;
            }
            return !Element.Enabled;
        }

        /// click on the element
        public void Click()
        {
            WaitForElementIsPresent();
            Info("Clicking");
            try
            {
                Element.Click();
            }
            catch (InvalidOperationException ex)
            {
                Warn(ex.Message);
                Fatal(" is not available for click ");
            }
        }

        ///click on an item and wait for the page is loaded
        public void ClickAndWaitForLoading()
        {
            try
            {
                Info("Perform click and wait for page to load");
                Click();
                Browser.WaitForPageToLoad();
            }
            catch (WebDriverException exc)
            {
                Info("An exception accured while we were trying to click by " + Name + "One attemp yet...\r\n" + exc.Message);
                WaitForElementIsPresent();
                Info("Perform click and wait for page to load");
                Click();
                Browser.WaitForPageToLoad();
            }
        }

        /// extended click through Enter
        public void ClickExt()
        {
            WaitForElementIsPresent();
            Info("extended Clicking");
            Browser.GetDriver().FindElement(Locator).SendKeys(Keys.Enter);
        }

        /// returns count of elements using findElements method of selenium
        public int GetElementsCount(By locator)
        {
            Browser.WaitForPageToLoad();
            return Browser.GetDriver().FindElements(locator).Count;
        }

        /// click on an item ext click through Enter and wait for the page is loaded.
        public void ClickExtAndWait()
        {
            try
            {
                ClickExt();
                Browser.WaitForPageToLoad();
            }
            catch (WebDriverException exc)
            {
                Warn(exc.Message);
                Browser.GetDriver().Navigate().Refresh();
                Browser.GetDriver().FindElement(By.XPath("//")).SendKeys(Keys.Enter);

                ClickExt();
                Browser.WaitForPageToLoad();
            }
        }


        /// click via Action.
        public void ClickViaAction()
        {
            WaitForElementIsPresent();
            AssertIsPresent();
            ClickViaActionWithoutWaiting();
        }

        /// click via Action.
        public void ClickViaActionWithoutWaiting()
        {
            Info("Clicking");
            var action = new Actions(Browser.GetDriver());
            action.Click(GetElement());
            action.Perform();
        }

        /// click via JS.
        public void ClickViaJs()
        {
            WaitForElementIsPresent();
            Info("Clicking");
            ((IJavaScriptExecutor)Browser.GetDriver()).ExecuteScript("arguments[0].click();", GetElement());
        }

        /// click via JS.
        public void FocusViaJs()
        {
            WaitForElementIsPresent();
            Info("Clicking");
            ((IJavaScriptExecutor)Browser.GetDriver()).ExecuteScript("arguments[0].focus();", GetElement());
        }

        /// scroll element into view
        public void ScrollIntoView()
        {
            WaitForElementIsPresent();
            Info("Scrolling into view");
            ((IJavaScriptExecutor)Browser.GetDriver()).ExecuteScript("arguments[0].scrollIntoView(true);", GetElement());
        }

        /// scroll element into view with center aligning
        public void ScrollIntoViewWithCenterAligning()
        {
            WaitForElementIsPresent();
            Info("Scrolling into view");
            ((IJavaScriptExecutor)Browser.GetDriver()).ExecuteScript("arguments[0].scrollIntoView({block: 'center', inline: 'center'});", GetElement());
        }

        /// scroll element to right by Right Arrow Key
        public void ScrollHorizontalToEndFromKeyboard()
        {
            WaitForElementIsPresent();
            Info("Scrolling to the right end sending Keys.Right");
            var currentXcoordinate = Element.Location.X;
            int previousXcoordinate;
            do
            {
                new Actions(Browser.GetDriver()).MoveToElement(Element).SendKeys(Keys.Right).Perform();
                previousXcoordinate = currentXcoordinate;
                currentXcoordinate = Element.Location.X;
            } while (previousXcoordinate != currentXcoordinate);
        }

        /// scroll element to left by Left Arrow Key
        public void ScrollHorizontalToBeginFromKeyboard()
        {
            WaitForElementIsPresent();
            Info("Scrolling to the left end sending Keys.Left");
            var currentXcoordinate = Element.Location.X;
            int previousXcoordinate;
            do
            {
                new Actions(Browser.GetDriver()).MoveToElement(Element).SendKeys(Keys.Left).Perform();
                previousXcoordinate = currentXcoordinate;
                currentXcoordinate = Element.Location.X;
            } while (previousXcoordinate != currentXcoordinate);
        }

        /// click via JS.
        public void ClickInvisible()
        {
            WaitForElementExists();
            Info("Clicking");
            ((IJavaScriptExecutor)Browser.GetDriver()).ExecuteScript("arguments[0].click();", GetElement());
        }

        /// click on an item js click and wait for the page is loaded.
        public void ClickViaJsAndWait()
        {
            try
            {
                ClickViaJs();
                Browser.WaitForPageToLoad();
            }
            catch (WebDriverException exc)
            {
                Logger.Instance.Debug(exc.Message);
                Browser.GetDriver().Navigate().Refresh();
                Browser.GetDriver().FindElement(By.XPath("//")).SendKeys(Keys.Enter);
                ClickViaJs();
                Browser.WaitForPageToLoad();
            }
        }

        /// move the cursor to the element and click him
        public void ClickWithMouseOver()
        {
            WaitForElementIsPresent();
            Info("Clicking with mouse over");
            new Actions(Browser.GetDriver()).MoveToElement(Element).Click(Element).Perform();
        }

        /// click and look forward to the emergence of a new window
        public void ClickAndWaitForNewWindow()
        {
            int count = Browser.WindowCount();
            Click();
            Info("Select next window");
            Browser.WaitForNewWindow(count);
            Browser.SwitchWindow(count);
            Browser.WindowMaximise();
        }

        /// click and look forward to closing the current window
        public void ClickAndWaitForWindowClose()
        {
            int count = Browser.WindowCount();
            Click();
            Info("Select previous window");
            Browser.WaitForNewWindow(count - 2);
            Browser.SwitchWindow(count - 2);
        }

        /// double click
        public void DoubleClick()
        {
            WaitForElementIsPresent();
            Info("Double clicking");
            new Actions(Browser.GetDriver()).DoubleClick(Element).Perform();
        }

        /// send keys
        public void SendKeys(string key)
        {
            Info($"Typing '{key}'");
            WaitForElementIsPresent();
            Browser.GetDriver().FindElement(Locator).SendKeys(key);
        }

        /// send keys without element waiting
        public void SendKeysWithoutPresent(string key)
        {
            Info($"Typing '{key}'");
            Browser.GetDriver().FindElement(Locator).SendKeys(key);
        }

        /// sets the value of the title attribute
        public void SetAttribute(string attName, string attValue)
        {
            GetScriptExecutor().ExecuteScript("arguments[0].setAttribute(arguments[1], arguments[2]);", Element, attName, attValue);
        }

        public IJavaScriptExecutor GetScriptExecutor()
        {
            var driver = ((IWrapsDriver)Element).WrappedDriver;
            var executor = (IJavaScriptExecutor)driver;
            return executor;
        }

        /// gets the value of the class attribute
        public string GetClass()
        {
            return GetAttribute("class");
        }

        /// gets the value of the class attribute
        public string GetClassInvisible()
        {
            return GetAttributeInvisible("class");
        }

        /// gets the value of the xlink attribute
        public string GetXlink()
        {
            return GetAttribute("xlink:href");
        }

        /// gets the value of the xlink attribute
        public string GetXlinkInvisible()
        {
            return GetAttributeInvisible("xlink:href");
        }

        /// gets the value of the class attribute aria-invalid
        public string GetAriaInvalidAttribute()
        {
            return GetAttribute("aria-invalid");
        }

        /// gets the value of the class attribute aria-label
        public string GetAriaLabelAttribute()
        {
            return GetAttribute("aria-label");
        }

        /// gets the value of the title attribute
        public string GetAttribute(string attr)
        {
            return WaitForElementIsPresentAndGetIt().GetAttribute(attr);
        }

        /// gets the CSS value of the property
        public string GetCssValue(string propertyName)
        {
            return WaitForElementIsPresentAndGetIt().GetCssValue(propertyName);
        }

        /// get attribute value from element that is not displayed
        public string GetAttributeInvisible(string attr)
        {
            return WaitForElementExistsAndGetIt().GetAttribute(attr);
        }

        /// gets the value
        public string GetValue()
        {
            return GetAttribute("value");
        }

        /// gets the value
        public string GetValueInvisible()
        {
            return WaitForElementExistsAndGetIt().GetAttribute("value");
        }

        /// get the text of the element
        public string GetText()
        {
            return WaitForElementIsPresentAndGetIt().Text;
        }

        /// get the text of the element Without Waiting
        public string GetTextWithoutWaiting()
        {
            return Element.Text;
        }

        /// get the textContent of the element
        public string GetTextContent()
        {
            return GetAttribute("textContent");
        }

        /// get the border color of the element by CSS value
        public string GetBorderColorByCssValue()
        {
            return GetCssValue("border-color");
        }

        /// get the border Top color of the element by CSS value
        public string GetBorderTopColorByCssValue()
        {
            return GetCssValue("border-top-color");
        }

        /// get the title of the element
        public string GetTitleAttrubute()
        {
            return GetAttribute("title");
        }

        /// get the style of the element
        public string GetStyleAttrubute()
        {
            return GetAttribute("style");
        }

        /// get the sortvalue of the element
        public string GetSortValueAttrubute()
        {
            return GetAttribute("sortvalue");
        }

        /// get the color of the element by CSS value
        public string GetColorByCssValue()
        {
            return GetCssValue("color");
        }

        /// get the padding-left value of the element by CSS value
        public string GetPaddingLeftByCssValue()
        {
            return GetCssValue("padding-left");
        }

        /// focus on the element and send key ""
        public void FocusWithKeys()
        {
            Focus();
            try
            {
                Element.SendKeys("");
            }
            catch (Exception)
            {
                Info("Focused");
            }
        }

        /// focuses the element
        public void Focus()
        {
            Info("Focusing");
            WaitForElementIsPresent();
            new Actions(Browser.GetDriver()).MoveToElement(Element).Build().Perform();
        }

        /// focuses the element with mouse shift
        public void FocusWithMouseShift(int pixelX, int pixelY)
        {
            Info("Focusing");
            WaitForElementIsPresent();
            new Actions(Browser.GetDriver()).MoveToElement(Element, pixelX, pixelY).Build().Perform();
        }

        /// move the cursor to the element 
        public void MouseOver()
        {
            WaitForElementIsPresent();
            Info("Mouse over");
            new Actions(Browser.GetDriver()).MoveToElement(Element).Perform();
        }

        /// scroll the page
        public void ScrollThePage(int x, int y)
        {
            ((IJavaScriptExecutor)Browser.GetDriver()).ExecuteScript("window.scrollBy(" + x + "," + y + ");");
        }

        /// scroll the element to the position
        public void ScrollTo(int x, int y)
        {
            WaitForElementIsPresent();
            Info("Scrolling into view");
            ((IJavaScriptExecutor)Browser.GetDriver()).ExecuteScript("arguments[0].scrollTo(" + x + "," + y + ");", GetElement());
        }

        /// abstract method for get the type of the element 
        protected abstract string GetElementType();

        /// right click
        public void ClickRight()
        {
            WaitForElementIsPresent();
            Info("Right clicking");
            var action = new Actions(Browser.GetDriver());
            action.ContextClick(Element);
            action.Perform();
        }

        /// verify that the drop-down element is minimized (performed by a class member)
        public bool IsCollapsed()
        {
            return GetClass().ToLower().Contains("collapse");
        }

        /// set value via javascript <b>document.getElementById('{0}').value='{1}' </b>
        public void SetValueViaJs(string elementId, string value)
        {
            try
            {
                ((IJavaScriptExecutor)Browser.GetDriver()).ExecuteScript(
                    $"document.getElementById('{elementId}').value='{value}'", Element);
            }
            catch (Exception r)
            {
                Logger.Instance.Warn(r.Message);
            }
        }

        /// set innerHtml via javascript <b>arguments[0].innerHTML='{0}' </b>
        public void SetInnerHtml(string value)
        {
            WaitForElementIsPresent();
            AssertIsPresent();
            Element.Click();
            Info("Ввод текста '" + value + "'");

            ((IJavaScriptExecutor)Browser.GetDriver()).ExecuteScript("arguments[0].innerHTML=\"\";", Element);
            ((IJavaScriptExecutor)Browser.GetDriver()).ExecuteScript("arguments[0].innerHTML=\"" + value + "\";", Element);
        }

        /// set value via javascript <b>arguments[0].value='{0}' </b>
        public void SetValueViaJs(string value)
        {
            WaitForElementIsPresent();
            Element.Click();
            Info("Ввод текста '" + value + "'");

            ((IJavaScriptExecutor)Browser.GetDriver()).ExecuteScript("arguments[0].value=\"\";", Element);

            ((IJavaScriptExecutor)Browser.GetDriver()).ExecuteScript("arguments[0].value=\"" + value + "\";", Element);
        }

        /// enum to set expected condition for explicit wait
        public enum ExpectedConditions
        {
            /// condition when element exists in the html source code
            ElementExists,
            /// condition when element exists in the html source code and visible now
            ElementIsVisible
        }

        /// set explicit wait
        //public void ExplicitWait(ExpectedConditions condition, int seconds)
        //{
        //    try
        //    {
        //        var wait = new WebDriverWait(Browser.GetDriver(), TimeSpan.FromSeconds(seconds));
        //        if (condition.ToString().Equals("ElementExists", StringComparison.OrdinalIgnoreCase))
        //        {
        //            Element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(Locator));
        //        }
        //        else if (condition.ToString().Equals("ElementIsVisible", StringComparison.OrdinalIgnoreCase))
        //        {
        //            Element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(Locator));
        //        }
        //        else
        //        {
        //            Logger.Instance.Info($"Unexpected ExpectedConditions value: {condition}");
        //        }
        //    }
        //    catch (TimeoutException ex)
        //    {
        //        Browser.GetDriver().Navigate().Refresh();
        //        if (!IsPresent())
        //        {
        //            Logger.Instance.Fail(ex.Message + "\n" + FormatLogMsg(" is not Present"));
        //        }
        //    }
        //}

        /// select all text by Ctrl+A
        public void SelectAllTextByCtrlA()
        {
            WaitForElementIsPresentAndGetIt().SendKeys(Convert.ToString('\u0001'));
        }

        /// clear field by selecting all chars and pressing Delete
        public void ClearFieldBySelectingAndDeleting()
        {
            WaitForElementIsPresent();
            SelectAllTextByCtrlA();
            Element.SendKeys(Keys.Delete);
        }

        /// clear field by pressing Backspace
        public void ClearFieldByCharsDeleting(int stringLength)
        {
            WaitForElementIsPresent();
            for (int i = 0; i < stringLength; i++)
            {
                Element.SendKeys(Keys.Backspace);
            }
        }

        /// clear field by selecting all chars and pressing Delete and pressing Backspace
        public void ClearFieldBySelectingPressingDelete(string text)
        {
            if (Browser.CurrentBrowser == "Firefox")
            {
                new Actions(Browser.GetDriver()).MoveToElement(GetElement()).DoubleClick().Perform();
                Browser.Sleep(TimeoutToDelayCharMs);
            }
            else
            {
                Browser.Sleep(TimeoutToDelayCharMs);
                ClearFieldBySelectingAndDeleting();
            }
            ClearFieldByCharsDeleting(2 * text.Length);
        }

        /// clear field and set text with delay
        /// <param name="text">text for set</param>
        public void SetTextWithDelayInCharTyping(string text)
        {
            List<string> list = ConvertTextToListOfTypedString(text);
            foreach (var letter in list)
            {
                Info($"Setting '{letter}' with delay");
                foreach (char t in letter)
                {
                    Element.SendKeys(t.ToString());
                    Browser.Sleep(TimeoutToDelayCharMs);
                }
            }
        }

        /// Convert Text To List Of Typed String
        /// <param name="text">text for set</param>
        protected List<string> ConvertTextToListOfTypedString(string text)
        {
            WaitForElementIsPresent();
            WaitForIsEnabled();
            Element.Clear();
            var charArray = text.ToCharArray();
            var list = new List<string>();
            string s = "";
            for (int index = 0; index < charArray.Length; index++)
            {
                s += charArray[index];
                if (s.ToCharArray().Length != 250 && index != charArray.Length - 1)
                {
                    continue;
                }

                list.Add(s);
                s = "";
            }
            return list;
        }
    }
}
