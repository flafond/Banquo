using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Support.UI;

namespace Banquo.Extensions
{
    public static class ExtendActions
    {
        private static By ByRouter(this string selector)
        {
            if (selector.StartsWith("//")) return By.XPath(selector);
            if (selector.StartsWith(".")) return By.ClassName(selector.Substring(1));
            if (selector.StartsWith("#")) return By.Id(selector.Substring(1));
            if (selector.StartsWith("<") && selector.EndsWith(">"))
                return By.TagName(selector.Substring(1, selector.Length - 2));
            return By.CssSelector(selector);

            // Just make CSS the default I think
            // throw new InvalidSelectorException($"Couldn't find match for selector '{selector}'");
        }

        public static IWebDriver And(this IWebDriver me) => me;

        public static IWebElement And(this IWebElement me) => me;

        // CodeceptJS: AmOnPage
        public static IWebDriver AmOnPage(this IWebDriver driver, string url)
        {
            driver.Navigate().GoToUrl(url);
            return driver;
        }

        public static IWebElement Click(this IWebDriver driver, By by, int msTimeout)
        {
            var element = driver.WaitForClickable(by, msTimeout);
            element.Click();
            return element;
        }

        public static IWebElement Click(this IWebDriver driver, string selector, int msTimeout) =>
            Click(driver, ByRouter(selector), msTimeout);

        public static IWebElement Click(this IWebDriver driver, By by) =>
            Click(driver, by, Banquo.defaultTimeout);

        public static IWebElement Click(this IWebDriver driver, string selector) =>
            Click(driver, ByRouter(selector));

        public static SelectElement AsSelect(this IWebElement element) =>
            new SelectElement(element);

        // Not needed in general, but sometimes functionality is triggered by scrolling into view
        public static IWebDriver VerticalScrollWindow(this IWebDriver driver, int scrollAmount)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript($"window.scroll(0, {scrollAmount});");
            return driver;
        }

        public static Actions Actions(this IWebElement element)
        {
            var driver = ((IWrapsDriver)element).WrappedDriver;
            var actions = new Actions(driver);
            // This gives the element focus
            return actions.MoveToElement(element);
        }

        public static IWebElement FillField(this IWebDriver driver, By by, string formValue, int msTimeout)
        {
            var element = driver.WaitForEnabled(by, msTimeout);
            element.Clear();
            element.SendKeys(formValue);
            return element;
        }

        public static IWebElement FillField(this IWebDriver driver, string selector, string formValue, int msTimeout) =>
            driver.FillField(ByRouter(selector), formValue, msTimeout);

        public static IWebElement FillField(this IWebDriver driver, By by, string formValue) =>
            driver.FillField(by, formValue, Banquo.defaultTimeout);

        public static IWebElement FillField(this IWebDriver driver, string selector, string formValue) =>
             driver.FillField(ByRouter(selector), formValue, Banquo.defaultTimeout);

        // CodeceptJS: action: ForceClick
        //
        // See https://www.w3.org/TR/webdriver1/#element-click for all the
        // stuff that a normal .Click() does. This is a way to do a direct
        // click (say if you want to click a hidden element).
        public static IWebElement ForceClick(this IWebElement element)
        {
            var driver = ((IWrapsDriver)element).WrappedDriver;
            var executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", element);
            return element;
        }

        public static IWebElement ForceClick(this IWebDriver driver, By by, int msTimeout)
        {
            var element = driver.WaitForElement(by, msTimeout);
            element.ForceClick();
            return element;
        }

        public static IWebElement ForceClick(this IWebDriver driver, string selector, int msTimeout) =>
            ForceClick(driver, ByRouter(selector), msTimeout);

        public static IWebElement ForceClick(this IWebDriver driver, By by) =>
            ForceClick(driver, by, Banquo.defaultTimeout);

        public static IWebElement ForceClick(this IWebDriver driver, string selector) =>
            ForceClick(driver, ByRouter(selector));

        public static IWebElement SendKeys(this IWebElement element, string text, int msDelay)
        {
            foreach (var @char in text)
            {
                element.SendKeys($"{@char}");
                Thread.Sleep(msDelay);
            }
            return element;
        }

        // He needed this once on an Angular page. Won't need this in most cases.
        public static IWebElement ForceClear(this IWebElement element)
        {
            var nChars = element.GetAttribute("value").Length;
            element.SendKeys(Keys.End);
            for (int i = 0; i < nChars; i++)
            {
                element.SendKeys(Keys.Backspace, 50);
            }
            return element;
        }

        public static IWebDriver SubmitForm(this IWebDriver driver, int index)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript($"document.forms[{index}].submit();");
            return driver;
        }

        public static IWebDriver SubmitForm(this IWebDriver driver)
        {
            return driver.SubmitForm(0);
        }

        public static IWebDriver Submitform(this IWebDriver driver, string id)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript($"document.forms['{id}'].submit();");
            return driver;
        }
    }
}