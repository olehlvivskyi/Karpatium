namespace ToolsQa.UI;

/// <summary>
/// Specifies constant values for the Tools QA application.
/// </summary>
public static class ToolsQaConstants
{
    /// <summary>
    /// Specifies constant values for the "Buttons" page.
    /// </summary>
    public static class ButtonsPage
    {
        public const string ClickMeMessageText = "You have done a dynamic click";
        public const string DoubleClickMeMessageText = "You have done a double click";
        public const string RightClickMeMessageText = "You have done a right click";
    }

    /// <summary>
    /// Specifies constant values for the "Dynamic Properties" page.
    /// </summary>
    public static class DynamicPropertiesPage
    {
        public const string InitialColorChangeTextColor = "rgba(255, 255, 255, 1)";
        public const string NextColorChangeTextColor = "rgba(220, 53, 69, 1)";
    }
    
    /// <summary>
    /// Specifies constant values for the "Links" page.
    /// </summary>
    public static class LinksPage
    {
        public const string LinkResponse201 = "Link has responded with staus 201 and status text Created";
        public const string LinkResponse404 = "Link has responded with staus 404 and status text Not Found";
        public const string LinkTabUrl = "https://demoqa.com/";
    }
    
    /// <summary>
    /// Specifies constant values for the "Radio Button" page.
    /// </summary>
    public static class RadioButtonPage
    {
        public const string ImpressiveRadioButtonText = "Impressive";
        public const string YesRadioButtonText = "Yes";
    }
}