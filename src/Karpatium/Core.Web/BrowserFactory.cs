using Karpatium.Core.Web.Exceptions;

namespace Karpatium.Core.Web;

/// <summary>
/// Provides a factory method for creating browser factory instances based on specified browser settings.
/// </summary>
internal static class BrowserFactory
{
    private static readonly Dictionary<BrowserType, IBrowserFactory> Factories = new()
    {
        { BrowserType.Chrome, new ChromeBrowserFactory() }
    };
    
    /// <summary>
    /// Creates an instance of an <see cref="IBrowserFactory"/> based on the provided browser settings.
    /// </summary>
    /// <param name="browserSettings">The browser settings used to determine the appropriate factory.</param>
    /// <returns>
    /// An instance of an <see cref="IBrowserFactory"/> corresponding to the specified browser settings.
    /// </returns>
    /// <exception cref="BrowserFactoryNotImplementedException">
    /// Thrown when the factory for the given browser type is not implemented.
    /// </exception>
    internal static IBrowserFactory Get(IBrowserSettings browserSettings)
    {
        if (!Factories.TryGetValue(browserSettings.BrowserType, out var factory))
        {
            throw new BrowserFactoryNotImplementedException(browserSettings.BrowserType);
        }

        return factory;
    }
}