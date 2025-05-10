namespace Karpatium.Core.Web;

/// <summary>
/// Defines a factory for creating browser instances with specific settings.
/// </summary>
internal interface IBrowserFactory
{
    /// <summary>
    /// Creates a new instance of <see cref="Browser"/> based on the specified browser settings.
    /// </summary>
    /// <param name="browserSettings">The settings for configuring the browser instance.</param>
    /// <returns>A configured <see cref="Browser"/> instance.</returns>
    internal Browser CreateBrowser(IBrowserSettings browserSettings);
}