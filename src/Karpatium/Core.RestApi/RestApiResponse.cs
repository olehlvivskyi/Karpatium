using System.Net;

namespace Karpatium.Core.RestApi;

/// <summary>
/// Represents the response from a REST API call.
/// </summary>
public class RestApiResponse
{
    /// <summary>
    /// Gets the content body of the REST API response as a string. This property contains the data returned by the API call in raw text format.
    /// </summary>
    public string? Content { get; init; }
    
    /// <summary>
    /// Gets the HTTP status code associated with the REST API response. This property indicates the outcome of the HTTP request.
    /// </summary>
    /// <remarks>
    /// The status code adheres to the <see cref="HttpStatusCode"/> enumeration, providing standard HTTP response codes as defined in RFC 2616.
    /// </remarks>
    public HttpStatusCode StatusCode { get; init; }
}

/// <summary>
/// Represents the response from a REST API call.
/// </summary>
/// <typeparam name="T">The type of data contained in the response.</typeparam>
public class RestApiResponse<T> : RestApiResponse
{
    /// <summary>
    /// Gets the data included in the REST API response. This property contains the deserialized payload of the response or a default value if no data is present.
    /// </summary>
    /// <remarks>
    /// The type of data is determined by the generic parameter <typeparamref name="T"/> specified when making the API request.
    /// </remarks>
    public T? Data { get; init; }
}