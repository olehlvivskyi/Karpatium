using CrudCrud.API.Apis;
using Karpatium.Core.RestApi;

namespace CrudCrud.API;

/// <summary>
/// Provides access to the different APIs in the Crud Crud application.
/// </summary>
/// <remarks>
/// This class serves as a central entry point for accessing specific APIs.
/// </remarks>
public sealed class CrudCrudApis(string baseUrl)
{
    private readonly RestApiClient _client = new(baseUrl);
    
    /// <summary>
    /// Provides access to the "Users" API.
    /// </summary>
    public UsersApi UsersApi => new(_client);
}