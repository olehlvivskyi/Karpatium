
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Commands;

namespace Karpatium.Core.Nunit;

/// <summary>
/// An attribute used to retry a test execution when the test results are errors or failures.
/// If the test passes or finishes without these states, the retry process stops.
/// </summary>
/// <remarks>
/// This attribute can be applied to individual test methods within NUnit test suites. It ensures
/// that tests are retried a specified number of times when they encounter failure or unexpected
/// errors during execution.
/// </remarks>
/// <param name="retryCount">
/// Defines the number of times the test should be retried if it encounters
/// <see cref="NUnit.Framework.Interfaces.ResultState.Error"/> or
/// <see cref="NUnit.Framework.Interfaces.ResultState.Failure"/>.
/// </param>
[AttributeUsage(AttributeTargets.Method, Inherited = false)]
public sealed class RetryOnErrorAndFailureAttribute(int retryCount) : NUnitAttribute, IRepeatTest
{
    /// <summary>
    /// Wraps a test command to add retry functionality for tests that encounter errors or failures.
    /// </summary>
    /// <param name="command">
    /// The original <see cref="TestCommand"/> that represents the test to be executed.
    /// </param>
    public TestCommand Wrap(TestCommand command) => new RetryExtendedCommand(command, retryCount);

    private class RetryExtendedCommand(TestCommand innerCommand, int retryCount) : DelegatingTestCommand(innerCommand)
    {
        private static readonly HashSet<ResultState> CriticalStates = [ResultState.Error, ResultState.Failure];
        
        private int _retryCount = retryCount;

        public override TestResult Execute(TestExecutionContext context)
        {
            while (_retryCount-- > 0)
            {
                try
                {
                    context.CurrentResult = innerCommand.Execute(context);
                }
                catch (Exception exception)
                {
                    context.CurrentResult.RecordException(exception);
                }

                if (!CriticalStates.Contains(context.CurrentResult.ResultState))
                {
                    break;
                }
                
                if (_retryCount > 0) 
                {
                    context.CurrentResult = context.CurrentTest.MakeTestResult();
                    context.CurrentRepeatCount++;
                }
            }
            
            return context.CurrentResult;
        }
    }
}