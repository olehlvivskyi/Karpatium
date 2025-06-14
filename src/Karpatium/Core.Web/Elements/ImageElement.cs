using Karpatium.Core.Utilities;

namespace Karpatium.Core.Web.Elements;

/// <summary>
/// Represents an img HTML element.
/// </summary>
public class ImageElement : Element
{
    /// <summary>
    /// Gets the "alt" attribute of the image element.
    /// </summary>
    public string? Alt
    {
        get
        {
            string? src = GetAttribute("src");
            
            return src;
        }
    }
    
    /// <summary>
    /// Indicates whether the image is valid.
    /// </summary>
    /// <remarks>
    /// The property utilizes "naturalWidth" attribute.
    /// </remarks>
    public bool IsImageValid
    {
        get
        {
            bool isImageValid = ConditionalWaiter.ForResult(() 
                    => int.TryParse(GetAttribute("naturalWidth"), out int result) && result > 0, 
                $"{nameof(ImageElement)}: {nameof(IsImageValid)} failed.");
            
            return isImageValid;
        }
    }

    /// <summary>
    /// Gets the "src" attribute of the image element.
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