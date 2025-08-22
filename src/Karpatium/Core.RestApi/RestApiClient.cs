using RestSharp;
using Serilog;

namespace Karpatium.Core.RestApi;

/// <summary>
/// A client for interacting with REST APIs, providing support for common HTTP methods asynchronously.
/// </summary>
public class RestApiClient(string baseUrl)
{
    private readonly RestClient _clientWrapper = new(baseUrl);

    /// <summary>
    /// Executes an asynchronous HTTP DELETE request using the specified request.
    /// </summary>
    /// <param name="request">The <see cref="RestApiRequest"/> containing the details of the DELETE request.</param>
    public async Task<RestApiResponse> DeleteAsync(RestApiRequest request)
    {
        Log.Verbose("RestApiClient: Executing DELETE `{Path}` API.", Path.Combine(baseUrl, request.RequestWrapper.Resource));
        
        RestResponse response = await _clientWrapper.ExecuteDeleteAsync(request.RequestWrapper);
        RestApiResponse responseFinal = new RestApiResponse
        {
            Content = response.Content,
            StatusCode = response.StatusCode
        };
        
        return responseFinal;
    }
    
    /// <summary>
    /// Executes an asynchronous HTTP DELETE request using the specified request.
    /// </summary>
    /// <param name="request">The <see cref="RestApiRequest"/> containing the details of the DELETE request.</param>
    public async Task<RestApiResponse<T>> DeleteAsync<T>(RestApiRequest request)
    {
        Log.Verbose("RestApiClient: Executing DELETE `{Path}` API.", Path.Combine(baseUrl, request.RequestWrapper.Resource));
        
        RestResponse<T> response = await _clientWrapper.ExecuteDeleteAsync<T>(request.RequestWrapper);
        RestApiResponse<T> responseFinal = new RestApiResponse<T>
        {
            Content = response.Content,
            Data = response.Data,
            StatusCode = response.StatusCode
        };
        
        return responseFinal;
    }
    
    /// <summary>
    /// Executes an asynchronous HTTP GET request using the specified request.
    /// </summary>
    /// <param name="request">The <see cref="RestApiRequest"/> containing the details of the GET request.</param>
    public async Task<RestApiResponse> GetAsync(RestApiRequest request)
    {
        Log.Verbose("RestApiClient: Executing GET `{Path}` API.", Path.Combine(baseUrl, request.RequestWrapper.Resource));
        
        RestResponse response = await _clientWrapper.ExecuteGetAsync(request.RequestWrapper);
        RestApiResponse responseFinal = new RestApiResponse
        {
            Content = response.Content,
            StatusCode = response.StatusCode
        };
        
        return responseFinal;
    }

    /// <summary>
    /// Executes an asynchronous HTTP GET request using the specified request.
    /// </summary>
    /// <param name="request">The <see cref="RestApiRequest"/> containing the details of the GET request.</param>
    public async Task<RestApiResponse<T>> GetAsync<T>(RestApiRequest request)
    {
        Log.Verbose("RestApiClient: Executing GET `{Path}` API.", Path.Combine(baseUrl, request.RequestWrapper.Resource));
        
        RestResponse<T> response = await _clientWrapper.ExecuteGetAsync<T>(request.RequestWrapper);
        RestApiResponse<T> responseFinal = new RestApiResponse<T>
        {
            Content = response.Content,
            Data = response.Data,
            StatusCode = response.StatusCode
        };
        
        return responseFinal;
    }
    
    /// <summary>
    /// Executes an asynchronous HTTP HEAD request using the specified request.
    /// </summary>
    /// <param name="request">The <see cref="RestApiRequest"/> containing the details of the HEAD request.</param>
    public async Task<RestApiResponse> HeadAsync(RestApiRequest request)
    {
        Log.Verbose("RestApiClient: Executing HEAD `{Path}` API.", Path.Combine(baseUrl, request.RequestWrapper.Resource));
        
        RestResponse response = await _clientWrapper.ExecuteHeadAsync(request.RequestWrapper);
        RestApiResponse responseFinal = new RestApiResponse
        {
            Content = response.Content,
            StatusCode = response.StatusCode
        };
        
        return responseFinal;
    }

    /// <summary>
    /// Executes an asynchronous HTTP HEAD request using the specified request.
    /// </summary>
    /// <param name="request">The <see cref="RestApiRequest"/> containing the details of the HEAD request.</param>
    public async Task<RestApiResponse<T>> HeadAsync<T>(RestApiRequest request)
    {
        Log.Verbose("RestApiClient: Executing HEAD `{Path}` API.", Path.Combine(baseUrl, request.RequestWrapper.Resource));
        
        RestResponse<T> response = await _clientWrapper.ExecuteHeadAsync<T>(request.RequestWrapper);
        RestApiResponse<T> responseFinal = new RestApiResponse<T>
        {
            Content = response.Content,
            Data = response.Data,
            StatusCode = response.StatusCode
        };
        
        return responseFinal;
    }
    
    /// <summary>
    /// Executes an asynchronous HTTP OPTIONS request using the specified request.
    /// </summary>
    /// <param name="request">The <see cref="RestApiRequest"/> containing the details of the OPTIONS request.</param>
    public async Task<RestApiResponse> OptionsAsync(RestApiRequest request)
    {
        Log.Verbose("RestApiClient: Executing OPTIONS `{Path}` API.", Path.Combine(baseUrl, request.RequestWrapper.Resource));
        
        RestResponse response = await _clientWrapper.ExecuteOptionsAsync(request.RequestWrapper);
        RestApiResponse responseFinal = new RestApiResponse
        {
            Content = response.Content,
            StatusCode = response.StatusCode
        };
        
        return responseFinal;
    }

    /// <summary>
    /// Executes an asynchronous HTTP OPTIONS request using the specified request.
    /// </summary>
    /// <param name="request">The <see cref="RestApiRequest"/> containing the details of the OPTIONS request.</param>
    public async Task<RestApiResponse<T>> OptionsAsync<T>(RestApiRequest request)
    {
        Log.Verbose("RestApiClient: Executing OPTIONS `{Path}` API.", Path.Combine(baseUrl, request.RequestWrapper.Resource));
        
        RestResponse<T> response = await _clientWrapper.ExecuteOptionsAsync<T>(request.RequestWrapper);
        RestApiResponse<T> responseFinal = new RestApiResponse<T>
        {
            Content = response.Content,
            Data = response.Data,
            StatusCode = response.StatusCode
        };
        
        return responseFinal;
    }
    
    /// <summary>
    /// Executes an asynchronous HTTP PATCH request using the specified request.
    /// </summary>
    /// <param name="request">The <see cref="RestApiRequest"/> containing the details of the PATCH request.</param>
    public async Task<RestApiResponse> PatchAsync(RestApiRequest request)
    {
        Log.Verbose("RestApiClient: Executing PATCH `{Path}` API.", Path.Combine(baseUrl, request.RequestWrapper.Resource));
        
        RestResponse response = await _clientWrapper.ExecutePatchAsync(request.RequestWrapper);
        RestApiResponse responseFinal = new RestApiResponse
        {
            Content = response.Content,
            StatusCode = response.StatusCode
        };
        
        return responseFinal;
    }

    /// <summary>
    /// Executes an asynchronous HTTP PATCH request using the specified request.
    /// </summary>
    /// <param name="request">The <see cref="RestApiRequest"/> containing the details of the PATCH request.</param>
    public async Task<RestApiResponse<T>> PatchAsync<T>(RestApiRequest request)
    {
        Log.Verbose("RestApiClient: Executing PATCH `{Path}` API.", Path.Combine(baseUrl, request.RequestWrapper.Resource));
        
        RestResponse<T> response = await _clientWrapper.ExecutePatchAsync<T>(request.RequestWrapper);
        RestApiResponse<T> responseFinal = new RestApiResponse<T>
        {
            Content = response.Content,
            Data = response.Data,
            StatusCode = response.StatusCode
        };
        
        return responseFinal;
    }
    
    /// <summary>
    /// Executes an asynchronous HTTP POST request using the specified request.
    /// </summary>
    /// <param name="request">The <see cref="RestApiRequest"/> containing the details of the POST request.</param>
    public async Task<RestApiResponse> PostAsync(RestApiRequest request)
    {
        Log.Verbose("RestApiClient: Executing POST `{Path}` API.", Path.Combine(baseUrl, request.RequestWrapper.Resource));
        
        RestResponse response = await _clientWrapper.ExecuteGetAsync(request.RequestWrapper);
        RestApiResponse responseFinal = new RestApiResponse
        {
            Content = response.Content,
            StatusCode = response.StatusCode
        };
        
        return responseFinal;
    }

    /// <summary>
    /// Executes an asynchronous HTTP POST request using the specified request.
    /// </summary>
    /// <param name="request">The <see cref="RestApiRequest"/> containing the details of the POST request.</param>
    public async Task<RestApiResponse<T>> PostAsync<T>(RestApiRequest request)
    {
        Log.Verbose("RestApiClient: Executing POST `{Path}` API.", Path.Combine(baseUrl, request.RequestWrapper.Resource));
        
        RestResponse<T> response = await _clientWrapper.ExecuteGetAsync<T>(request.RequestWrapper);
        RestApiResponse<T> responseFinal = new RestApiResponse<T>
        {
            Content = response.Content,
            Data = response.Data,
            StatusCode = response.StatusCode
        };
        
        return responseFinal;
    }
    
    /// <summary>
    /// Executes an asynchronous HTTP PUT request using the specified request.
    /// </summary>
    /// <param name="request">The <see cref="RestApiRequest"/> containing the details of the PUT request.</param>
    public async Task<RestApiResponse> PutAsync(RestApiRequest request)
    {
        Log.Verbose("RestApiClient: Executing PUT `{Path}` API.", Path.Combine(baseUrl, request.RequestWrapper.Resource));
        
        RestResponse response = await _clientWrapper.ExecuteGetAsync(request.RequestWrapper);
        RestApiResponse responseFinal = new RestApiResponse
        {
            Content = response.Content,
            StatusCode = response.StatusCode
        };
        
        return responseFinal;
    }

    /// <summary>
    /// Executes an asynchronous HTTP PUT request using the specified request.
    /// </summary>
    /// <param name="request">The <see cref="RestApiRequest"/> containing the details of the PUT request.</param>
    public async Task<RestApiResponse<T>> PutAsync<T>(RestApiRequest request)
    {
        Log.Verbose("RestApiClient: Executing PUT `{Path}` API.", Path.Combine(baseUrl, request.RequestWrapper.Resource));
        
        RestResponse<T> response = await _clientWrapper.ExecuteGetAsync<T>(request.RequestWrapper);
        RestApiResponse<T> responseFinal = new RestApiResponse<T>
        {
            Content = response.Content,
            Data = response.Data,
            StatusCode = response.StatusCode
        };
        
        return responseFinal;
    }
}