using CodeEvents.Api.Core.DTOs;

namespace CodeEvents.Client.Clients
{
    public interface ICodeEventClient
    {
        //ToDo remove hardcoded contentType.
        Task<T?> GetAsync<T>(string path, string contentType = "application/json");


    }
}