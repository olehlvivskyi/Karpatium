namespace Karpatium.Core.Mobile;

/// <summary>
/// Represents configuration settings for mobile driver instances.
/// </summary>
public interface IMobileSettings
{
    /// <summary>
    /// Gets the target mobile platform (Android or iOS).
    /// </summary>
    /// <remarks>
    /// The <see cref="MobilePlatform"/> specifies the platform (e.g., Android) that will be instantiated by the framework.
    /// </remarks>
    MobilePlatform MobilePlatform { get; }

    /// <summary>
    /// Gets the Appium server URL (e.g., http://localhost:4723).
    /// </summary>
    string AppiumServerUrl { get; }

    /// <summary>
    /// Gets the name of the device to run tests on.
    /// </summary>
    string DeviceName { get; }

    /// <summary>
    /// Gets the platform version (e.g., "14.0" for iOS or "11" for Android).
    /// </summary>
    string PlatformVersion { get; }

    /// <summary>
    /// Gets the path to the application under test, or bundle/package ID.
    /// </summary>
    string App { get; }

    /// <summary>
    /// Gets the implicit wait timeout in seconds.
    /// </summary>
    int ImplicitWaitInSeconds { get; }
}
