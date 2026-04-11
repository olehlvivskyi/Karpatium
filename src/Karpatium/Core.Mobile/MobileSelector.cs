using OpenQA.Selenium;
using OpenQA.Selenium.Appium;

namespace Karpatium.Core.Mobile;

/// <summary>
/// Represents a selector for locating mobile elements.
/// </summary>
public sealed class MobileSelector
{
    internal By ByWrapper { get; }

    private MobileSelector(By by) => ByWrapper = by;

    /// <summary>
    /// Creates a selector using an accessibility ID.
    /// </summary>
    /// <param name="id">The accessibility ID of the element to locate (content-desc on Android, accessibility id on iOS).</param>
    /// <example>
    /// <code>
    /// MobileSelector.AccessibilityId("login_button");
    /// </code>
    /// </example>
    public static MobileSelector AccessibilityId(string id) =>
        new(MobileBy.AccessibilityId(id));

    /// <summary>
    /// Creates a selector using an XPath expression.
    /// </summary>
    /// <param name="xpath">The XPath expression used to locate mobile elements.</param>
    /// <example>
    /// <code>
    /// MobileSelector.XPath("//android.widget.Button[@text='Login']");
    /// </code>
    /// </example>
    public static MobileSelector XPath(string xpath) =>
        new(By.XPath(xpath));

    /// <summary>
    /// Creates a selector using an Android UIAutomator expression.
    /// </summary>
    /// <param name="expression">The UIAutomator expression used to locate the element.</param>
    /// <example>
    /// <code>
    /// MobileSelector.AndroidUiAutomator("new UiSelector().text(\"Login\")");
    /// </code>
    /// </example>
    public static MobileSelector AndroidUiAutomator(string expression) =>
        new(MobileBy.AndroidUIAutomator(expression));

    /// <summary>
    /// Creates a selector using an iOS Class Chain expression.
    /// </summary>
    /// <param name="expression">The iOS Class Chain expression used to locate the element.</param>
    /// <example>
    /// <code>
    /// MobileSelector.IosClassChain("**/XCUIElementTypeButton[`label == 'Login'`]");
    /// </code>
    /// </example>
    public static MobileSelector IosClassChain(string expression) =>
        new(MobileBy.IosClassChain(expression));

    /// <summary>
    /// Creates a selector using an iOS NSPredicate string.
    /// </summary>
    /// <param name="expression">The NSPredicate string used to locate the element.</param>
    /// <example>
    /// <code>
    /// MobileSelector.IosNsPredicate("label == 'Login'");
    /// </code>
    /// </example>
    public static MobileSelector IosNsPredicate(string expression) =>
        new(MobileBy.IosNSPredicate(expression));

    /// <summary>
    /// Returns a string representation of the selector.
    /// </summary>
    public override string ToString() => ByWrapper.ToString();
}