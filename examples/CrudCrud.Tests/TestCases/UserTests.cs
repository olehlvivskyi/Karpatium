using Allure.Net.Commons;
using Allure.NUnit.Attributes;
using Bogus;
using CrudCrud.API.Models;
using Karpatium.Core.Nunit;
using Karpatium.Core.RestApi;
using NUnit.Framework;

namespace CrudCrud.Tests.TestCases;

[AllureParentSuite("CrudCrud QA")]
[AllureSuite("API")]
[AllureSubSuite("Users")]
[TestFixture]
public sealed class UserTests : BaseFixture
{
    private string _createdUserId = null!;

    private readonly UserRequest _userToCreate = GetRandomUser();

    [AllureBefore]
    [OneTimeSetUp]
    public async Task UserOneTimeSetUp()
    {
        RestApiResponse<UserResponse> createdUser = await CrudCrudApis.UsersApi.PostUserAsync(_userToCreate);
        _createdUserId = createdUser.Data!.Id;
    }
    
    [TestCase(TestName = "Ensure that user is retrieved by ID.")]
    [AllureEpic("CCC-987 Users: Implement Users API")]
    [AllureLink("Epic", "CCC-987")]
    [AllureStory("TQA-222 Users: Implement GET for Users API")]
    [AllureLink("User Story", "TQA-222")]
    [AllureTms("Test Case", "TC-63261")]
    [AllureSeverity(SeverityLevel.critical)]
    [AllureOwner("oleh.lvivskyi")]
    [RetryOnErrorAndFailure(3)]
    public async Task EnsureThatUserIsRetrievedById()
    {
        RestApiResponse<UserResponse> retrievedUser = await CrudCrudApis.UsersApi.GetUserAsync(_createdUserId);
        
        Assert.Multiple(() =>
        {
            Assert.That(retrievedUser.Data!.Id, Is.EqualTo(_createdUserId), "The retrieved user ID is incorrect.");
            Assert.That(retrievedUser.Data.Username, Is.EqualTo(_userToCreate.Username), "The retrieved username is incorrect.");
            Assert.That(retrievedUser.Data.Email, Is.EqualTo(_userToCreate.Email), "The retrieved email is incorrect.");
            Assert.That(retrievedUser.Data.FirstName, Is.EqualTo(_userToCreate.FirstName), "The retrieved first name is incorrect.");
            Assert.That(retrievedUser.Data.LastName, Is.EqualTo(_userToCreate.LastName), "The retrieved last name is incorrect.");
            Assert.That(retrievedUser.Data.Age, Is.EqualTo(_userToCreate.Age), "The retrieved age is incorrect.");
            Assert.That(retrievedUser.Data.IsActive, Is.EqualTo(_userToCreate.IsActive), "The retrieved active status is incorrect.");
        });
    }
    
    private static UserRequest GetRandomUser()
    {
        UserRequest user = new Faker<UserRequest>()
            .StrictMode(true)
            .RuleFor(u => u.Username, (f, w) => f.Internet.UserName())
            .RuleFor(u => u.Email, (f, w) => f.Internet.Email(w.FirstName, w.LastName))
            .RuleFor(u => u.FirstName, f => f.Name.FirstName())
            .RuleFor(u => u.LastName, f => f.Name.LastName())
            .RuleFor(u => u.Age, f => f.Random.Number(18, 65))
            .RuleFor(u => u.IsActive, f => f.Random.Bool())
            .Generate();

        return user;
    }
}