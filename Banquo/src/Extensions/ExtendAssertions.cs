using System.Collections.Generic;
using Banquo.Exceptions;
using OpenQA.Selenium;

namespace Banquo.Extensions
{
    public static class ExtendAssertions
    {
        public static IWebElement See(this IWebDriver driver, string expectedText, int msTimeout)
        {
            By findBy = default;
            try
            {
                // Used this as reference: https://stackoverflow.com/a/3655588
                findBy = By.XPath($"//*[text()[contains(.,'{expectedText}')]]");
                return driver.WaitForElement(findBy, msTimeout);
            }
            catch (WebDriverTimeoutException e)
            {
                throw new TimeoutException(
                    $"text '{expectedText}', not found", msTimeout, e);
            }
        }

        public static IWebElement See(this IWebDriver driver, string expectedText) =>
            See(driver, expectedText, Banquo.defaultTimeout);

        public static IWebDriver DontSee(this IWebDriver driver, string unexpectedText, int msTimeout)
        {
            // Used this as reference: https://stackoverflow.com/a/3655588
            var findBy = By.XPath($"//body//*[text()[contains(.,'{unexpectedText}')]]");
            return driver.WaitForNoElement(findBy, msTimeout);
        }

        public static IWebDriver DontSee(this IWebDriver driver, string unexpectedText) =>
            DontSee(driver, unexpectedText, Banquo.defaultTimeout);

        public static IWebElement SeeElement(this IWebDriver driver, By by, int msTimeout)
        {
            try
            {
                return driver.WaitForVisible(by, msTimeout);
            }
            catch (WebDriverTimeoutException e)
            {
                throw new TimeoutException($"visible element '{by}'", msTimeout, e);
            }
        }

        public static IWebElement SeeElement(this IWebDriver driver, string selector, int msTimeout) =>
            driver.SeeElement(Banquo.ByRouter(selector), msTimeout);

        public static IWebElement SeeElement(this IWebDriver driver, By by) =>
            driver.SeeElement(by, Banquo.defaultTimeout);

        public static IWebElement SeeElement(this IWebDriver driver, string selector) =>
            driver.SeeElement(Banquo.ByRouter(selector));

        public static IWebElement SeeElementInDOM(this IWebDriver driver, By by, int msTimeout)
        {
            try
            {
                return driver.WaitForElement(by, msTimeout);
            }
            catch (WebDriverTimeoutException e)
            {
                throw new TimeoutException($"'{by}' in DOM", msTimeout, e);
            }
        }

        public static IWebElement SeeElementInDOM(this IWebDriver driver, string selector, int msTimeout) =>
            driver.SeeElementInDOM(Banquo.ByRouter(selector), msTimeout);

        public static IWebElement SeeElementInDOM(this IWebDriver driver, By by) =>
            driver.SeeElementInDOM(by, Banquo.defaultTimeout);

        public static IWebElement SeeElementInDOM(this IWebDriver driver, string selector) =>
            driver.SeeElementInDOM(Banquo.ByRouter(selector));

        public static IWebDriver SeeInTitle(this IWebDriver driver, string expectedTitleContent, int msTimeout)
        {
            try
            {
                return driver.WaitUntil((d, expected) => d.Title.Contains(expected), expectedTitleContent, msTimeout);
            }
            catch (WebDriverTimeoutException e)
            {
                throw new TimeoutException(
                    $"'{expectedTitleContent}' in title '{driver.Title}'", msTimeout, e);
            }
        }

        public static IWebDriver SeeInTitle(this IWebDriver driver, string expectedTitleContent) =>
            driver.SeeInTitle(expectedTitleContent, Banquo.defaultTimeout);

        public static IWebDriver SeeTitleEquals(this IWebDriver driver, string expectedTitleContent, int msTimeout)
        {
            try
            {
                return driver.WaitUntil((d, expected) => d.Title == expected, expectedTitleContent, msTimeout);
            }
            catch (WebDriverTimeoutException e)
            {
                // This exception message is not necessarily accurate - the page title may have
                // changed any number of times during the timeout process. But this is a decent
                // message at least.
                throw new TimeoutException(
                    $"'{expectedTitleContent}' in title", msTimeout, e);
            }
        }

        public static IWebDriver SeeTitleEquals(this IWebDriver driver, string expectedTitleContent) =>
            driver.SeeTitleEquals(expectedTitleContent, Banquo.defaultTimeout);

        public static IWebDriver DontSeeInTitle(this IWebDriver driver, string expectedTitleContent, int msTimeout)
        {
            try
            {
                return driver.WaitUntil((d, expected) => !d.Title.Contains(expected), expectedTitleContent, msTimeout);
            }
            catch (WebDriverTimeoutException e)
            {
                throw new TimeoutException(
                    $"Expected '{expectedTitleContent}' to not be in title '{driver.Title}', it always was [timeout: {msTimeout} mS].\n", e);
            }
        }

        public static IWebDriver DontSeeInTitle(this IWebDriver driver, string expectedTitleContent) =>
            driver.DontSeeInTitle(expectedTitleContent, Banquo.defaultTimeout);

        public static IReadOnlyList<IWebElement> SeeNumberofElements(this IWebDriver driver, By by, int expectedCount, int msTimeout)
        {
            try
            {
                return driver.WaitForElementCount(by, expectedCount);
            }
            catch (WebDriverTimeoutException e)
            {
                throw new TimeoutException(
                    $"Expected'{by}' to be seen {expectedCount} times, was not [timeout: {msTimeout} mS].\n", e);
            }
        }

        public static IReadOnlyList<IWebElement> SeeNumberofElements(this IWebDriver driver, string selector, int expectedCount, int msTimeout) =>
            driver.SeeNumberofElements(Banquo.ByRouter(selector), expectedCount, msTimeout);

        public static IReadOnlyList<IWebElement> SeeNumberofElements(this IWebDriver driver, By by, int expectedCount) =>
            driver.SeeNumberofElements(by, expectedCount, Banquo.defaultTimeout);

        public static IReadOnlyList<IWebElement> SeeNumberofElements(this IWebDriver driver, string selector, int expectedCount) =>
            driver.SeeNumberofElements(Banquo.ByRouter(selector), expectedCount);
    }
}