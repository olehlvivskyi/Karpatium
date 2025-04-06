using Bogus;

namespace Karpatium.Core.TestData;

/// <summary>
/// A class responsible for generating randomized test data.
/// </summary>
public static class TestDataGenerator
{
    private static readonly Faker Faker = new ();

    /// <summary>
    /// Generates and returns a randomized full address.
    /// </summary>
    /// <returns>A string representing a full address including street, city and country.</returns>
    /// <example>
    /// "432 Johnson Dam, Lake Verna, Australia"
    /// </example>
    public static string GetFullAddress() => Faker.Address.FullAddress();
    
    /// <summary>
    /// Generates and returns a randomized email address.
    /// </summary>
    /// <returns>A string representing an email address.</returns>
    /// <example>
    /// "Johnsmith99@gmail.com"
    /// </example>
    public static string GetEmail() => Faker.Internet.Email();

    /// <summary>
    /// Generates and returns a randomized person full name.
    /// </summary>
    /// <returns>A string representing a person full name.</returns>
    /// <example>
    /// "John Smith"
    /// </example>
    public static string GetFullName() => Faker.Name.FullName();
}