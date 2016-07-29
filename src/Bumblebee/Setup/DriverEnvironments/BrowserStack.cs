using System;

using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Bumblebee.Setup.DriverEnvironments
{
	public class BrowserStack<TWebDriver> : RemoteDriverEnvironment<TWebDriver>
		where TWebDriver : IWebDriver, new()
    {
		public string bsUserName { get; set; }
		public string bsKey { get; set; }

		public BrowserStack(DesiredCapabilities capabilities) : base(capabilities)
        {
            this.remoteURL = "http://hub-cloud.browserstack.com/wd/hub/";

            this.bsUserName = Environment.GetEnvironmentVariable("BROWSERSTACK_USERNAME");
            this.bsKey = Environment.GetEnvironmentVariable("BROWSERSTACK_KEY");

            this.capabilities.SetCapability("browserstack.user", bsUserName);
            this.capabilities.SetCapability("browserstack.key", bsKey);
        }
    }
}
