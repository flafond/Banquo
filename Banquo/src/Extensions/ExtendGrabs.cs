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
    public partial class User : IWebDriver
    {
        // -- GrabAttributeFrom --
        public string GrabAttributeFrom(By by, string attributeName, int msTimeout = Banquo.DefaultTimeout) =>
            WaitForElement(by, msTimeout).GetAttribute(attributeName);

        public string GrabAttributeFrom(string selector, string attributeName, int msTimeout = Banquo.DefaultTimeout) =>
            GrabAttributeFrom(Banquo.ByRouter(selector), attributeName, msTimeout);

        // -- GrabAttributeFromAll --

        // Because this is multiple, there is no wait version currently. Should we wait for the
        // entire timeout to see if more show up (or disappear)? For now this is a real-time grab.
        public IReadOnlyList<string> GrabAttributeFromAll(By by, string attributeName)
        {
            var list = new List<IWebElement>(driver.FindElements(by));
            return (IReadOnlyList<string>)list.Select<IWebElement, string>((e, i) => e.GetAttribute(attributeName));
        }

        public IReadOnlyList<string> GrabAttributeFromAll(string selector, string attributeName) =>
            GrabAttributeFromAll(Banquo.ByRouter(selector), attributeName);
    }
}