using Karpatium.Core.Utilities;
using Karpatium.Core.Web;
using ToolsQa.UI;

namespace ToolsQa.Tests.TestFixtures.Elements;

public class UploadDownloadFixture : BaseFixture<EmptyTestData>
{
    private const int FileDownloadTimeoutInMilliseconds = 5000;
    
    protected override string TestDataPath => string.Empty;
    
    [OneTimeSetUp]
    public void UploadDownloadOneTimeSetUp()
    {
        WebManager.Browser.NavigateTo($"{TestConfiguration.ApplicationSettings.BaseUrl}{ToolsQaPageUrls.UploadDownloadPage}");
    }
    
    [Test]
    public void EnsureThatFileIsDownloaded()
    {
        string fileName = ToolsQaPages.UploadDownloadPage.DownloadFileName;
        string image = PathUtils.GetLocalUserPath(TestConfiguration.WebManagerSettings.DownloadedFilesFolderName, fileName);
        File.Delete(image);
        
        ToolsQaPages.UploadDownloadPage.ClickDownload();
        ConditionalWaiter.ForTrueIfPossible(() => File.Exists(image), FileDownloadTimeoutInMilliseconds);
        
        Assert.That(File.Exists(image), Is.True, "File is not downloaded.");
    }
    
    [Test]
    public void EnsureThatFileIsUploaded()
    {
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "TestData", "Elements", "test.jpg");
        
        ToolsQaPages.UploadDownloadPage.UploadFile(filePath);
        
        Assert.That(ToolsQaPages.UploadDownloadPage.UploadedFilePathText, Contains.Substring(Path.GetFileName(filePath)), "Incorrect file path.");
    }
}