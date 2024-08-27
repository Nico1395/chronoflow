using ChronoFlow.Shared.Common.Http;
using ChronoFlow.Shared.Common.Mapping;
using ChronoFlow.Shared.Common.Messaging;
using MapsterMapper;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System.Text;

namespace ChronoFlow.Client.Common.Http;

internal sealed class ServerHttpRequestService(IHttpClientProvider _httpClientProvider, IMapper _mapper) : IServerHttpRequestService
{
    public async Task<Result<TViewModel>> GetAsync<TDto, TViewModel>(string uri, CancellationToken cancellationToken = default)
        where TViewModel : class
        where TDto : class
    {
        var responseMessage = await _httpClientProvider.GetServerClient().GetAsync(uri, cancellationToken);
        var response = await responseMessage.Content.DeserializeAsync<Result<TDto>>(cancellationToken);

        return _mapper.MapResult<TDto, TViewModel>(response);
    }

    public async Task<Result<TViewModel>> GetAsync<TDto, TViewModel>(string uri, CancellationToken cancellationToken = default, params (string Name, object Value)[] queryParameters)
        where TViewModel : class
        where TDto : class
    {
        var compoundUri = CreateCompoundUri(uri, queryParameters);

        var responseMessage = await _httpClientProvider.GetServerClient().GetAsync(compoundUri, cancellationToken);
        var response = await responseMessage.Content.DeserializeAsync<Result<TDto>>(cancellationToken);

        return _mapper.MapResult<TDto, TViewModel>(response);
    }

    public async Task<Result> PostAsync<TDto, TViewModel>(string uri, TViewModel data, CancellationToken cancellationToken = default)
        where TViewModel : class
        where TDto : class
    {
        var requestMessage = CreateRequestMessage<TDto, TViewModel>(data);

        var responseMessage = await _httpClientProvider.GetServerClient().PostAsync(uri, requestMessage.Content, cancellationToken);
        var response = await responseMessage.Content.DeserializeAsync<Result>(cancellationToken);

        return response;
    }

    public async Task<Result> PostAsync<TDto, TViewModel>(string uri, TViewModel data, CancellationToken cancellationToken = default, params (string Name, object Value)[] queryParameters)
        where TViewModel : class
        where TDto : class
    {
        var requestMessage = CreateRequestMessage<TDto, TViewModel>(data);
        var compoundUri = CreateCompoundUri(uri, queryParameters);

        var responseMessage = await _httpClientProvider.GetServerClient().PostAsync(compoundUri, requestMessage.Content, cancellationToken);
        var response = await responseMessage.Content.DeserializeAsync<Result>(cancellationToken);

        return response;
    }

    public async Task<Result> PatchAsync<TDto, TViewModel>(string uri, TViewModel data, CancellationToken cancellationToken = default)
        where TViewModel : class
        where TDto : class
    {
        var requestMessage = CreateRequestMessage<TDto, TViewModel>(data);

        var responseMessage = await _httpClientProvider.GetServerClient().PatchAsync(uri, requestMessage.Content, cancellationToken);
        var response = await responseMessage.Content.DeserializeAsync<Result>(cancellationToken);

        return response;
    }

    public async Task<Result> PatchAsync<TDto, TViewModel>(string uri, TViewModel data, CancellationToken cancellationToken = default, params (string Name, object Value)[] queryParameters)
        where TViewModel : class
        where TDto : class
    {
        var requestMessage = CreateRequestMessage<TDto, TViewModel>(data);
        var compoundUri = CreateCompoundUri(uri, queryParameters);

        var responseMessage = await _httpClientProvider.GetServerClient().PatchAsync(compoundUri, requestMessage.Content, cancellationToken);
        var response = await responseMessage.Content.DeserializeAsync<Result>(cancellationToken);

        return response;
    }

    public async Task<Result> PostAsync(string uri, CancellationToken cancellationToken = default)
    {
        var requestMessage = CreateRequestMessage();

        var responseMessage = await _httpClientProvider.GetServerClient().PostAsync(uri, requestMessage.Content, cancellationToken);
        var response = await responseMessage.Content.DeserializeAsync<Result>(cancellationToken);

        return response;
    }

    public async Task<Result> PostAsync(string uri, CancellationToken cancellationToken = default, params (string Name, object Value)[] queryParameters)
    {
        var requestMessage = CreateRequestMessage();
        var compoundUri = CreateCompoundUri(uri, queryParameters);

        var responseMessage = await _httpClientProvider.GetServerClient().PostAsync(compoundUri, requestMessage.Content, cancellationToken);
        var response = await responseMessage.Content.DeserializeAsync<Result>(cancellationToken);

        return response;
    }

    public async Task<Result> DeleteAsync(string uri, CancellationToken cancellationToken = default)
    {
        var responseMessage = await _httpClientProvider.GetServerClient().DeleteAsync(uri, cancellationToken);
        var response = await responseMessage.Content.DeserializeAsync<Result>(cancellationToken);

        return response;
    }

    public async Task<Result> DeleteAsync(string uri, CancellationToken cancellationToken = default, params (string Name, object Value)[] queryParameters)
    {
        var compoundUri = CreateCompoundUri(uri, queryParameters);

        var responseMessage = await _httpClientProvider.GetServerClient().DeleteAsync(compoundUri, cancellationToken);
        var response = await responseMessage.Content.DeserializeAsync<Result>(cancellationToken);

        return response;
    }

    private HttpRequestMessage CreateRequestMessage(string? serializedData = null)
    {
        HttpContent? content = null;
        if (serializedData != null)
            content = new StringContent(serializedData, Encoding.UTF8, "application/json");

        return new HttpRequestMessage() { Content = content, };
    }

    private HttpRequestMessage CreateRequestMessage<TDto, TViewModel>(TViewModel data)
        where TViewModel : class
        where TDto : class
    {
        var mappedData = _mapper.Map<TDto>(data);
        var serializedData = JsonConvert.SerializeObject(mappedData, HttpSerializationConstants.JsonSerializerSettings);
        return CreateRequestMessage(serializedData);
    }

    private string CreateCompoundUri(string uri, (string Name, object Value)[] queryParameters)
    {
        foreach (var (Name, Value) in queryParameters)
            uri = QueryHelpers.AddQueryString(uri, Name, Value.ToString()!);

        return uri;
    }
}
