using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;

namespace Karpatium.Core.Mobile.MobileFactories;

internal sealed class IosMobileFactory : IMobileFactory
{
    public Mobile CreateMobile(IMobileSettings mobileSettings)
    {
        var options = new AppiumOptions
        {
            App = mobileSettings.App,
            AutomationName = "XCUITest",
            DeviceName = mobileSettings.DeviceName,
            PlatformVersion = mobileSettings.PlatformVersion,
            PlatformName = "iOS"
        };
        options.AddAdditionalAppiumOption("newCommandTimeout", 120);

        var driver = new IOSDriver(
            new Uri(mobileSettings.AppiumServerUrl),
            options,
            TimeSpan.FromSeconds(120));

        driver.Manage().Timeouts().ImplicitWait =
            TimeSpan.FromSeconds(mobileSettings.ImplicitWaitInSeconds);

        return new Mobile(driver);
    }
}
