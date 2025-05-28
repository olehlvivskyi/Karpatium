using Karpatium.Core.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace Karpatium.Core.Web;

/// <summary>
/// A factory responsible for creating instances of the Chrome browser with specific configurations
/// based on the provided settings.
/// </summary>
internal sealed class ChromeBrowserFactory : IBrowserFactory
{
    /// <summary>
    /// Creates and configures a browser instance based on the provided browser settings.
    /// </summary>
    /// <param name="browserSettings">The settings that define browser behaviors.</param>
    /// <returns>Returns a <see cref="Browser"/> object configured as per the specified settings.</returns>
    public Browser CreateBrowser(IBrowserSettings browserSettings)
    {
        var chromeOptions = GetChromeOptions(browserSettings);
        var chromeDriver = GetWebDriver(browserSettings, chromeOptions);
        return new Browser(chromeDriver);
    }

    private ChromeOptions GetChromeOptions(IBrowserSettings browserSettings)
    {
        var chromeOptions = new ChromeOptions();

        chromeOptions.AddUserProfilePreference("disable-popup-blocking", true);
        chromeOptions.AddUserProfilePreference("download.default_directory", PathUtils.GetLocalUserPath(browserSettings.DownloadedFilesFolderName));
        chromeOptions.AddUserProfilePreference("download.prompt_for_download", false);

        if (browserSettings.IsHeadlessEnabled)
        {
            chromeOptions.AddArguments("--headless=new");
            chromeOptions.AddArguments("window-size=1920,1080");
        }

        return chromeOptions;
    }

    private IWebDriver GetWebDriver(IBrowserSettings browserSettings, ChromeOptions chromeOptions)
    {
        return browserSettings.IsLocalExecution
            ? GetWebDriverLocal(chromeOptions)
            : GetWebDriverRemote(browserSettings, chromeOptions);
    }

    private IWebDriver GetWebDriverLocal(ChromeOptions chromeOptions) => new ChromeDriver(chromeOptions);

    private IWebDriver GetWebDriverRemote(IBrowserSettings browserSettings, ChromeOptions chromeOptions)
    {
        if (string.IsNullOrWhiteSpace(browserSettings.RemoteUrl) 
            || !Uri.IsWellFormedUriString(browserSettings.RemoteUrl, UriKind.Absolute))
        {
            throw new ArgumentException("RemoteUrl must be a valid, non-empty URL.", nameof(browserSettings.RemoteUrl));
        }
        
        IWebDriver chromeDriver = new RemoteWebDriver(new Uri(browserSettings.RemoteUrl), chromeOptions);
        ((RemoteWebDriver)chromeDriver).FileDetector = new LocalFileDetector();
        return chromeDriver;
    }
}