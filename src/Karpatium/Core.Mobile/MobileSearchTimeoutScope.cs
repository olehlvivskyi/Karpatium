namespace Karpatium.Core.Mobile;

/// <summary>
/// A class that provides a temporary override for the mobile driver's implicit wait timeout.
/// </summary>
/// <example>
/// <code>
/// using (new MobileSearchTimeoutScope(TimeSpan.FromSeconds(1)))
/// {
///     ...
/// }
/// </code>
/// </example>
public sealed class MobileSearchTimeoutScope : IDisposable
{
    private readonly TimeSpan _previousTimeout;

    /// <summary>
    /// Represents a disposable scope that temporarily overrides the mobile driver's implicit wait timeout.
    /// Restores the original timeout value upon disposal.
    /// </summary>
    public MobileSearchTimeoutScope(TimeSpan temporaryTimeout)
    {
        _previousTimeout = MobileManager.Driver.ImplicitWait;
        MobileManager.Driver.ImplicitWait = temporaryTimeout;
    }

    /// <summary>
    /// Disposes of the current search timeout scope by restoring the mobile driver's implicit wait timeout
    /// to its original value prior to the creation of this scope.
    /// </summary>
    public void Dispose()
    {
        MobileManager.Driver.ImplicitWait = _previousTimeout;
    }
}
