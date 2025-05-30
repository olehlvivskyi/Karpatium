using Karpatium.Core.Web;
using Microsoft.Extensions.Configuration;
using ToolsQa.Tests.TestUsers;

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

    /// <summary>
    /// Represents a collection of user accounts defined in the application settings.
    /// </summary>
    /// <remarks>
    /// Not currently utilized in the application, but serves as an example of using User Pool for parallelization.
    /// </remarks>
    public required IReadOnlyList<User> Users { get; init; }
}

/// <summary>
/// Represents the configuration settings related to test execution.
/// </summary>
[Serializable]
public sealed class TestSettings
{
    /// <summary>
    /// Represents the JavaScript code used to remove an ad plus 1 from a webpage during test execution.
    /// </summary>
    public required string AdPlus1RemovalScript { get; init; }
    
    /// <summary>
    /// Represents the JavaScript code used to remove an ad plus 2 from a webpage during test execution.
    /// </summary>
    public required string AdPlus2RemovalScript { get; init; }
    
    /// <summary>
    /// Represents the JavaScript code used to remove a banner from a webpage during test execution.
    /// </summary>
    public required string BannerRemovalScript { get; init; }

    /// <summary>
    /// Represents the JavaScript code used to remove the footer from a web page during test execution.
    /// </summary>
    public required string FooterRemovalScript { get; init; }

    /// <summary>
    /// Represents the JavaScript code used to remove the right side from a web page during test execution.
    /// </summary>
    public required string RightSideRemovalScript { get; init; }
}

/// <summary>
/// Represents the configuration settings related to the web manager.
/// </summary>
[Serializable]
public sealed class WebManagerSettings : IBrowserSettings
{
    public required BrowserType BrowserType { get; init; }
    public required int DemoModeDelayInMilliseconds { get; init; }
    public required string DownloadedFilesFolderName { get; init; }
    public bool IsDemoModeEnabled { get; init; }
    public required bool IsHeadlessEnabled { get; init; }
    public required bool IsLocalExecution { get; init; }
    public required string RemoteUrl { get; init; }
}