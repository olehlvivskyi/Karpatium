using Karpatium.Core.Utilities;

namespace Karpatium.Core.Mobile.MobileElements;

/// <summary>
/// Represents a common mobile element, such as a label or a generic view.
/// </summary>
public class MobileCommonElement : MobileElement
{
    /// <summary>
    /// Gets the text content of the associated mobile element.
    /// </summary>
    public string Text =>
        ConditionalWaiter.ForResult(() => WebElementWrapper.Text, $"{nameof(MobileCommonElement)}: {nameof(Text)} failed.");
}
