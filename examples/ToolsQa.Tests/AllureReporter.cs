using System.Text;

namespace ToolsQa.Tests;

public static class AllureReporter
{
    private const string AllureFolder = "allure-results";
    private const string EnvironmentPropertiesFileName = "environment.properties";

    public static void CreateEnvironmentPropertiesFile()
    {
        var environmentPropertiesContent = GetEnvironmentPropertiesContent();

        Directory.CreateDirectory(AllureFolder);
        string filePath = Path.Combine(AllureFolder, EnvironmentPropertiesFileName);

        File.WriteAllText(filePath, environmentPropertiesContent);
    }

    private static string GetEnvironmentPropertiesContent()
    {
        var baseUrl = TestConfiguration.ApplicationSettings.BaseUrl;
        var browserType = TestConfiguration.WebManagerSettings.BrowserType;
        var osVersion = Environment.OSVersion.ToString();
        var dotnetVersion = Environment.Version.ToString();
        var userInteractive = Environment.UserInteractive;

        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine($"BaseUrl={baseUrl}");
        stringBuilder.AppendLine($"BrowserType={browserType}");
        stringBuilder.AppendLine($"OperatingSystem={osVersion}");
        stringBuilder.AppendLine($"DotNetVersion={dotnetVersion}");
        stringBuilder.AppendLine($"UserInteractive={userInteractive}");

        return stringBuilder.ToString();
    }
}