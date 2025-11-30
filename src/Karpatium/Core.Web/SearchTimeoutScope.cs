namespace Karpatium.Core.Web;

/// <summary>
/// A class that provides a temporary override for the browser's implicit wait timeout.
/// </summary>
/// <example>
/// <code>
/// using (new SearchTimeoutScope(TimeSpan.FromSeconds(1)))
/// {
///     ...
/// }
/// </code>
/// </example>
public sealed class SearchTimeoutScope : IDisposable
{
    private readonly TimeSpan _previousTimeout;

    /// <summary>
    /// Represents a disposable scope that temporarily overrides the browser's implicit wait timeout.
    /// Restores the original timeout value upon disposal.
    /// </summary>
    public SearchTimeoutScope(TimeSpan temporaryTimeout)
    {
        _previousTimeout = WebManager.Browser.ImplicitWait;
        WebManager.Browser.ImplicitWait = temporaryTimeout;
    }

    /// <summary>
    /// Disposes of the current search timeout scope by restoring the browser's implicit wait timeout
    /// to its original value prior to the creation of this scope.
    /// </summary>
    public void Dispose()
    {
        WebManager.Browser.ImplicitWait = _previousTimeout;
    }
}