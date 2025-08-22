using RestSharp;

namespace Karpatium.Core.RestApi;

/// <summary>
/// Represents an abstraction for constructing and managing HTTP requests in a REST API context.
/// </summary>
public class RestApiRequest(string resourceUrl)
{
    internal RestRequest RequestWrapper { get; } = new(resourceUrl);

    /// <summary>
    /// Adds a header to the REST API request.
    /// </summary>
    /// <param name="name">The name of the header to add.</param>
    /// <param name="value">The value of the header to add.</param>
    public void AddHeader(string name, string value) => RequestWrapper.AddHeader(name, value);

    /// <summary>
    /// Adds a JSON payload to the REST API request.
    /// </summary>
    /// <param name="json">The JSON string to be added as the request body.</param>
    public void AddJson(string json) => RequestWrapper.AddJsonBody(json);
    
    /// <summary>
    /// Adds a parameter to the REST API request.
    /// </summary>
    /// <param name="name">The name of the parameter to add.</param>
    /// <param name="value">The value of the parameter to add.</param>
    public void AddParameter(string name, string value) => RequestWrapper.AddParameter(name, value);
}