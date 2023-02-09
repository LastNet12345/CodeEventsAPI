using CodeEvents.Api.Core.DTOs;

namespace CodeEvents.Client.Clients
{
    public interface ICodeEventClient
    {
        Task<CodeEventDto?> GetCodeEventAsync(string name);
        Task<IEnumerable<CodeEventDto>?> GetCodeEventsAsync();
        Task<LectureDto?> GetLectureAsync(string name, int id);
    }
}