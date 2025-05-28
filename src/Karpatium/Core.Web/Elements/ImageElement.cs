using Karpatium.Core.Utilities;

namespace Karpatium.Core.Web.Elements;

/// <summary>
/// Represents an img HTML element.
/// </summary>
public sealed class ImageElement : Element
{
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
                $"{nameof(ImageElement)}: {nameof(IsImageValid)} failed.");;
            
            return isImageValid;
        }
    }
}