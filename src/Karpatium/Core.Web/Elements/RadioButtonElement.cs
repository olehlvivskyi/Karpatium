using Karpatium.Core.Utilities;

namespace Karpatium.Core.Web.Elements;

/// <summary>
/// Represents an input HTML element with "radio" type.
/// </summary>
public sealed class RadioButtonElement : Element
{
    /// <summary>
    /// Gets a value indicating whether the radio button is currently checked.
    /// </summary>
    public bool IsChecked
    {
        get
        {
            bool isChecked = ConditionalWaiter.ForResult(() => WebElementWrapper.Selected, $"{nameof(RadioButtonElement)}: {nameof(IsChecked)} failed.");
            
            return isChecked;
        }
    }
    
    /// <summary>
    /// Checks the radio button.
    /// </summary>
    /// <remarks>
    /// If the radio button is already checked, this method will not perform any action.
    /// </remarks>
    public void Check()
    {
        if (!IsChecked)
        {
            Click();
        }
    }
}