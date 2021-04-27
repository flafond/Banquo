using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;

namespace Banquo.Extensions
{
   public partial class DOMElement : IWebElement
    {
        private readonly IWebElement element;

        public DOMElement(IWebElement element) => this.element = element;

        public IWebElement WebElement => element;

        public User AsUser => new User(((IWrapsDriver)element).WrappedDriver);

        public static DOMElement Make(IWebElement element) => new DOMElement(element);

        public static IReadOnlyList<DOMElement> Make(IReadOnlyList<IWebElement> elements) =>
            elements.Select(e => Make(e)).ToList();

        public string TagName => element.TagName;

        public string Text => element.Text;

        public bool Enabled => element.Enabled;

        public bool Selected => element.Selected;

        public Point Location => element.Location;

        public Size Size => element.Size;

        public bool Displayed => element.Displayed;

        public void Clear()
        {
            element.Clear();
        }

        public void Click()
        {
            element.Click();
        }

        public IWebElement FindElement(By by)
        {
            return element.FindElement(by);
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return element.FindElements(by);
        }

        public string GetAttribute(string attributeName)
        {
            return element.GetAttribute(attributeName);
        }

        public string GetCssValue(string propertyName)
        {
            return element.GetCssValue(propertyName);
        }

        public string GetProperty(string propertyName)
        {
            return element.GetProperty(propertyName);
        }

        public void SendKeys(string text)
        {
            element.SendKeys(text);
        }

        public void Submit()
        {
            element.Submit();
        }
    }
}