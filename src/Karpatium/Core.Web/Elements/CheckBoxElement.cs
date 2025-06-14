using Karpatium.Core.Utilities;

namespace Karpatium.Core.Web.Elements;

/// <summary>
/// Represents an input HTML element with "checkbox" type.
/// </summary>
public class CheckBoxElement : Element
{
    /// <summary>
    /// Gets a value indicating whether the checkbox is currently checked.
    /// </summary>
    public bool IsChecked
    {
        get
        {
            bool isChecked = ConditionalWaiter.ForResult(() => WebElementWrapper.Selected, $"{nameof(CheckBoxElement)}: {nameof(IsChecked)} failed.");
            
            return isChecked;
        }
    }

    /// <summary>
    /// Checks the checkbox.
    /// </summary>
    /// <remarks>
    /// If the checkbox is already checked, this method will not perform any action.
    /// </remarks>
    public void Check()
    {
        if (!IsChecked)
        {
            Click();
        }
    }
    
    /// <summary>
    /// Unchecks the checkbox.
    /// </summary>
    /// <remarks>
    /// If the checkbox is not checked, this method will not perform any action.
    /// </remarks>
    public void Uncheck()
    {
        if (IsChecked)
        {
            Click();
        }
    }
}