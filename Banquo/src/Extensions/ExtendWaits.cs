using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Banquo.Extensions
{
    public partial class User : IWebDriver
    {
        private static string FixByString(By by)
        {
            var match = Regex.Match(by.ToString(), "'(.*)'");
            return match.Groups[1].Value;
        }

        public static By ByRouter(string selector)
        {
            if (selector.StartsWith("//")) return By.XPath(selector);
            if (selector.StartsWith(".")) return By.ClassName(selector.Substring(1));
            if (selector.StartsWith("#")) return By.Id(selector.Substring(1));
            if (selector.StartsWith("<") && selector.EndsWith(">"))
                return By.TagName(selector.Substring(1, selector.Length - 2));
            return By.CssSelector(selector);
        }

        // Generic method
        public User WaitFor(Func<IWebDriver, string, bool> WaitFn, string expected, int msTimeout = Banquo.DefaultTimeout)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromMilliseconds(msTimeout));
            wait.Until(d => WaitFn(d, expected));
            return this;
        }

      public User WaitFor(Func<IWebDriver, bool> WaitFn, int msTimeout = Banquo.DefaultTimeout)
      {
         var wait = new WebDriverWait(Driver, TimeSpan.FromMilliseconds(msTimeout));
         wait.Until(d => WaitFn(d));
         return this;
      }

      public User WaitMSec(int msWait = Banquo.DefaultTimeout)
        {
            Thread.Sleep(TimeSpan.FromMilliseconds(msWait));
            return this;
        }

        public DOMElement WaitForElement(By by, int msTimeout = Banquo.DefaultTimeout)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromMilliseconds(msTimeout));
            return DOMElement.Make(wait.Until(d => d.FindElement(by)));
        }

        public DOMElement WaitForElement(string selector, int msTimeout = Banquo.DefaultTimeout) =>
            WaitForElement(ByRouter(selector), msTimeout);

        public User WaitForNoElement(By by, int msTimeout = Banquo.DefaultTimeout)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromMilliseconds(msTimeout));
            try
            {
                wait.Until(d => d.FindElements(by).Count == 0);
                return this;
            }
            catch (WebDriverTimeoutException e)
            {
                throw new Exceptions.TimeoutException($"Unexpected element '{FixByString(by)}' on page.", e);
            }
        }

        public User WaitForNoElement(string selector, int msTimeout = Banquo.DefaultTimeout) =>
            WaitForNoElement(ByRouter(selector), msTimeout);

        public DOMElement WaitForClickable(By by, int msTimeout = Banquo.DefaultTimeout)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromMilliseconds(msTimeout));
            return DOMElement.Make(wait.Until(d =>
            {
                var element = d.FindElement(by);
                return element.Displayed && element.Enabled ? element : null;
            }));
        }

        public DOMElement WaitForClickable(string selector, int msTimeout = Banquo.DefaultTimeout) =>
            WaitForClickable(ByRouter(selector), msTimeout);

        public IReadOnlyList<DOMElement> GetElements(By by, int msTimeout = Banquo.DefaultTimeout)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromMilliseconds(msTimeout));
            return DOMElement.Make((wait.Until(d => d.FindElements(by))));
        }

        public IReadOnlyList<DOMElement> GetElements(string selector, int msTimeout = Banquo.DefaultTimeout) =>
            GetElements(ByRouter(selector), msTimeout);

        public DOMElement WaitForVisible(By by, int msTimeout = Banquo.DefaultTimeout)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromMilliseconds(msTimeout));
            return DOMElement.Make(wait.Until(d =>
            {
                var element = d.FindElement(by);
                return element.Displayed ? element : null;
            }));
        }

        public bool WaitForNotVisible(By by, int msTimeout = Banquo.DefaultTimeout)
        {
            try
            {
                var wait = new WebDriverWait(Driver, TimeSpan.FromMilliseconds(msTimeout));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                return wait.Until(d => !d.FindElement(by).Displayed);
            }
            catch (WebDriverTimeoutException)
            {
                return true;
            }
        }

        public bool WaitForNotVisible(string selector, int msTimeout = Banquo.DefaultTimeout) =>
            WaitForNotVisible(ByRouter(selector), msTimeout);

        public DOMElement WaitForVisible(string selector, int msTimeout = Banquo.DefaultTimeout) =>
            WaitForVisible(ByRouter(selector), msTimeout);

        public IReadOnlyList<DOMElement> GetVisibleElements(By by, int msTimeout = Banquo.DefaultTimeout)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromMilliseconds(msTimeout));
            return wait.Until(d =>
            {
                var elements = d.FindElements(by).Where(i => i.Displayed).ToList();
                return DOMElement.Make(elements);
            });
        }

        public IReadOnlyList<DOMElement> GetVisibleElements(string selector, int msTimeout = Banquo.DefaultTimeout) =>
            GetVisibleElements(ByRouter(selector), msTimeout);

        // An enabled element is one that is ready to be interacted with
        public DOMElement WaitForEnabled(By by, int msTimeout = Banquo.DefaultTimeout)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromMilliseconds(msTimeout));
            return DOMElement.Make(wait.Until(d =>
            {
                var element = d.FindElement(by);
                return element.Enabled ? element : null;
            }));
        }

        public DOMElement WaitForEnabled(string selector, int msTimeout = Banquo.DefaultTimeout) =>
            WaitForEnabled(ByRouter(selector), msTimeout);

        // This is by far the hardest so far since it requires two parameters to be passed.
        // Think about a more graceful way to do this.
        public IReadOnlyList<DOMElement> WaitForElementCount(By by, int count, int msTimeout = Banquo.DefaultTimeout)
        {
            var byAndCount = new { by, count };

            Banquo.WaitFor<IReadOnlyList<DOMElement>>(this, byAndCount, (wfDriver, wfParams) =>
            {
                var elements = wfDriver.FindElements((By)wfParams.by);
                return (elements.Count == (int)wfParams.count) ? DOMElement.Make(elements) : null;
            }
            , msTimeout);
            return null;
        }

        public IReadOnlyList<DOMElement> WaitForElementCount(string selector, int targetCount, int msTimeout = Banquo.DefaultTimeout) =>
            WaitForElementCount(ByRouter(selector), targetCount, msTimeout);

        // See https://stackoverflow.com/questions/36590274/selenium-how-to-wait-until-page-is-completely-loaded
        public User WaitPageReady(int msTimeout = Banquo.DefaultTimeout)
        {
            new WebDriverWait(Driver, TimeSpan.FromMilliseconds(msTimeout)).Until(
               d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete")
            );
            return this;
        }
    }
}