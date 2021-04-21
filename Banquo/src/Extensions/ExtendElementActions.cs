using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Internal;

namespace Banquo.Extensions
{
    public partial class DOMElement : IWebElement
    {
        public DOMElement And() => this;

        public Actions Actions()
        {
            var driver = ((IWrapsDriver)this).WrappedDriver;
            var actions = new Actions(driver);
            // This gives the element focus
            return actions.MoveToElement(this);
        }

        // CodeceptJS: action: ForceClick
        //
        // See https://www.w3.org/TR/webdriver1/#element-click for all the
        // stuff that a normal .Click() does. This is a way to do a direct
        // click (say if you want to click a hidden element).
        public DOMElement ForceClick()
        {
            var driver = ((IWrapsDriver)element).WrappedDriver;
            var executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", this);
            return this;
        }

        public DOMElement SendKeys(string text, int msDelay)
        {
            foreach (var @char in text)
            {
                SendKeys($"{@char}");
                Thread.Sleep(msDelay);
            }
            return this;
        }

        // He needed this once on an Angular page. Won't need this in most cases.
        public DOMElement ForceClear()
        {
            var nChars = GetAttribute("value").Length;
            SendKeys(Keys.End);
            for (int i = 0; i < nChars; i++)
            {
                SendKeys(Keys.Backspace, 50);
            }
            return this;
        }

        public DOMElement ClearsField()
        {
            Clear();
            return this;
        }

        public DOMElement FillsField(string value)
        {
            ClearsField().SendKeys(value);
            return this;
        }

        public DOMElement ChecksOption()
        {
            if (!Selected)
            {
                // This worked for the DemoQA site; is this the best?
                SendKeys(Keys.Space);
            }
            return this;
        }

        public DOMElement UncheckOption()
        {
            if (Selected)
            {
                // This worked for the DemoQA site; is this the best?
                SendKeys(Keys.Space);
            }
            return this;
        }

        public DOMElement Type(string toType)
        {
            foreach (char c in toType)
            {
                SendKeys(c.ToString());
                Thread.Sleep(10);
            }
            return this;
        }
    }
}