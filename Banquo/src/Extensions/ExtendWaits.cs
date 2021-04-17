using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Banquo.Extensions
{
    public static class ExtendWaits
    {
        //public static readonly int Banquo.defaultTimeout = 5000;

        private static string FixByString(By by)
        {
            var match = Regex.Match(by.ToString(), "'(.*)'");
            return match.Groups[1].Value;
        }

        public static By ByRouter(this string selector)
        {
            if (selector.StartsWith("//")) return By.XPath(selector);
            if (selector.StartsWith(".")) return By.ClassName(selector.Substring(1));
            if (selector.StartsWith("#")) return By.Id(selector.Substring(1));
            if (selector.StartsWith("<") && selector.EndsWith(">"))
                return By.TagName(selector.Substring(1, selector.Length - 2));
            return By.CssSelector(selector);
        }

        // Generic method
        public static IWebDriver WaitUntil(this IWebDriver driver, Func<IWebDriver, string, bool> WaitFn, string expected, int msTimeout)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(msTimeout));
            wait.Until<bool>((d) => WaitFn(d, expected));
            return driver;
        }

        public static IWebDriver Wait(this IWebDriver driver, int msWait)
        {
            Thread.Sleep(TimeSpan.FromMilliseconds(msWait));
            return driver;
        }

        public static IWebDriver Wait(this IWebDriver driver) => Wait(driver, Banquo.defaultTimeout);

        public static IWebElement WaitForElement(this IWebDriver driver, By by, int msTimeout)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(msTimeout));
            return wait.Until(d => d.FindElement(by));
        }

        public static IWebElement WaitForElement(this IWebDriver driver, string selector, int msTimeout) =>
            WaitForElement(driver, ByRouter(selector), msTimeout);

        public static IWebElement WaitForElement(this IWebDriver driver, By by) =>
            WaitForElement(driver, by, Banquo.defaultTimeout);

        public static IWebElement WaitForElement(this IWebDriver driver, string selector) =>
           WaitForElement(driver, ByRouter(selector), Banquo.defaultTimeout);

        public static IWebDriver WaitForNoElement(this IWebDriver driver, By by, int msTimeout)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(msTimeout));
            try
            {
                wait.Until(d => d.FindElements(by).Count == 0);
                return driver;
            }
            catch (WebDriverTimeoutException e)
            {
                throw new Exceptions.TimeoutException($"Unexpected element '{FixByString(by)}' on page.", e);
            }
        }

        public static IWebDriver WaitForNoElement(this IWebDriver driver, string selector, int msTimeout) =>
            WaitForNoElement(driver, ByRouter(selector), msTimeout);

        public static IWebDriver WaitForNoElement(this IWebDriver driver, By by) =>
            WaitForNoElement(driver, by, Banquo.defaultTimeout);

        public static IWebDriver WaitForNoElement(this IWebDriver driver, string selector) =>
           WaitForNoElement(driver, ByRouter(selector), Banquo.defaultTimeout);

        public static IWebElement WaitForClickable(this IWebDriver driver, By by, int msTimeout)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(msTimeout));
            return wait.Until(d =>
            {
                var element = d.FindElement(by);
                return element.Displayed && element.Enabled ? element : null;
            });
        }

        public static IWebElement WaitForClickable(this IWebDriver driver, string selector, int msTimeout) =>
            WaitForClickable(driver, ByRouter(selector), msTimeout);

        public static IWebElement WaitForClickable(this IWebDriver driver, By by) =>
            WaitForClickable(driver, by, Banquo.defaultTimeout);

        public static IWebElement WaitForClickable(this IWebDriver driver, string selector) =>
            WaitForClickable(driver, ByRouter(selector));

        public static IReadOnlyList<IWebElement> GetElements(this IWebDriver driver, By by, int msTimeout)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(msTimeout));
            return new List<IWebElement>(wait.Until(d => d.FindElements(by)));
        }

        public static IReadOnlyList<IWebElement> GetElements(this IWebDriver driver, string selector, int msTimeout) =>
            GetElements(driver, ByRouter(selector), msTimeout);

        public static IReadOnlyList<IWebElement> GetElements(this IWebDriver driver, By by) =>
            GetElements(driver, by, Banquo.defaultTimeout);

        public static IReadOnlyList<IWebElement> GetElements(this IWebDriver driver, string selector) =>
            GetElements(driver, ByRouter(selector));

        public static IWebElement WaitForVisible(this IWebDriver driver, By by, int msTimeout)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(msTimeout));
            return wait.Until(d =>
            {
                var element = d.FindElement(by);
                return element.Displayed ? element : null;
            });
        }

        public static IWebElement WaitForVisible(this IWebDriver driver, string selector, int msTimeout) =>
            WaitForVisible(driver, ByRouter(selector), msTimeout);

        public static IWebElement WaitForVisible(this IWebDriver driver, By by) =>
            WaitForVisible(driver, by, Banquo.defaultTimeout);

        public static IWebElement WaitForVisible(this IWebDriver driver, string selector) =>
            WaitForVisible(driver, ByRouter(selector));

        public static IReadOnlyList<IWebElement> GetVisibleElements(this IWebDriver driver, By by, int msTimeout)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(msTimeout));
            return wait.Until(d =>
            {
                var elements = d.FindElements(by).Where(i => i.Displayed).ToList();
                return new List<IWebElement>(elements);
            });
        }

        public static IReadOnlyList<IWebElement> GetVisibleElements(this IWebDriver driver, string selector, int msTimeout) =>
            GetVisibleElements(driver, ByRouter(selector), msTimeout);

        public static IReadOnlyList<IWebElement> GetVisibleElements(this IWebDriver driver, By by) =>
            GetVisibleElements(driver, by, Banquo.defaultTimeout);

        public static IReadOnlyList<IWebElement> GetVisibleElements(this IWebDriver driver, string selector) =>
            GetVisibleElements(driver, ByRouter(selector));

        // An enabled element is one that is ready to be interacted with
        public static IWebElement WaitForEnabled(this IWebDriver driver, By by, int msTimeout)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(msTimeout));
            return wait.Until(d =>
            {
                var element = d.FindElement(by);
                return element.Enabled ? element : null;
            });
        }

        public static IWebElement WaitForEnabled(this IWebDriver driver, string selector, int msTimeout) =>
            WaitForEnabled(driver, ByRouter(selector), msTimeout);

        public static IWebElement WaitForEnabled(this IWebDriver driver, By by) =>
            WaitForVisible(driver, by, Banquo.defaultTimeout);

        public static IWebElement WaitForEnabled(this IWebDriver driver, string selector) =>
            WaitForEnabled(driver, ByRouter(selector));

        // This is by far the hardest so far since it requires two parameters to be passed.
        // Think about a more graceful way to do this.
        public static IReadOnlyList<IWebElement> WaitForElementCount(this IWebDriver driver, By by, int count, int msTimeout)
        {
            var byAndCount = new { by, count };

            Banquo.WaitFor<IReadOnlyList<IWebElement>>(driver, byAndCount, (wfDriver, wfParams) =>
            {
                var elements = wfDriver.FindElements((By)wfParams.by);
                return (elements.Count == (int)wfParams.count) ? elements : null;
            }
            , msTimeout);
            return null;
        }

        public static IReadOnlyList<IWebElement> WaitForElementCount(this IWebDriver driver, string selector, int targetCount, int msTimeout) =>
            WaitForElementCount(driver, ByRouter(selector), targetCount, msTimeout);

        public static IReadOnlyList<IWebElement> WaitForElementCount(this IWebDriver driver, By by, int targetCount) =>
            WaitForElementCount(driver, by, targetCount, Banquo.defaultTimeout);

        public static IReadOnlyList<IWebElement> WaitForElementCount(this IWebDriver driver, string selector, int targetCount) =>
            WaitForElementCount(driver, ByRouter(selector), targetCount);
    }
}