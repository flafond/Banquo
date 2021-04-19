using System;
using OpenQA.Selenium;
using System.Threading;
using OpenQA.Selenium.Internal;

namespace Banquo
{
    public static class Banquo
    {
        public const int DefaultTimeout = 5000;

        public static IWebDriver Element2Driver(IWebElement element) => ((IWrapsDriver)element).WrappedDriver;

        public static bool WaitFor(IWebElement element, Func<IWebElement, bool> cond, int msTimeout)
        {
            int cycles = 10;
            int msPerCycle = msTimeout / cycles;
            for (int c = 0; c < cycles; c++)
            {
                if (cond(element)) return true;
                Thread.Sleep(msPerCycle);
            }
            return false;
        }

        /// <summary>
        /// Continually cycle, evaluating waitingFor() until it's not null or
        /// until it times out. The standard WebDriverWait doesn't handle parameters.
        /// </summary>
        /// <typeparam name="T">Type of data to be returned upon successful completion</typeparam>
        /// <param name="driver">driver made available to the conditionsMet() delegate</param>
        /// <param name="parms">Array of parameters made available to conditionsMet() delegate</param>
        /// <param name="waitingFor">Delegate to evaluate to determine if we're done waiting (and what to return)</param>
        /// <param name="msTimeout">Milliseconds to wait before timing out</param>
        /// <returns></returns>
        public static T WaitFor<T>(IWebDriver driver, dynamic parms, Func<IWebDriver, dynamic, T> waitingFor, int msTimeout)
        {
            int cycles = 10;
            int msPerCycle = msTimeout / cycles;
            for (int c = 0; c < cycles; c++)
            {
                var result = (T)waitingFor(driver, parms);
                if (result != null)
                    return result;
                Thread.Sleep(msPerCycle);
            }
            return default;
        }

        public static By ByRouter(string selector)
        {
            if (selector.StartsWith("//")) return By.XPath(selector);
            if (selector.StartsWith(".")) return By.ClassName(selector[1..]);
            if (selector.StartsWith("#")) return By.Id(selector[1..]);
            if (selector.StartsWith("<") && selector.EndsWith(">"))
                return By.TagName(selector[1..^1]);
            return By.CssSelector(selector);
        }
    }
}
