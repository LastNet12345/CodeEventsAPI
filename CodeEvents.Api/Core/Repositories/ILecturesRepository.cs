using CodeEvents.Api.Core.Entities;

namespace CodeEvents.Api.Core.Repositories
{
    public interface ILecturesRepository
    {
        Task AddAsync(Lecture lecture);
        Task<IEnumerable<Lecture>> GetAsync(string name);
        Task<Lecture?> GetAsync(string name, int id);
    }
}