namespace Karpatium.Core.Web.Exceptions;

/// <summary>
/// Represents an exception that is thrown when a browser factory implementation
/// for the specified browser type is not available.
/// </summary>
public class BrowserFactoryNotImplementedException(BrowserType browserType) 
    : Exception($"Browser factory for `{browserType}` is not implemented.");