using System;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;

namespace Banquo.Support
{
    public class WebDriverFactory
    {
        private const string LOCAL = "LOCAL";
        private const string CHROME = "CHROME";
        private const string REMOTE = "REMOTE";
        private const string EDGE = "EDGE";
        private const string FIREFOX = "FIREFOX";

        private readonly DriverParams driverParams;

        public WebDriverFactory(string driverParamsJson)
            : this(LoadParams(driverParamsJson)) { }

        public WebDriverFactory(DriverParams driverParams)
        {
            this.driverParams = driverParams;
            if (string.IsNullOrEmpty(driverParams.Binaries) || driverParams.Binaries == ".")
            {
                driverParams.Binaries = Environment.CurrentDirectory;
            }

            if (string.IsNullOrEmpty(driverParams.Driver))
            {
                driverParams.Driver = CHROME;
            }

            if (string.IsNullOrEmpty(driverParams.Source))
            {
                driverParams.Source = LOCAL;
            }
        }

        /// <summary>
        /// Generates WebDriver instance based on input parameters
        /// </summary>
        /// <returns>WebDriver instance</returns>
        public IWebDriver Get() =>
            (driverParams.Source.ToUpper() != REMOTE) ? GetDriver() : GetRemoteDriver();

        // Local web drivers
        private IWebDriver GetChrome() => new ChromeDriver(driverParams.Binaries);

        private IWebDriver GetFirefox() => new FirefoxDriver(driverParams.Binaries);

        private IWebDriver GetEdge() => new EdgeDriver(driverParams.Binaries);

        private IWebDriver GetDriver()
        {
            switch (driverParams.Driver.ToUpper())
            {
                case EDGE: return GetEdge();
                case FIREFOX: return GetFirefox();
                case CHROME:
                default: return GetChrome();
            }
        }

        // Remote web drivers
        private IWebDriver GetRemoteChrome() =>
            new RemoteWebDriver(new Uri(driverParams.Binaries), new ChromeOptions());

        private IWebDriver GetRemoteFirefox() =>
            new RemoteWebDriver(new Uri(driverParams.Binaries), new FirefoxOptions());

        private IWebDriver GetRemoteEdge()
        {
            return new RemoteWebDriver(new Uri(driverParams.Binaries), new EdgeOptions());
        }

        private IWebDriver GetRemoteDriver()
        {
            switch (driverParams.Driver.ToUpper())
            {
                case EDGE: return GetRemoteEdge();
                case FIREFOX: return GetRemoteFirefox();
                case CHROME:
                default: return GetRemoteChrome();
            }
        }

        // Load JSon into DriverParams object
        private static DriverParams LoadParams(string driverParamsJson)
        {
            if (string.IsNullOrEmpty(driverParamsJson))
            {
                return new DriverParams { };
            }
            return JsonConvert.DeserializeObject<DriverParams>(driverParamsJson);
        }
    }
}