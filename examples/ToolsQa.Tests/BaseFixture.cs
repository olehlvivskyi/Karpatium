using Karpatium.Core.Utilities;
using Karpatium.Core.Web;
using Newtonsoft.Json;
using Serilog;

namespace ToolsQa.Tests;

public abstract class BaseFixture<TTestData>
{
    protected abstract string TestDataPath { get; }
    protected TTestData TestData { get; private set; }
    
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
        
        WebManager.Initialize(TestConfiguration.WebManagerSettings);
        WebManager.Browser.MaximizeWindow();
    }
    
    [OneTimeTearDown]
    public void BaseOneTimeTearDown()
    {
        WebManager.Quit();
    }

    protected void RemoveBannerAndFooter()
    {
        ConditionalRunner.IgnoreException(() => 
            WebManager.Browser.ExecuteJavascript(TestConfiguration.TestSettings.BannerRemovalScript));
        ConditionalRunner.IgnoreException(() => 
            WebManager.Browser.ExecuteJavascript(TestConfiguration.TestSettings.FooterRemovalScript));
    }
    
    private TTestData GetTestDataFromJson()
    {
        string jsonFileName = TestDataPath;
        string jsonText = File.ReadAllText(jsonFileName);
        TTestData testData = JsonConvert.DeserializeObject<TTestData>(jsonText)!;
        return testData;
    }
}