using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace Karpatium.Core.Mobile.MobileFactories;

internal sealed class AndroidMobileFactory : IMobileFactory
{
    public Mobile CreateMobile(IMobileSettings mobileSettings)
    {
        var options = new AppiumOptions
        {
            App = mobileSettings.App,
            AutomationName = "UiAutomator2",
            DeviceName = mobileSettings.DeviceName,
            PlatformVersion = mobileSettings.PlatformVersion,
            PlatformName = "Android"
        };
        options.AddAdditionalAppiumOption("newCommandTimeout", 120);

        var driver = new AndroidDriver(
            new Uri(mobileSettings.AppiumServerUrl),
            options,
            TimeSpan.FromSeconds(120));

        driver.Manage().Timeouts().ImplicitWait =
            TimeSpan.FromSeconds(mobileSettings.ImplicitWaitInSeconds);

        return new Mobile(driver);
    }
}
