namespace Karpatium.Core.Utilities;

/// <summary>
/// Provides utility methods for handling file or directory paths.
/// </summary>
public static class PathUtils
{
    /// <summary>
    /// Combines the user's home directory path with the folder name and optionally file name to generate a local path.
    /// </summary>
    /// <param name="folderName">The name of the folder to be appended to the user's home directory path.</param>
    /// <param name="fileName">The name of the file to be included in the path.</param>
    /// <returns>
    /// A string representing the full local path constructed from the user's profile directory,
    /// the specified folder name, and optionally the file name.
    /// </returns>
    public static string GetLocalUserPath(string folderName, string? fileName = null)
    {
        return string.IsNullOrEmpty(fileName)
            ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), folderName)
            : Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), folderName, fileName);
    }
}