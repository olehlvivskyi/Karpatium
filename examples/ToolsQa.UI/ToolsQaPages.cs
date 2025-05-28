using ToolsQa.UI.Pages;

namespace ToolsQa.UI;

/// <summary>
/// Provides access to the different pages in the Tools QA application.
/// </summary>
/// <remarks>
/// This class serves as a central entry point for accessing specific pages.
/// </remarks>
public static class ToolsQaPages
{
    /// <summary>
    /// Provides access to the "Broken" page.
    /// </summary>
    public static BrokenPage BrokenPage => new (ToolsQaPageUrls.BrokenPage);
    
    /// <summary>
    /// Provides access to the "Buttons" page.
    /// </summary>
    public static ButtonsPage ButtonsPage => new (ToolsQaPageUrls.ButtonsPage);

    /// <summary>
    /// Provides access to the "CheckBox" page.
    /// </summary>
    public static CheckBoxPage CheckBoxPage => new (ToolsQaPageUrls.CheckBoxPage);

    /// <summary>
    /// Provides access to the "Dynamic Properties" page.
    /// </summary>
    public static DynamicPropertiesPage DynamicPropertiesPage => new (ToolsQaPageUrls.DynamicPropertiesPage);

    /// <summary>
    /// Provides access to the "Links" page.
    /// </summary>
    public static LinksPage LinksPage => new (ToolsQaPageUrls.LinksPage);
    
    /// <summary>
    /// Provides access to the "Radio Button" page.
    /// </summary>
    public static RadioButtonPage RadioButtonPage => new (ToolsQaPageUrls.RadioButtonPage);
    
    /// <summary>
    /// Provides access to the "Text Box" page.
    /// </summary>
    public static TextBoxPage TextBoxPage => new (ToolsQaPageUrls.TextBoxPage);
    
    /// <summary>
    /// Provides access to the "Upload Download" page.
    /// </summary>
    public static UploadDownloadPage UploadDownloadPage => new (ToolsQaPageUrls.UploadDownloadPage);
    
    /// <summary>
    /// Provides access to the "Web Tables" page.
    /// </summary>
    public static WebTablesPage WebTablesPage => new (ToolsQaPageUrls.WebTablesPage);
}