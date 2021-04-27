using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Banquo.Extensions
{
   public partial class DOMElement : IWebElement
    {
        private DOMElement DOMElementWait(Func<DOMElement, string, bool> waitFn, string expect, int msTimeout = Banquo.DefaultTimeout)
        {
            int delay = msTimeout / 10;
            for (int i = 0; i < 10; i++)
            {
                if (waitFn(this, expect)) return this;
                Thread.Sleep(delay);
            }
            throw new TimeoutException("DOMElement not found");
        }
        public DOMElement WaitForField(string fieldValue, int msTimeout = Banquo.DefaultTimeout) =>
            DOMElementWait((de, field) => de.GetAttribute("value") == field, fieldValue, msTimeout);

        public DOMElement WaitMSec(int ms)
        {
            Thread.Sleep(ms);
            return this;
        }

        public DOMElement GetElement(By by) => DOMElement.Make(FindElement(by));

        public DOMElement GetElement(string selector) => GetElement(Banquo.ByRouter(selector));

        public IReadOnlyList<DOMElement> GetElements(By by) => DOMElement.Make(FindElements(by));

        public IReadOnlyList<DOMElement> GetElements(string selector) =>
            GetElements(Banquo.ByRouter(selector));
    }
}