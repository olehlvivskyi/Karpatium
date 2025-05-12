using Karpatium.Core.Web;
using Microsoft.Extensions.Configuration;

namespace ToolsQa.Tests;

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
    
    public static ApplicationSettings ApplicationSettings => LazyConfig
        .Value
        .GetSection("ApplicationSettings")
        .Get<ApplicationSettings>()!;
    
    public static TestSettings TestSettings => LazyConfig
        .Value
        .GetSection("TestSettings")
        .Get<TestSettings>()!;
    
    public static WebManagerSettings WebManagerSettings => LazyConfig
        .Value
        .GetSection("WebManagerSettings")
        .Get<WebManagerSettings>()!;
}

[Serializable]
public sealed class ApplicationSettings
{
    public required string BaseUrl { get; init; }
}

[Serializable]
public sealed class TestSettings
{
    public required string BannerRemovalScript { get; init; }
    public required string FooterRemovalScript { get; init; }
}

[Serializable]
public sealed class WebManagerSettings : IBrowserSettings
{
    public required BrowserType BrowserType { get; init; }
    public required bool IsHeadlessEnabled { get; init; }
    public required bool IsLocalExecution { get; init; }
    public required string RemoteUrl { get; init; }
}