using Allure.Net.Commons;
using Allure.NUnit;
using Allure.NUnit.Attributes;
using Bogus;
using Karpatium.Core.Utilities;
using Karpatium.Core.Web;
using Newtonsoft.Json;
using NUnit.Framework.Interfaces;
using Serilog;
using ToolsQa.Tests.TestUsers;

[assembly:LevelOfParallelism(1)]

namespace ToolsQa.Tests;

[AllureNUnit]
[Parallelizable(ParallelScope.Fixtures)]
public abstract class BaseFixture<TTestData>
{
    protected readonly Faker Faker = new();
    protected abstract string TestDataPath { get; }
    protected TTestData TestData { get; private set; }
    protected User TestCaseUser { get; private set; }
    
    [AllureBefore]
    [OneTimeSetUp]
    public void BaseOneTimeSetUp()
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .MinimumLevel.Verbose()
            .CreateLogger();

        TestData = typeof(TTestData) == typeof(EmptyTestData)
            ? default!
            : GetTestDataFromJson();
        
        AllureReporter.CreateEnvironmentPropertiesFile();
        
        TestCaseUser = GetTestCaseUser();
        Log.Information($"Test user: `{TestCaseUser.Email}`");
        
        WebManager.Initialize(TestConfiguration.WebManagerSettings);
        WebManager.Browser.MaximizeWindow();
    }

    [AllureBefore]
    [SetUp]
    public void BaseSetUp()
    {
        Log.Verbose($"### Test Started: {GetTestName()} ###");
    }

    [AllureAfter]
    [TearDown]
    public void BaseTearDown()
    {
        if (!TestContext.CurrentContext.Result.Outcome.Equals(ResultState.Success))
        {
            string fileName = $"{TestContext.CurrentContext.Test.MethodName} - {DateTime.UtcNow:yyyy.MM.dd-hh.mm.ss}";
            string pageSourcePath = WebManager.DebugPageSource(fileName);
            string screenshotPath = WebManager.DebugScreenshot(fileName);
            Log.Error($"### Test Failed: {GetTestName()} ###");
            Log.Verbose($"### Page source: {pageSourcePath} ###");
            Log.Verbose($"### Screenshot: {screenshotPath} ### ");
            AllureApi.AddAttachment(pageSourcePath);
            AllureApi.AddAttachment(screenshotPath);
        }
        else
        {
            Log.Verbose($"### Test Passed: {GetTestName()} ###");
        }
    }
    
    [AllureAfter]
    [OneTimeTearDown]
    public void BaseOneTimeTearDown()
    {
        UsersPool.Return(TestCaseUser);
        
        WebManager.Quit();
    }
    
    protected void RemoveBanners()
    {
        ConditionalRunner.IgnoreException(() => WebManager.Browser.ExecuteJavascript(TestConfiguration.TestSettings.AdPlus1RemovalScript));
        ConditionalRunner.IgnoreException(() => WebManager.Browser.ExecuteJavascript(TestConfiguration.TestSettings.AdPlus2RemovalScript));
        ConditionalRunner.IgnoreException(() => WebManager.Browser.ExecuteJavascript(TestConfiguration.TestSettings.BannerRemovalScript));
        ConditionalRunner.IgnoreException(() => WebManager.Browser.ExecuteJavascript(TestConfiguration.TestSettings.FooterRemovalScript));
        ConditionalRunner.IgnoreException(() => WebManager.Browser.ExecuteJavascript(TestConfiguration.TestSettings.RightSideRemovalScript));
    }

    private User GetTestCaseUser()
    {
        User user = ConditionalWaiter.ForResult(() =>
        {
            User? user = UsersPool.Get();
            return user ?? throw new Exception("User is not available.");
        }, "User is not available.");

        return user;
    }
    
    private TTestData GetTestDataFromJson()
    {
        string jsonFileName = TestDataPath;
        string jsonText = File.ReadAllText(jsonFileName);
        TTestData testData = JsonConvert.DeserializeObject<TTestData>(jsonText)!;
        return testData;
    }

    private string GetTestName() => $"{TestContext.CurrentContext.Test.DisplayName}.{TestContext.CurrentContext.Test.MethodName}";
}