using System;

using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Bumblebee.Setup.DriverEnvironments
{
	public abstract class RemoteDriverEnvironment<TWebDriver> : IDriverEnvironment
		where TWebDriver : IWebDriver, new()
	{
		public RemoteDriverEnvironment(DesiredCapabilities capabilities)
		{
			this.remoteURL = "http://localhost:4444/wd/hub";
			this.capabilities = capabilities;
		}

		public RemoteDriverEnvironment(String remoteURL, DesiredCapabilities capabilities)
		{
			this.remoteURL = remoteURL;
			this.capabilities = capabilities;      
		}

		public virtual IWebDriver CreateWebDriver()
		{
			var driver = new RemoteWebDriver(new Uri(remoteURL), capabilities);
			return driver;
		}
	}
}
