using Serilog;

namespace Karpatium.Core.Utilities;

/// <summary>
/// Provides functionality to run with a specific condition.
/// </summary>
public static class ConditionalRunner
{
    /// <summary>
    /// Executes the specified action while suppressing any exceptions that may occur during execution.
    /// </summary>
    /// <param name="anonymousFunction">The action to execute. Any exceptions will be caught and ignored.</param>
    public static void IgnoreException(Action anonymousFunction)
    {
        try
        {
            anonymousFunction();
        }
        catch (Exception exception)
        {
            // ignored
        }
    }
}