using Karpatium.Core.Web;
using Karpatium.Core.Web.Elements;
using Serilog;

namespace ToolsQa.UI.Pages;

/// <summary>
/// Provides access to the "Upload Download" page.
/// </summary>
public sealed class UploadDownloadPage(string relativePath) : BasePage(relativePath)
{
    private const string PageName = nameof(UploadDownloadPage);
    
    private CommonElement Download => ElementFactory.Create<CommonElement>(Selector.Id("downloadButton"));
    private InputElement Upload => ElementFactory.Create<InputElement>(Selector.Id("uploadFile"));
    private CommonElement UploadedFilePath => ElementFactory.Create<CommonElement>(Selector.Id("uploadedFilePath"));
    
    
    /// <summary>
    /// Gets the file name of the downloaded file.
    /// </summary>
    public string DownloadFileName
    {
        get
        {
            Log.Information("{PageName}: Checking the file name of the downloaded file.", PageName);
            string downloadFileName = Download.GetAttribute("download")!;
            Log.Debug("{PageName}: {MemberName} = `{MemberValue}`", PageName, nameof(DownloadFileName), downloadFileName);
            
            return downloadFileName;
        }
    }
    
    /// <summary>
    /// Gets the text of the uploaded file path.
    /// </summary>
    public string UploadedFilePathText
    {
        get
        {
            Log.Information("{PageName}: Checking the text of the uploaded file path.", PageName);
            
            string uploadedFilePathText = UploadedFilePath.Text;
            Log.Debug("{PageName}: {MemberName} = `{MemberValue}`", PageName, nameof(UploadedFilePathText), uploadedFilePathText); 
            
            return uploadedFilePathText;
        }
    }
    
    /// <summary>
    /// Clicks on the "Download" button.
    /// </summary>
    public void ClickDownload()
    {
        Log.Information("{PageName}: Clicking on the \"Download\" button.", PageName);
        
        Download.Click();
    }

    /// <summary>
    /// Uploads a file.
    /// </summary>
    /// <param name="filePath">The full path of the file to upload.</param>
    public void UploadFile(string filePath)
    {
        Log.Information("{PageName}: Uploading `{FilePath}` file.", PageName, filePath);
        
        Upload.SetText(filePath);
    }
}