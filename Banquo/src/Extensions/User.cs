using System.Collections.ObjectModel;
using OpenQA.Selenium;

namespace Banquo.Extensions
{
    public partial class User : IWebDriver
    {
        private readonly IWebDriver driver;

        public string Url { get => driver.Url; set => driver.Url = value; }

        public string Title { get => driver.Title; }

        public string PageSource { get => driver.PageSource; }

        public string CurrentWindowHandle { get => driver.CurrentWindowHandle; }

        public ReadOnlyCollection<string> WindowHandles { get => driver.WindowHandles; }

        public IWebDriver Driver => driver;

        public User(IWebDriver driver) => this.driver = driver;

        public void Close() => driver.Close();

        public void Quit() => driver.Quit();

        public IOptions Manage() => driver.Manage();

        public INavigation Navigate() => driver.Navigate();

        public ITargetLocator SwitchTo() => driver.SwitchTo();

        public IWebElement FindElement(By by) => driver.FindElement(by);

        public ReadOnlyCollection<IWebElement> FindElements(By by) => driver.FindElements(by);

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                driver.Dispose();
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            System.GC.SuppressFinalize(this);
        }
    }
}