using OpenQA.Selenium;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Remote;

namespace Karpatium.Core.Web.BrowserFactories;

/// <summary>
/// A factory responsible for creating instances of the Safari browser with specific configurations based on the provided settings.
/// </summary>
internal sealed class SafariBrowserFactory : IBrowserFactory
{
    public Browser CreateBrowser(IBrowserSettings browserSettings)
    {
        SafariOptions safariOptions = GetSafariOptions(browserSettings);
        IWebDriver safariDriver = GetWebDriver(browserSettings, safariOptions);
        
        return new Browser(safariDriver);
    }

    private SafariOptions GetSafariOptions(IBrowserSettings browserSettings)
    {
        SafariOptions safariOptions = new SafariOptions();
        
        safariOptions.SetLoggingPreference(LogType.Browser, LogLevel.All);
        safariOptions.SetLoggingPreference(LogType.Driver, LogLevel.All);

        if (browserSettings.IsHeadlessEnabled)
        {
            throw new NotSupportedException("Headless mode is not supported for Safari.");
        }

        return safariOptions;
    }

    private IWebDriver GetWebDriver(IBrowserSettings browserSettings, SafariOptions safariOptions)
    {
        return browserSettings.IsLocalExecution
            ? GetWebDriverLocal(safariOptions)
            : GetWebDriverRemote(browserSettings, safariOptions);
    }

    private IWebDriver GetWebDriverLocal(SafariOptions safariOptions) => new SafariDriver(safariOptions);

    private IWebDriver GetWebDriverRemote(IBrowserSettings browserSettings, SafariOptions safariOptions)
    {
        if (string.IsNullOrWhiteSpace(browserSettings.RemoteUrl) || !Uri.IsWellFormedUriString(browserSettings.RemoteUrl, UriKind.Absolute))
        {
            throw new ArgumentException("RemoteUrl must be a valid, non-empty URL.", nameof(browserSettings.RemoteUrl));
        }
        
        IWebDriver safariDriver = new RemoteWebDriver(new Uri(browserSettings.RemoteUrl), safariOptions);
        ((RemoteWebDriver)safariDriver).FileDetector = new LocalFileDetector();
        
        return safariDriver;
    }
}