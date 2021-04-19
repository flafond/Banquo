using System.Collections.ObjectModel;
using System.Threading;
using OpenQA.Selenium;

namespace Banquo.Extensions
{
    public partial class User : IWebDriver
    {

        public User And() => this;

        // CodeceptJS: AmOnPage
        public User IsOnPage(string url)
        {
            Navigate().GoToUrl(url);
            return this;
        }

        public DOMElement Click(By by, int msTimeout = Banquo.DefaultTimeout)
        {
            var element = WaitForClickable(by, msTimeout);
            element.Click();
            return element;
        }

        public DOMElement Click(string selector, int msTimeout = Banquo.DefaultTimeout) =>
            Click(ByRouter(selector), msTimeout);

        // Not needed in general, but sometimes functionality is triggered by scrolling into view
        public User VerticalScrollWindow(int scrollAmount)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript($"window.scroll(0, {scrollAmount});");
            return this;
        }

        public DOMElement ClearsField(By by, int msTimeout = Banquo.DefaultTimeout) =>
            WaitForEnabled(by, msTimeout).ClearsField();

        public DOMElement ClearsField(string selector, int msTimeout = Banquo.DefaultTimeout) =>
            this.ClearsField(ByRouter(selector), msTimeout);

        public DOMElement FillsField(By by, string formValue, int msTimeout = Banquo.DefaultTimeout)
        {
            var element = ClearsField(by, msTimeout);
            element.SendKeys(formValue);
            return element;
        }

        public DOMElement FillsField(string selector, string formValue, int msTimeout = Banquo.DefaultTimeout) =>
            FillsField(ByRouter(selector), formValue, msTimeout);

        // Maybe add a check here that we have a radio button or checkbox?
        public DOMElement ChecksOption(By by, int msTimeout = Banquo.DefaultTimeout) =>
            WaitForEnabled(by, msTimeout).ChecksOption();

        public DOMElement ChecksOption(string selector, int msTimeout = Banquo.DefaultTimeout) =>
            ChecksOption(ByRouter(selector), msTimeout);

        // Maybe add a check here that we have a radio button or checkbox?
        public DOMElement UncheckOption(By by, int msTimeout = Banquo.DefaultTimeout) =>
            WaitForEnabled(by, msTimeout).UncheckOption();

        public DOMElement UncheckOption(string selector, int msTimeout = Banquo.DefaultTimeout) =>
            UncheckOption(ByRouter(selector), msTimeout);

        // CodeceptJS: action: ForceClick
        //
        // See https://www.w3.org/TR/webdriver1/#element-click for all the
        // stuff that a normal .Click() does. This is a way to do a direct
        // click (say if you want to click a hidden element).
        public DOMElement ForceClick(By by, int msTimeout = Banquo.DefaultTimeout)
        {
            var element = WaitForEnabled(by, msTimeout);
            element.ForceClick();
            return element;
        }

        public DOMElement ForceClick(string selector, int msTimeout = Banquo.DefaultTimeout) =>
            ForceClick(ByRouter(selector), msTimeout);

        public User SubmitForm(int index = 0)
        {
            IJavaScriptExecutor submitExecutor = (IJavaScriptExecutor)Driver;
            submitExecutor.ExecuteScript($"document.forms[{index}].submit();");
            return this;
        }

        public User SubmitForm(string id)
        {
            IJavaScriptExecutor submitExecutor = (IJavaScriptExecutor)Driver;
            submitExecutor.ExecuteScript($"document.forms['{id}'].submit();");
            return this;
        }

        public User RefreshPage()
        {
            Navigate().Refresh();
            return this;
        }

        public DOMElement Type(By by, string toType, int msTimeout = Banquo.DefaultTimeout) =>
            WaitForEnabled(by, msTimeout).Type(toType);

        public DOMElement Type(string selector, string toType, int msTimeout = Banquo.DefaultTimeout) =>
            Type(ByRouter(selector), toType, msTimeout);
    }
}