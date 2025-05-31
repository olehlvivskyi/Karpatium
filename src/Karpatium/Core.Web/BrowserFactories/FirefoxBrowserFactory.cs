using Karpatium.Core.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;

namespace Karpatium.Core.Web.BrowserFactories;

/// <summary>
/// A factory responsible for creating instances of the Firefox browser with specific configurations based on the provided settings.
/// </summary>
internal sealed class FirefoxBrowserFactory : IBrowserFactory
{
    public Browser CreateBrowser(IBrowserSettings browserSettings)
    {
        FirefoxOptions firefoxOptions = GetFirefoxOptions(browserSettings);
        IWebDriver firefoxDriver = GetWebDriver(browserSettings, firefoxOptions);
        
        return new Browser(firefoxDriver);
    }

    private FirefoxOptions GetFirefoxOptions(IBrowserSettings browserSettings)
    {
        FirefoxOptions firefoxOptions = new FirefoxOptions();
        
        firefoxOptions.SetLoggingPreference(LogType.Browser, LogLevel.All);
        firefoxOptions.SetLoggingPreference(LogType.Driver, LogLevel.All);
        
        firefoxOptions.SetPreference("browser.download.folderList", 2);
        firefoxOptions.SetPreference("browser.download.dir", PathUtils.GetLocalUserPath(browserSettings.DownloadedFilesFolderName));
        firefoxOptions.SetPreference("browser.download.useDownloadDir", "true");
        firefoxOptions.SetPreference("browser.helperApps.neverAsk.saveToDisk", "application/pdf,application/zip,application/octet-stream,text/csv,application/vnd.ms-excel,image/jpeg,image/png,image/gif,image/bmp,image/webp,image/svg+xml,image/tiff,image/x-icon,image/pjpeg");
        firefoxOptions.SetPreference("browser.download.manager.showWhenStarting", false);
        firefoxOptions.SetPreference("browser.helperApps.alwaysAsk.force", false);
        
        if (browserSettings.IsHeadlessEnabled)
        {
            firefoxOptions.AddArgument("-headless");
            firefoxOptions.AddArgument("--width=1920");
            firefoxOptions.AddArgument("--height=1080");
        }

        return firefoxOptions;
    }

    private IWebDriver GetWebDriver(IBrowserSettings browserSettings, FirefoxOptions firefoxOptions)
    {
        return browserSettings.IsLocalExecution
            ? GetWebDriverLocal(firefoxOptions)
            : GetWebDriverRemote(browserSettings, firefoxOptions);
    }
    
    private IWebDriver GetWebDriverLocal(FirefoxOptions firefoxOptions) => new FirefoxDriver(firefoxOptions);
    
    private IWebDriver GetWebDriverRemote(IBrowserSettings browserSettings, FirefoxOptions firefoxOptions)
    {
        if (string.IsNullOrWhiteSpace(browserSettings.RemoteUrl) || !Uri.IsWellFormedUriString(browserSettings.RemoteUrl, UriKind.Absolute))
        {
            throw new ArgumentException("RemoteUrl must be a valid, non-empty URL.", nameof(browserSettings.RemoteUrl));
        }
        
        IWebDriver firefoxDriver = new RemoteWebDriver(new Uri(browserSettings.RemoteUrl), firefoxOptions);
        ((RemoteWebDriver)firefoxDriver).FileDetector = new LocalFileDetector();
        
        return firefoxDriver;
    }
}