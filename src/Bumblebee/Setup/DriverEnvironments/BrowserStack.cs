using System;
using OpenQA.Selenium.Remote;

namespace Bumblebee.Setup.DriverEnvironments
{
    public class BrowserStack : RemoteDriverEnvironment<BrowserStackDriver>
    {
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
