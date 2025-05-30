using Karpatium.Core.Web;

namespace ToolsQa.UI.Elements;

/// <summary>
/// Represents a script HTML element.
/// </summary>
/// <remarks>
/// Provided as an example for implementing elements that are not included in Karpatium.
/// </remarks>
public class ScriptElement : Element
{
    /// <summary>
    /// Gets the "src" attribute of the script element.
    /// </summary>
    public string? Src
    {
        get
        {
            string? src = GetAttribute("src");
            
            return src;
        }
    }
}