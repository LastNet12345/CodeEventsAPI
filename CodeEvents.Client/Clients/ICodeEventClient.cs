using CodeEvents.Api.Core.DTOs;

namespace CodeEvents.Client.Clients
{
    public interface ICodeEventClient
    {
        //ToDo remove hardcoded contentType.
        Task<T?> GetAsync<T>(string path, CancellationToken cancellationToken, string contentType = "application/json");


    }
}