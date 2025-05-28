using Karpatium.Core.Web;
using Microsoft.Extensions.Configuration;

namespace ToolsQa.Tests;

/// <summary>
/// Provides access to application, test, and web manager configurations.
/// </summary>
public static class TestConfiguration
{
    private const string JsonFilePath = "testconfiguration.json";
    
    private static readonly Lazy<IConfiguration> LazyConfig = new (() =>
    {
        IConfigurationBuilder builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(JsonFilePath)
            .AddEnvironmentVariables();

        IConfiguration config = builder.Build();
        return config;
    });

    /// <summary>
    /// Represents the configuration settings related to the application.
    /// </summary>
    public static ApplicationSettings ApplicationSettings => LazyConfig
        .Value
        .GetSection("ApplicationSettings")
        .Get<ApplicationSettings>()!;

    /// <summary>
    /// Represents the configuration settings related to test execution.
    /// </summary>
    public static TestSettings TestSettings => LazyConfig
        .Value
        .GetSection("TestSettings")
        .Get<TestSettings>()!;
    
    /// <summary>
    /// Represents the configuration settings related to the web manager.
    /// </summary>
    public static WebManagerSettings WebManagerSettings => LazyConfig
        .Value
        .GetSection("WebManagerSettings")
        .Get<WebManagerSettings>()!;
}

/// <summary>
/// Represents the configuration settings related to the application.
/// </summary>
[Serializable]
public sealed class ApplicationSettings
{
    /// <summary>
    /// Represents the base URL of the application, used to construct full navigation paths.
    /// </summary>
    public required string BaseUrl { get; init; }
}

/// <summary>
/// Represents the configuration settings related to test execution.
/// </summary>
[Serializable]
public sealed class TestSettings
{
    /// <summary>
    /// Represents the JavaScript code used to remove a banner from a webpage during test execution.
    /// </summary>
    public required string BannerRemovalScript { get; init; }

    /// <summary>
    /// Represents the JavaScript code used to remove the footer from a web page during test execution.
    /// </summary>
    public required string FooterRemovalScript { get; init; }
}

/// <summary>
/// Represents the configuration settings related to the web manager.
/// </summary>
[Serializable]
public sealed class WebManagerSettings : IBrowserSettings
{
    public required BrowserType BrowserType { get; init; }
    public required string DownloadedFilesFolderName { get; init; }
    public required bool IsHeadlessEnabled { get; init; }
    public required bool IsLocalExecution { get; init; }
    public required string RemoteUrl { get; init; }
}