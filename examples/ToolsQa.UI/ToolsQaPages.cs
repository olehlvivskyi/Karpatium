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
    /// Represents the "CheckBox" page.
    /// </summary>
    public static CheckBoxPage CheckBoxPage => new (ToolsQaPageUrls.CheckBoxPage);
    
    /// <summary>
    /// Represents the "RadioButton" page.
    /// </summary>
    public static RadioButtonPage RadioButtonPage => new (ToolsQaPageUrls.RadioButtonPage);
    
    /// <summary>
    /// Represents the "TextBox" page.
    /// </summary>
    public static TextBoxPage TextBoxPage => new (ToolsQaPageUrls.TextBoxPage);
}