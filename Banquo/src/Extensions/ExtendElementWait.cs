using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Threading;
using static Banquo.Banquo;

namespace Banquo.Extensions
{
   public partial class DOMElement : IWebElement
    {
        private const int delay = 100;

        private DOMElement DOMElementWait(Func<DOMElement, string, bool> waitFn, string expect, int msTimeout = Banquo.DefaultTimeout)
        {
            var iterations = msTimeout / delay;
            iterations = (iterations >= 1) ? iterations : 1;
            for (var i = 0; i < iterations; i++)
            {
                if (waitFn(this, expect)) return this;
                Thread.Sleep(delay);
            }
            throw new TimeoutException("DOMElement not found");
        }

        private DOMElement DOMElementWait(Func<DOMElement, bool> waitFn, int msTimeout = Banquo.DefaultTimeout)
        {
            var iterations = msTimeout / delay;
            iterations = (iterations >= 1) ? iterations : 1;
            for (var i = 0; i < iterations; i++)
            {
                if (waitFn(this)) return this;
                Thread.Sleep(delay);
            }
            throw new TimeoutException("DOMElement not found");
        }

        private DOMElement DOMElementWait(By by, int msTimeout = Banquo.DefaultTimeout)
        {
            var iterations = msTimeout / delay;
            iterations = (iterations >= 1) ? iterations : 1;
            for (var i = 0; i < iterations; i++)
            {
                if (FindElement(by).Displayed) return this;
            }
            throw new TimeoutException("DOMElement not found");
        }

        public DOMElement WaitForField(string fieldValue, int msTimeout = Banquo.DefaultTimeout) =>
            DOMElementWait((de, field) => de.GetAttribute("value") == field, fieldValue, msTimeout);

        public DOMElement WaitForVisible(By by, int msTimeout = Banquo.DefaultTimeout) =>
            DOMElementWait(by, msTimeout);

        public DOMElement WaitForVisible(string selector, int msTimeout = Banquo.DefaultTimeout) =>
            WaitForVisible(ByRouter(selector), msTimeout);

        public DOMElement WaitForEnabled(By by, int msTimeout = Banquo.DefaultTimeout) =>
            DOMElementWait(by, msTimeout);

        public DOMElement WaitForEnabled(string selector, int msTimeout = Banquo.DefaultTimeout) =>
            WaitForEnabled(ByRouter(selector), msTimeout);

        public DOMElement WaitForElement(By by, int msTimeout = Banquo.DefaultTimeout) =>
            DOMElementWait(by, msTimeout);

        public DOMElement WaitForElement(string selector, int msTimeout = Banquo.DefaultTimeout) =>
            WaitForElement(ByRouter(selector), msTimeout);

        public DOMElement WaitMSec(int ms)
        {
            Thread.Sleep(ms);
            return this;
        }

        public DOMElement PickVisibleOne(By by)
        {
            DOMElement oneFound = default;
            IReadOnlyList<DOMElement> list = GetElements(by);
            foreach (DOMElement item in list)
            {
                if (item.Displayed)
                {
                    try
                    {
                        oneFound = (oneFound == default) ? item : throw new InvalidSelectorException($"Multiple elements matched {by}");
                    }
                    catch (StaleElementReferenceException)
                    {
                        // Why do they immediately go stale? I guess this cuts down on extra choices
                    }
                }
            }
            return (oneFound != default) ? oneFound : throw new InvalidSelectorException($"No elements matched {by}");
        }

        public DOMElement PickVisibleOne(string selector) => PickVisibleOne(ByRouter(selector));

        public DOMElement GetElement(By by) => DOMElement.Make(FindElement(by));

        public DOMElement GetElement(string selector) => GetElement(ByRouter(selector));

        public DOMElement MaybeGetElement(By by)
        {
            try
            {
                return GetElement(by);
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }

        public DOMElement MaybeGetElement(string selector) => MaybeGetElement(ByRouter(selector));

        public IReadOnlyList<DOMElement> GetElements(By by) => DOMElement.Make(FindElements(by));

        public IReadOnlyList<DOMElement> GetElements(string selector) =>
            GetElements(ByRouter(selector));
    }
}