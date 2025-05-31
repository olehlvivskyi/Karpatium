using System.Collections.Concurrent;
using System.Diagnostics;
using NUnit.Framework;
using Serilog;

namespace Karpatium.Core.Utilities;

/// <summary>
/// Provides functionality to wait for a specific condition to be met until a configurable timeout is reached.
/// </summary>
public static class ConditionalWaiter
{
    private const int DefaultTimeoutInSeconds = 10;
    
    private static ConcurrentDictionary<string, Exception?> LastExceptions { get; } = new();

    private static Exception? LastException
    {
        get => LastExceptions.GetValueOrDefault(TestContext.CurrentContext.WorkerId ?? "single");

        set => LastExceptions[TestContext.CurrentContext.WorkerId ?? "single"] = value!;
    }
    
    /// <summary>
    /// Waits for the specified condition to evaluate to false within a configurable timeout period.
    /// </summary>
    /// <param name="anonymousFunction">A function representing the condition to be evaluated.</param>
    /// <param name="message">The message that will be included if the timeout is reached.</param>
    /// <param name="timeoutInSeconds">The maximum duration, in seconds, to wait for the condition to be false.</param>
    public static void ForFalse(Func<bool> anonymousFunction, string message, int timeoutInSeconds = DefaultTimeoutInSeconds)
    {
        LastException = null;
        Wait(() => !anonymousFunction(), message, timeoutInSeconds);
    }

    /// <summary>
    /// Waits for the specified condition to evaluate to false within a configurable timeout period.
    /// Any exceptions thrown during execution are suppressed.
    /// </summary>
    /// <param name="anonymousFunction">The function that evaluates the condition to be checked.</param>
    /// <param name="timeoutInSeconds">The maximum duration, in seconds, to wait for the condition to be false.</param>
    public static void ForFalseIfPossible(Func<bool> anonymousFunction, int timeoutInSeconds = DefaultTimeoutInSeconds)
    {
        try
        {
            LastException = null;
            Wait(() => !anonymousFunction(), timeoutInSeconds: timeoutInSeconds);
        }
        catch
        {
            // ignored
        }
    }

    /// <summary>
    /// Waits for the specified action to execute without throwing any exceptions within a configurable timeout period.
    /// </summary>
    /// <param name="anonymousFunction">The action to be executed and checked for exceptions.</param>
    /// <param name="message">The message that will be included if the timeout is reached.</param>
    /// <param name="timeoutInSeconds">The maximum duration, in seconds, to wait for the action to complete.</param>
    public static void ForNoException(Action anonymousFunction, string message, 
        int timeoutInSeconds = DefaultTimeoutInSeconds)
    {
        LastException = null;
        Wait(anonymousFunction, message, timeoutInSeconds);
    }

    /// <summary>
    /// Waits for the specified function to execute and return a result within a configurable timeout period.
    /// </summary>
    /// <typeparam name="T">The type of the result expected from the function.</typeparam>
    /// <param name="anonymousFunction">The function to be executed while waiting for a successful result.</param>
    /// <param name="message">The message that will be included if the timeout is reached.</param>
    /// <param name="timeoutInSeconds">The maximum duration, in seconds, to wait for the function to complete.</param>
    /// <returns>The result of the function if it executes successfully within the timeout period.</returns>
    public static T ForResult<T>(Func<T> anonymousFunction, string message,
        int timeoutInSeconds = DefaultTimeoutInSeconds)
    {
        LastException = null;
        return Wait(anonymousFunction, message, timeoutInSeconds);
    }

    /// <summary>
    /// Waits for the specified condition to evaluate to true within a configurable timeout period.
    /// </summary>
    /// <param name="anonymousFunction">A function representing the condition to be evaluated.</param>
    /// <param name="message">The message that will be included if the timeout is reached.</param>
    /// <param name="timeoutInSeconds">The maximum duration, in seconds, to wait for the condition to be true.</param>
    public static void ForTrue(Func<bool> anonymousFunction, string message, int timeoutInSeconds = DefaultTimeoutInSeconds)
    {
        LastException = null;
        Wait(anonymousFunction, message, timeoutInSeconds);
    }

    /// <summary>
    /// Waits for the specified condition to evaluate to true within a configurable timeout period.
    /// Any exceptions thrown during execution are suppressed.
    /// </summary>
    /// <param name="anonymousFunction">The function that evaluates the condition to be checked.</param>
    /// <param name="timeoutInSeconds">The maximum duration, in seconds, to wait for the condition to be true.</param>
    public static void ForTrueIfPossible(Func<bool> anonymousFunction, int timeoutInSeconds = DefaultTimeoutInSeconds)
    {
        try
        {
            LastException = null;
            Wait(anonymousFunction, timeoutInSeconds: timeoutInSeconds);
        }
        catch
        {
            // ignored
        }
    }

    private static void Wait(Action anonymousFunction, string message = "", int timeoutInSeconds = DefaultTimeoutInSeconds)
    {
        var stopwatch = Stopwatch.StartNew();
        while (stopwatch.Elapsed.TotalSeconds < timeoutInSeconds)
        {
            try
            {
                anonymousFunction();
                return;
            }
            catch (Exception ex)
            {
                LastException = ex;
            }
        }
        
        Exception exception = LastException ?? new TimeoutException($"{anonymousFunction.Method.DeclaringType}: Timeout of `{timeoutInSeconds}` seconds reached.");
        Log.Error(exception, "{ClassName}[{MemberName}]: An error occurred ({DeclaringType}).", nameof(ConditionalRunner), nameof(Wait), anonymousFunction.Method.DeclaringType);
        
        throw exception;
    }
    
    private static void Wait(Func<bool> anonymousFunction, string message = "", int timeoutInSeconds = DefaultTimeoutInSeconds)
    {
        var stopwatch = Stopwatch.StartNew();
        while (stopwatch.Elapsed.TotalSeconds < timeoutInSeconds)
        {
            if (TryWithExceptionHandling(anonymousFunction))
            {
                return;
            }
        }
        
        Exception exception = LastException ?? new TimeoutException($"{anonymousFunction.Method.DeclaringType}: Timeout of `{timeoutInSeconds}` seconds reached.");
        Log.Error(exception, "{ClassName}[{MemberName}]: An error occurred ({DeclaringType}).", nameof(ConditionalRunner), nameof(Wait), anonymousFunction.Method.DeclaringType);
        
        throw exception;
    }
    
    private static T Wait<T>(Func<T> anonymousFunction, string message, int timeoutInSeconds = DefaultTimeoutInSeconds)
    {
        var stopwatch = Stopwatch.StartNew();
        while (stopwatch.Elapsed.TotalSeconds < timeoutInSeconds)
        {
            var response = TryWithExceptionHandling(anonymousFunction);
            if (response.state)
            {
                return response.result;
            }
        }
        
        Exception exception = LastException ?? new TimeoutException($"{anonymousFunction.Method.DeclaringType}: Timeout of `{timeoutInSeconds}` seconds reached.");
        Log.Error(exception, "{ClassName}[{MemberName}]: An error occurred ({DeclaringType}).", nameof(ConditionalRunner), nameof(Wait), anonymousFunction.Method.DeclaringType);
        
        throw exception;
    }
    
    private static bool TryWithExceptionHandling(Func<bool> anonymousFunction)
    {
        try
        {
            return anonymousFunction();
        }
        catch (Exception exception)
        {
            LastException = exception;
        }

        return false;
    }
    
    private static (bool state, T result) TryWithExceptionHandling<T>(Func<T> anonymousFunction)
    {
        try
        {
            T value = anonymousFunction();
            return (true, value);
        }
        catch (Exception exception)
        {
            LastException = exception;
        }

        return (false, default)!;
    }
}