using Banquo.Exceptions;
using OpenQA.Selenium;

namespace Banquo.Extensions
{
    public partial class DOMElement : IWebElement

    {
        public  DOMElement Sees(int msTimeout = Banquo.DefaultTimeout)
        {
            var driver = Banquo.Element2Driver(this);
            if (Banquo.WaitFor(this, e => e.Displayed, msTimeout))
            {
                return this;
            }
            else
            {
                throw new TimeoutException($"{this} to be visible", msTimeout);
            }
        }

        public DOMElement SeesInField(string fieldValue, int msTimeout = Banquo.DefaultTimeout) =>
            WaitForField(fieldValue, msTimeout);
    }
}