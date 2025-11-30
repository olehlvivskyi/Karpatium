using CrudCrud.API.Models;
using Karpatium.Core.RestApi;
using Newtonsoft.Json;

namespace CrudCrud.API.Apis;

/// <summary>
/// Provides access to the "Users" API.
/// </summary>
public sealed class UsersApi(RestApiClient client)
{
    private const string ApiUrl = "/users";

    /// <summary>
    /// Retrieves a user by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the user to retrieve.</param>
    public async Task<RestApiResponse<UserResponse>> GetUserAsync(string id)
    {
        var request = new RestApiRequest($"{ApiUrl}/{id}");
        var response = await client.GetAsync<UserResponse>(request);
        return response;
    }

    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="user">The details of the user to create.</param>
    /// <returns>A response containing the created user's details.</returns>
    public async Task<RestApiResponse<UserResponse>> PostUserAsync(UserRequest user)
    {
        var request = new RestApiRequest($"{ApiUrl}");
        request.AddJson(JsonConvert.SerializeObject(user));
        var response = await client.PostAsync<UserResponse>(request);
        return response;
    }
}