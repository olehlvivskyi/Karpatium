using Karpatium.Core.Web;

namespace ToolsQa.Tests;

/*
 * The ToolsQa.Tests project is currently in its initial draft stage, created primarily to validate the framework
 * and page object structure.
 * At this stage, it lacks configuration, structured layering, and additional functionality, which will be introduced
 * in upcoming commits.
 * Stay tuned for further updates and improvements.
 */
public class TestConfiguration : IBrowserSettings
{
    public BrowserType BrowserType => BrowserType.Chrome;
    public bool IsHeadlessEnabled => false;
    public bool IsLocalExecution => true;
    public string RemoteUrl => string.Empty;
}