using Allure.NUnit.Attributes;
using Karpatium.Core.Nunit;
using Karpatium.Core.Utilities;
using Karpatium.Core.Web;
using ToolsQa.UI;

namespace ToolsQa.Tests.TestCases;

[AllureParentSuite("Tools QA")]
[AllureSuite("Elements")]
[AllureSubSuite("Upload Download")]
[TestFixture]
public sealed class UploadDownloadTests : BaseFixture<EmptyTestData>
{
    private const int FileDownloadTimeoutInSeconds = 5;

    protected override string TestDataPath => string.Empty;

    private string _image = null!;
    
    [AllureBefore]
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        WebManager.Browser.NavigateTo($"{TestConfiguration.ApplicationSettings.BaseUrl}{ToolsQaPageUrls.UploadDownloadPage}");
        RemoveBanners();
    }

    [AllureAfter]
    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        if (File.Exists(_image))
        {
            File.Delete(_image);
        }
    }
    
    [TestCase(TestName = "Ensure that file is uploaded."), Order(1)]
    [RetryOnErrorAndFailure(3)]
    public void EnsureThatFileIsUploaded()
    {
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "TestData", "test.jpg");
        
        ToolsQaPages.UploadDownloadPage.UploadFile(filePath);
        
        Assert.That(ToolsQaPages.UploadDownloadPage.UploadedFilePathText, Contains.Substring(Path.GetFileName(filePath)), "Incorrect file path.");
    }
    
    [TestCase(TestName = "Ensure that file is downloaded."), Order(2)]
    [RetryOnErrorAndFailure(3)]
    public void EnsureThatFileIsDownloaded()
    {
        string fileName = ToolsQaPages.UploadDownloadPage.DownloadFileName;
        _image = PathUtils.GetLocalUserPath(TestConfiguration.WebManagerSettings.DownloadedFilesFolderName, fileName);
        
        ToolsQaPages.UploadDownloadPage.ClickDownload();
        ConditionalWaiter.ForTrueIfPossible(() => File.Exists(_image), FileDownloadTimeoutInSeconds);
        
        Assert.That(File.Exists(_image), Is.True, "File is not downloaded.");
    }
}