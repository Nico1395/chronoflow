using ChronoFlow.Shared.Common.Messaging;

namespace ChronoFlow.Client.Common.Http;

public interface IServerHttpRequestService
{
    public Task<Result<TViewModel>> GetAsync<TDto, TViewModel>(string uri, CancellationToken cancellationToken = default)
        where TViewModel : class
        where TDto : class;

    public Task<Result<TViewModel>> GetAsync<TDto, TViewModel>(string uri, CancellationToken cancellationToken = default, params (string Name, object Value)[] queryParameters)
        where TViewModel : class
        where TDto : class;

    public Task<Result> PostAsync<TDto, TViewModel>(string uri, TViewModel data, CancellationToken cancellationToken = default)
        where TViewModel : class
        where TDto : class;

    public Task<Result> PostAsync<TDto, TViewModel>(string uri, TViewModel data, CancellationToken cancellationToken = default, params (string Name, object Value)[] queryParameters)
        where TViewModel : class
        where TDto : class;

    public Task<Result> PatchAsync<TDto, TViewModel>(string uri, TViewModel data, CancellationToken cancellationToken = default)
        where TViewModel : class
        where TDto : class;

    public Task<Result> PatchAsync<TDto, TViewModel>(string uri, TViewModel data, CancellationToken cancellationToken = default, params (string Name, object Value)[] queryParameters)
        where TViewModel : class
        where TDto : class;

    public Task<Result> PostAsync(string uri, CancellationToken cancellationToken = default);
    public Task<Result> PostAsync(string uri, CancellationToken cancellationToken = default, params (string Name, object Value)[] queryParameters);
    public Task<Result> DeleteAsync(string uri, CancellationToken cancellationToken = default);
    public Task<Result> DeleteAsync(string uri, CancellationToken cancellationToken = default, params (string Name, object Value)[] queryParameters);
}
