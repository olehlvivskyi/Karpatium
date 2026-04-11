namespace Karpatium.Core.Mobile.Exceptions;

/// <summary>
/// Represents an exception that is thrown when a mobile factory implementation
/// for the specified platform is not available.
/// </summary>
public sealed class MobileFactoryNotImplementedException(MobilePlatform mobilePlatform)
    : Exception($"`{mobilePlatform}` mobile factory is not implemented.");
