namespace Karpatium.Core.Web.Exceptions;

/// <summary>
/// Represents an exception that is thrown when a browser factory implementation
/// for the specified browser type is not available.
/// </summary>
public sealed class BrowserFactoryNotImplementedException(BrowserType browserType) 
    : Exception($"`{browserType}` browser factory is not implemented.");