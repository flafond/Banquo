using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using System.Drawing;
using static Banquo.Banquo;

namespace Banquo.Extensions
{
    /// <summary>
    /// These aren't asynchronous like the CodeceptJS ones are.
    /// They also don't wait for the data being grabbed to change, they just wait
    /// for the object being grabbed. The *ForAll() methods don't wait at all.
    /// </summary>
    public partial class User : IWebDriver
    {
        public string GrabAttributeFrom(By by, string attributeName, int msTimeout = Banquo.DefaultTimeout) =>
            WaitForElement(by, msTimeout).GetAttribute(attributeName);

        public string GrabAttributeFrom(string selector, string attributeName, int msTimeout = Banquo.DefaultTimeout) =>
            GrabAttributeFrom(ByRouter(selector), attributeName, msTimeout);

        public IReadOnlyList<string> GrabAttributeFromAll(By by, string attributeName)
        {
            var list = new List<DOMElement>(GetElements(by));
            return list.Select(e => e.GrabAttribute(attributeName)).ToList();
        }

        public IReadOnlyList<string> GrabAttributeFromAll(string selector, string attributeName) =>
            GrabAttributeFromAll(ByRouter(selector), attributeName);

        public Cookie GrabCookie(string cookieName) => Manage().Cookies.GetCookieNamed(cookieName);

        public string GrabCssPropertyFrom(By by, string propertyName, int msTimeout = Banquo.DefaultTimeout) =>
            WaitForElement(by, msTimeout).GrabCssProperty(propertyName);

        public string GrabCssPropertyFrom(string selector, string propertyName, int msTimeout = Banquo.DefaultTimeout) =>
            GrabCssPropertyFrom(ByRouter(selector), propertyName, msTimeout);

        public IReadOnlyList<string> GrabCssPropertyFromAll(By by, string attributeName)
        {
            var list = new List<DOMElement>(GetElements(by));
            return list.Select(e => e.GrabCssProperty(attributeName)).ToList();
        }

        public IReadOnlyList<string> GrabCssPropertyFromAll(string selector, string attributeName) =>
            GrabCssPropertyFromAll(ByRouter(selector), attributeName);

        public string GrabCurrentUrl() => Url;

        public Size GrabElementBoundingRect(By by, int msTimeout = Banquo.DefaultTimeout) =>
            WaitForElement(by, msTimeout).GrabBoundingRect;

        public Size GrabElementBoundingRect(string selector, int msTimeout = Banquo.DefaultTimeout) =>
            WaitForElement(ByRouter(selector), msTimeout).GrabBoundingRect;

        public string GrabHTMLFrom(By by, string propertyName, int msTimeout = Banquo.DefaultTimeout) =>
            WaitForElement(by, msTimeout).GrabHTML;

        public string GrabHTMLFrom(string selector, string propertyName, int msTimeout = Banquo.DefaultTimeout) =>
            GrabHTMLFrom(ByRouter(selector), propertyName, msTimeout);

        public IReadOnlyList<string> GrabHTMLFromAll(By by)
        {
            var list = new List<DOMElement>(GetElements(by));
            return list.Select(e => e.GrabHTML).ToList();
        }

        public IReadOnlyList<string> GrabHTMLFromAll(string selector) =>
            GrabHTMLFromAll(ByRouter(selector));

        public string GrabTextFrom(By by, int msTimeout = Banquo.DefaultTimeout) =>
            WaitForElement(by, msTimeout).GrabText;

        public string GrabTextFrom(string selector, int msTimeout = Banquo.DefaultTimeout) =>
            GrabTextFrom(ByRouter(selector), msTimeout);

        public IReadOnlyList<string> GrabTextFromAll(By by)
        {
            var list = new List<DOMElement>(GetElements(by));
            return list.Select(e => e.GrabText).ToList();
        }

        public IReadOnlyList<string> GrabTextFromAll(string selector) =>
            GrabHTMLFromAll(ByRouter(selector));

        public string GrabTitle(int msTimeout = Banquo.DefaultTimeout) =>
            WaitPageReady(msTimeout).Title;

        public string GrabValueFrom(By by, int msTimeout = Banquo.DefaultTimeout) =>
            WaitForEnabled(by, msTimeout).GrabValue;

        public string GrabValueFrom(string selector, int msTimeout = Banquo.DefaultTimeout) =>
            GrabValueFrom(ByRouter(selector), msTimeout);

        public IReadOnlyList<string> GrabValueFromAll(By by)
        {
            var list = new List<DOMElement>(GetElements(by));
            return list.Select(e => e.GrabValue).ToList();
        }

        public IReadOnlyList<string> GrabValueFromAll(string selector) =>
            GrabValueFromAll(ByRouter(selector));

    }
}