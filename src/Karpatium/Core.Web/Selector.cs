using OpenQA.Selenium;

namespace Karpatium.Core.Web;

/// <summary>
/// Represents a selector to find and interact with UI elements.
/// </summary>
public sealed class Selector
{
    internal By ByWrapper { get; }

    private Selector(By by) => ByWrapper = by;

    /// <summary>
    /// Creates a selector for finding elements by their HTML class attribute.
    /// </summary>
    /// <param name="className">The class name of the HTML element to locate.</param>
    /// <example>
    /// Given the following HTML:
    /// <code>
    /// &lt;div class="content-box"&gt;Hello, World!&lt;/div&gt;
    /// </code>
    /// You can locate the div element using:
    /// <code>
    /// Selector.Class("content-box");
    /// </code>
    /// </example>
    public static Selector Class(string className)
    {
        var selector = By.ClassName(className);
        return new Selector(selector);
    }
    
    /// <summary>
    /// Creates a selector for finding elements using a CSS selector.
    /// </summary>
    /// <param name="css">The CSS selector string used to locate HTML elements.</param>
    /// <example>
    /// Given the following HTML:
    /// <code>
    /// &lt;button class="submit-button"&gt;Submit&lt;/button&gt;
    /// </code>
    /// You can locate the button using:
    /// <code>
    /// Selector.Css(".submit-button");
    /// </code>
    /// </example>
    public static Selector Css(string css)
    {
        var selector = By.CssSelector(css);
        return new Selector(selector);
    }

    /// <summary>
    /// Creates a selector for finding elements using the data-testid attribute.
    /// </summary>
    /// <param name="dataTestId">The value of the data-testid attribute to locate the HTML element.</param>
    /// <example>
    /// Given the following HTML:
    /// <code>
    /// &lt;button data-testid="submit-button"&gt;Submit&lt;/button&gt;
    /// </code>
    /// You can locate the button element using:
    /// <code>
    /// Selector.DataTestId("submit-button");
    /// </code>
    /// </example>
    public static Selector DataTestId(string dataTestId)
    {
        var selector = By.CssSelector($"[data-testid='{dataTestId}']");
        return new Selector(selector);
    }
    
    /// <summary>
    /// Creates a selector for finding elements by their HTML ID attribute.
    /// </summary>
    /// <param name="id">The ID value of the HTML element to locate.</param>
    /// <example>
    /// Given the following HTML:
    /// <code>
    /// &lt;button id="submit-button"&gt;Submit&lt;/button&gt;
    /// </code>
    /// You can locate this element using:
    /// <code>
    /// Selector.Id("submit-button");
    /// </code>
    /// </example>
    public static Selector Id(string id)
    {
        var selector = By.Id(id);
        return new Selector(selector);
    }

    /// <summary>
    /// Creates a selector for finding elements by their HTML `name` attribute.
    /// </summary>
    /// <param name="name">The value of the `name` attribute of the HTML element to locate.</param>
    /// <example>
    /// Given the following HTML:
    /// <code>
    /// &lt;input name="username" type="text"&gt;
    /// </code>
    /// You can locate the username input field using:
    /// <code>
    /// Selector.Name("username");
    /// </code>
    /// </example>
    public static Selector Name(string name)
    {
        var selector = By.Name(name);
        return new Selector(selector);
    }

    /// <summary>
    /// Creates a selector for finding elements by their HTML tag name.
    /// </summary>
    /// <param name="tag">The name of the HTML tag to locate (e.g., "div", "button", "input").</param>
    /// <example>
    /// Given the following HTML:
    /// <code>
    /// &lt;button&gt;Submit&lt;/button&gt;
    /// </code>
    /// You can locate `button` element using:
    /// <code>
    /// Selector.Tag("button");
    /// </code>
    /// </example>
    public static Selector Tag(string tag)
    {
        var selector = By.TagName(tag);
        return new Selector(selector);
    }

    /// <summary>
    /// Creates a selector for finding elements using an XPath expression.
    /// </summary>
    /// <param name="xpath">The XPath expression used to locate HTML elements.</param>
    /// <example>
    /// Given the following HTML:
    /// <code>
    /// &lt;button type="submit"&gt;Login&lt;/button&gt;
    /// </code>
    /// You can locate the button using:
    /// <code>
    /// Selector.XPath("//button[@type='submit']");
    /// </code>
    /// </example>
    public static Selector XPath(string xpath)
    {
        var selector = By.XPath(xpath);
        return new Selector(selector);
    }

    /// <summary>
    /// Returns a string representation of the selector, reflecting the criteria used to locate the UI elements.
    /// </summary>
    /// <returns>A string describing the locator criteria for the selector.</returns>
    public override string ToString() => ByWrapper.ToString();
}