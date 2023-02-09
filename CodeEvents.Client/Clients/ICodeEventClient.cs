using CodeEvents.Api.Core.DTOs;

namespace CodeEvents.Client.Clients
{
    public interface ICodeEventClient
    {
        Task<T?> GetAsync<T>(string path, string contentType = "application/json");


    }
}