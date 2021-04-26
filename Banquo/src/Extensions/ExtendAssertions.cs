using System.Collections.Generic;
using Banquo.Exceptions;
using FluentAssertions;
using OpenQA.Selenium;

namespace Banquo.Extensions
{
    public partial class User : IWebDriver
    {
        public User Sees(string expected, int msTimeout = Banquo.DefaultTimeout)
        {
            try
            {
                // Used this as reference: https://stackoverflow.com/a/3655588
                WaitForVisible($"//*[text()[contains(.,'{expected}')]]");
                return this;
            }
            catch (WebDriverTimeoutException e)
            {
                throw new TimeoutException($"text '{expected}'", msTimeout, e);
            }
        }

        // Used this as reference: https://stackoverflow.com/a/3655588
        public User DoesntSee(string notExpected, int msTimeout = Banquo.DefaultTimeout)
        {
            WaitForNotVisible($"//body//*[text()[contains(.,'{notExpected}')]]", msTimeout)
               .Should().BeTrue();
            return this;
        }

        public DOMElement SeesElement(By by, int msTimeout = Banquo.DefaultTimeout)
        {
            try
            {
                return WaitForVisible(by, msTimeout);
            }
            catch (WebDriverTimeoutException e)
            {
                throw new TimeoutException($"visible element '{by}'", msTimeout, e);
            }
        }

        public DOMElement SeesElement(string selector, int msTimeout = Banquo.DefaultTimeout) =>
            SeesElement(Banquo.ByRouter(selector), msTimeout);

        public DOMElement SeesElementInDOM(By by, int msTimeout = Banquo.DefaultTimeout)
        {
            try
            {
                return WaitForElement(by, msTimeout);
            }
            catch (WebDriverTimeoutException e)
            {
                throw new TimeoutException($"'{by}' in DOM", msTimeout, e);
            }
        }

        public DOMElement SeesElementInDOM(string selector, int msTimeout = Banquo.DefaultTimeout) =>
            SeesElementInDOM(Banquo.ByRouter(selector), msTimeout);

        public User SeesInTitle(string expectedTitleContent, int msTimeout = Banquo.DefaultTimeout)
        {
            try
            {
                return WaitUntil((d, expected) => d.Title.Contains(expected), expectedTitleContent, msTimeout);
            }
            catch (WebDriverTimeoutException e)
            {
                throw new TimeoutException(
                    $"'{expectedTitleContent}' in title '{driver.Title}'", msTimeout, e);
            }
        }

        public User SeesTitleEquals(string expectedTitle, int msTimeout = Banquo.DefaultTimeout)
        {
            try
            {
                return WaitUntil((d, expected) => d.Title == expected, expectedTitle, msTimeout);
            }
            catch (WebDriverTimeoutException e)
            {
                throw new TimeoutException($"Expected title to be '{expectedTitle}', but it isn't ({Title}) [timeout: {msTimeout} mS].\n", e);
            }
        }

        public User DoesntSeeInTitle(string dontExpectInTitle, int msTimeout = Banquo.DefaultTimeout)
        {
            try
            {
                return WaitUntil((d, expected) => !d.Title.Contains(expected), dontExpectInTitle, msTimeout);
            }
            catch (WebDriverTimeoutException e)
            {
                throw new TimeoutException(
                    $"Unexpected string '{dontExpectInTitle}' in title ({Title}) [timeout: {msTimeout} mS].\n", e);
            }
        }

        public IReadOnlyList<DOMElement> SeesNumberofElements(By by, int expectedCount, int msTimeout = Banquo.DefaultTimeout)
        {
            try
            {
                return WaitForElementCount(by, expectedCount);
            }
            catch (WebDriverTimeoutException e)
            {
                throw new TimeoutException(
                    $"Expected'{by}' to be seen {expectedCount} times, but it wasn't [timeout: {msTimeout} mS].\n", e);
            }
        }

        public IReadOnlyList<DOMElement> SeesNumberofElements(string selector, int expectedCount, int msTimeout = Banquo.DefaultTimeout) =>
            SeesNumberofElements(Banquo.ByRouter(selector), expectedCount, msTimeout);

        public DOMElement SeesInField(By by, string fieldValue, int msTimeout = Banquo.DefaultTimeout)
        {
            try
            {
                var element = WaitForElement(by, msTimeout);
                return element.SeesInField(fieldValue);
            }
            catch (WebDriverTimeoutException e)
            {
                throw new TimeoutException($"'{by}' in DOM", msTimeout, e);
            }
        }

        public DOMElement SeesInField(string selector, string fieldValue, int msTimeout = Banquo.DefaultTimeout) =>
            SeesInField(ByRouter(selector), fieldValue, msTimeout);
    }
}