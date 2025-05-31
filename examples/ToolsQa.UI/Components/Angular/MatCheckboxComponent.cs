using Karpatium.Core.Web;
using Karpatium.Core.Web.Elements;

namespace ToolsQa.UI.Components.Angular;

/// <summary>
/// Represents a Material Checkbox component in a web application.
/// </summary>
/// <remarks>
/// Provided as an example for implementing components.
/// </remarks>
internal sealed class MatCheckboxComponent(CommonElement componentWrapper) : BaseComponent<CommonElement>(componentWrapper)
{
    private CommonElement Label => ElementFactory.Create<CommonElement>(Selector.Tag("label"), ComponentWrapper);
    private CheckBoxElement Input => ElementFactory.Create<CheckBoxElement>(Selector.Tag("input"), ComponentWrapper);
    
    internal string LabelText => Label.Text;

    internal bool IsChecked => Input.IsChecked;
    
    internal void Check() => Input.Check();
}