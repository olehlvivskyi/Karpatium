using OpenQA.Selenium;
using OpenQA.Selenium.Appium;

namespace Karpatium.Core.Mobile;

/// <summary>
/// Represents a mobile driver instance for managing test execution.
/// </summary>
internal sealed class Mobile(AppiumDriver driver) : IMobile
{
    private AppiumDriver DriverWrapper { get; } = driver;

    internal IWebElement FindElement(MobileSelector selector) => DriverWrapper.FindElement(selector.ByWrapper);

    internal IReadOnlyList<IWebElement> FindElements(MobileSelector selector) => DriverWrapper.FindElements(selector.ByWrapper);

    public TimeSpan ImplicitWait
    {
        get => DriverWrapper.Manage().Timeouts().ImplicitWait;
        set => DriverWrapper.Manage().Timeouts().ImplicitWait = value;
    }

    public string PageSource => DriverWrapper.PageSource;

    public void BackgroundApp(TimeSpan duration) => DriverWrapper.BackgroundApp(duration);

    public void CloseApp(string appId) => DriverWrapper.TerminateApp(appId);

    public void HideKeyboard() => DriverWrapper.HideKeyboard();

    public void LaunchApp(string appId) => DriverWrapper.ActivateApp(appId);

    public void Quit() => DriverWrapper.Quit();
}
