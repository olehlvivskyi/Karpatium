using Karpatium.Core.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Remote;

namespace Karpatium.Core.Web.BrowserFactories;

/// <summary>
/// A factory responsible for creating instances of the Edge browser with specific configurations based on the provided settings.
/// </summary>
internal sealed class EdgeBrowserFactory : IBrowserFactory
{
    public Browser CreateBrowser(IBrowserSettings browserSettings)
    {
        EdgeOptions edgeOptions = GetEdgeOptions(browserSettings);
        IWebDriver edgeDriver = GetWebDriver(browserSettings, edgeOptions);
        
        return new Browser(edgeDriver);
    }

    private EdgeOptions GetEdgeOptions(IBrowserSettings browserSettings)
    {
        EdgeOptions edgeOptions = new EdgeOptions();
        
        edgeOptions.AddUserProfilePreference("disable-popup-blocking", true);
        edgeOptions.AddUserProfilePreference("download.default_directory", PathUtils.GetLocalUserPath(browserSettings.DownloadedFilesFolderName));
        edgeOptions.AddUserProfilePreference("download.prompt_for_download", false);

        if (browserSettings.IsHeadlessEnabled)
        {
            edgeOptions.AddArguments("--headless=new");
            edgeOptions.AddArguments("window-size=1920,1080");
        }

        return edgeOptions;
    }

    private IWebDriver GetWebDriver(IBrowserSettings browserSettings, EdgeOptions edgeOptions)
    {
        return browserSettings.IsLocalExecution
            ? GetWebDriverLocal(edgeOptions)
            : GetWebDriverRemote(browserSettings, edgeOptions);
    }

    private IWebDriver GetWebDriverLocal(EdgeOptions edgeOptions) => new EdgeDriver(edgeOptions);

    private IWebDriver GetWebDriverRemote(IBrowserSettings browserSettings, EdgeOptions edgeOptions)
    {
        if (string.IsNullOrWhiteSpace(browserSettings.RemoteUrl) || !Uri.IsWellFormedUriString(browserSettings.RemoteUrl, UriKind.Absolute))
        {
            throw new ArgumentException("RemoteUrl must be a valid, non-empty URL.", nameof(browserSettings.RemoteUrl));
        }
        
        IWebDriver edgeDriver = new RemoteWebDriver(new Uri(browserSettings.RemoteUrl), edgeOptions);
        ((RemoteWebDriver)edgeDriver).FileDetector = new LocalFileDetector();
        
        return edgeDriver;
    }
}