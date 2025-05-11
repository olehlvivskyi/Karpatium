using Karpatium.Core.Web;
using Serilog;

namespace ToolsQa.Tests;

/*
 * The ToolsQa.Tests project is currently in its initial draft stage, created primarily to validate the framework
 * and page object structure.
 * At this stage, it lacks configuration, structured layering, and additional functionality, which will be introduced
 * in upcoming commits.
 * Stay tuned for further updates and improvements.
 */
public abstract class BaseFixture
{
    [OneTimeSetUp]
    public void BaseOneTimeSetUp()
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .MinimumLevel.Verbose()
            .CreateLogger();
        
        WebManager.Initialize(new TestConfiguration());
        WebManager.Browser.MaximizeWindow();
        WebManager.Browser.NavigateTo("https://demoqa.com");
    }
    
    [OneTimeTearDown]
    public void BaseOneTimeTearDown()
    {
        WebManager.Quit();
    }
}