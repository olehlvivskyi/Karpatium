using System.Collections.Concurrent;

namespace ToolsQa.Tests.TestUsers;

/// <summary>
/// Represents a pool of reusable user instances for testing purposes.
/// </summary>
/// <remarks>
/// Provided as an example for data-based parallelization implementation.
/// </remarks>
public static class UsersPool
{
    private static readonly ConcurrentBag<User> UsersBag;

    static UsersPool()
    {
        IReadOnlyList<User> users = TestConfiguration.ApplicationSettings.Users;
        UsersBag = new ConcurrentBag<User>(users);
    }

    public static User? Get() => UsersBag.TryTake(out var item) ? item : null;

    public static void Return(User item) => UsersBag.Add(item);
}