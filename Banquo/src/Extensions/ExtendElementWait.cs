using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

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
    }
}