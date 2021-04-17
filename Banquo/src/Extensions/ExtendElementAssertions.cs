using Banquo.Exceptions;
using OpenQA.Selenium;

namespace Banquo.Extensions
{
    public static class ExtendElementAssertions
    {
        public static IWebElement See(this IWebElement element, int msTimeout)
        {
            var driver = Banquo.Element2Driver(element);
            if (Banquo.WaitFor(element, e => e.Displayed, msTimeout))
            {
                return element;
            }
            else
            {
                throw new TimeoutException($"{element} to be visible", msTimeout);
            }
        }

        public static IWebElement See(this IWebElement element) =>
            element.See(Banquo.defaultTimeout);
    }
}