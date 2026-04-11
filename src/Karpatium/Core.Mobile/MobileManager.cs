using System.Collections.Concurrent;
using NUnit.Framework;
using Serilog;

namespace Karpatium.Core.Mobile;

/// <summary>
/// Manages mobile driver instances for tests.
/// </summary>
/// <remarks>
/// Ensures thread-safety and proper handling for parallel execution.
/// </remarks>
public static class MobileManager
{
    private static ConcurrentDictionary<string, Mobile> Mobiles { get; } = new();
    private static ConcurrentDictionary<string, MobileWaiter> Waiters { get; } = new();

    /// <summary>
    /// Gets the current mobile platform being used for test execution.
    /// </summary>
    public static MobilePlatform CurrentMobilePlatform { get; private set; }

    /// <summary>
    /// Gets or sets the mobile driver instance for the current test.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the driver is accessed before initialization.
    /// </exception>
    internal static Mobile MobileWrapper
    {
        get => Mobiles[TestContext.CurrentContext.WorkerId ?? "single"]
               ?? throw new InvalidOperationException($"{nameof(MobileManager)} is not initialized.");

        private set => Mobiles[TestContext.CurrentContext.WorkerId ?? "single"] = value;
    }

    /// <summary>
    /// Gets or sets the waiter instance for the current test.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the waiter is accessed before initialization.
    /// </exception>
    private static MobileWaiter WaiterWrapper
    {
        get => Waiters[TestContext.CurrentContext.WorkerId ?? "single"]
               ?? throw new InvalidOperationException($"{nameof(MobileManager)} is not initialized.");

        set => Waiters[TestContext.CurrentContext.WorkerId ?? "single"] = value;
    }

    /// <summary>
    /// Provides access to the mobile driver associated with the current test execution.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the driver is accessed before initialization.
    /// </exception>
    public static IMobile Driver => MobileWrapper;

    /// <summary>
    /// Provides access to the waiter associated with the current test execution.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the waiter is accessed before initialization.
    /// </exception>
    public static IMobileWaiter Waiter => WaiterWrapper;

    /// <summary>
    /// Initializes a new mobile driver instance using the provided settings.
    /// </summary>
    /// <param name="mobileSettings">Settings for configuring the mobile driver instance.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the provided <paramref name="mobileSettings"/> is null.
    /// </exception>
    public static void Initialize(IMobileSettings mobileSettings)
    {
        ArgumentNullException.ThrowIfNull(mobileSettings);

        Log.Verbose("MobileManager: Initializing `{Platform}` driver.", mobileSettings.MobilePlatform);
        Log.Verbose("MobileManager: IMobileSettings = `{@MobileSettings}`", mobileSettings);

        IMobileFactory factory = MobileFactory.Get(mobileSettings);
        MobileWrapper = factory.CreateMobile(mobileSettings);
        WaiterWrapper = new MobileWaiter(MobileWrapper);
        CurrentMobilePlatform = mobileSettings.MobilePlatform;
    }

    /// <summary>
    /// Closes the mobile driver associated with the current test.
    /// </summary>
    public static void Quit()
    {
        Log.Verbose("MobileManager: Quitting mobile driver.");
        MobileWrapper.Quit();
    }
}