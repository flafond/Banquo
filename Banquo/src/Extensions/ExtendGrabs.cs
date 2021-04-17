using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace Banquo.Extensions
{
    /// <summary>
    /// These aren't asynchronous like the CodeceptJS ones are.
    /// They also don't wait for the data being grabbed to change, they just wait
    /// for the object being grabbed.
    /// </summary>
    public static class ExtendGrabs
    {
        // -- GrabAttributeFrom --
        public static string GrabAttributeFrom(this IWebDriver driver, By by, string attributeName, int msTimeout)
        {
            return driver.WaitForElement(by, msTimeout).GetAttribute(attributeName);
        }

        public static string GrabAttributeFrom(this IWebDriver driver, By by, string attributeName) =>
            driver.GrabAttributeFrom(by, attributeName, Banquo.defaultTimeout);

        public static string GrabAttributeFrom(this IWebDriver driver, string selector, string attributeName, int msTimeout) =>
            driver.GrabAttributeFrom(Banquo.ByRouter(selector), attributeName, msTimeout);

        public static string GrabAttributeFrom(this IWebDriver driver, string selector, string attributeName) =>
            driver.GrabAttributeFrom(Banquo.ByRouter(selector), attributeName);

        // -- GrabAttributeFromAll --

        // Because this is multiple, there is no wait version currently. Should we wait for the
        // entire timeout to see if more show up (or disappear)? For now this is a real-time grab.
        public static IReadOnlyList<string> GrabAttributeFromAll(this IWebDriver driver, By by, string attributeName)
        {
            var list = new List<IWebElement>(driver.FindElements(by));
            return (IReadOnlyList<string>)list.Select<IWebElement, string>((e, i) => e.GetAttribute(attributeName));
        }

        public static IReadOnlyList<string> GrabAttributeFromAll(this IWebDriver driver, string selector, string attributeName) =>
            driver.GrabAttributeFromAll(Banquo.ByRouter(selector), attributeName);
    }
}