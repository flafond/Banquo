using System.Drawing;
using OpenQA.Selenium;

namespace Banquo.Extensions
{
    public partial class DOMElement : IWebElement
    {
        public string GrabAttribute(string attributeName) => GetAttribute(attributeName);

        public string GrabCssProperty(string propertyName) => GetCssValue(propertyName);

        public Size GrabBoundingRect => Size;

        public string GrabHTML => GetAttribute("innerHTML");

        public string GrabText => Text;

        public string GrabValue => GetAttribute("value");
    }
}
