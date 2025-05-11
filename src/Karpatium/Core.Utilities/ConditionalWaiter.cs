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
    public static void ForTrue(Func<bool> anonymousFunction, string message,
        int timeoutInSeconds = DefaultTimeoutInSeconds)
    {
        LastException = null;
        Wait(anonymousFunction, message, timeoutInSeconds);
    }

    private static void Wait(Action anonymousFunction, string message = "", 
        int timeoutInSeconds = DefaultTimeoutInSeconds)
    {
        var stopwatch = Stopwatch.StartNew();
        do
        {
            try
            {
                anonymousFunction();
                return;
            }
            catch (Exception exception)
            {
                LastException = exception;
            }
        }
        while (stopwatch.Elapsed.TotalSeconds < timeoutInSeconds);
        
        Log.Error("{Message} Timeout of '{TimeoutInSeconds}' seconds reached.", message, timeoutInSeconds);
        
        throw LastException;
    }
    
    private static T Wait<T>(Func<T> anonymousFunction, string message, int timeoutInSeconds = DefaultTimeoutInSeconds)
    {
        var stopwatch = Stopwatch.StartNew();
        do
        {
            var response = TryWithExceptionHandling(anonymousFunction);
            if (response.state)
            {
                return response.result;
            }
        }
        while (stopwatch.Elapsed.TotalSeconds < timeoutInSeconds);
        
        Log.Error("{Message} Timeout of '{TimeoutInSeconds}' seconds reached.", message, timeoutInSeconds);
        
        throw LastException ?? new TimeoutException($"{message} Timeout of '{timeoutInSeconds}' seconds reached.");
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