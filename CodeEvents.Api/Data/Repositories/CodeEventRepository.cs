using CodeEvents.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodeEvents.Api.Data.Repositories
{
    public class CodeEventRepository
    {
        private readonly CodeEventsApiContext db;

        public CodeEventRepository(CodeEventsApiContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<CodeEvent>> GetAsync(bool includeLectures = false)
        {
            return includeLectures ? await db.CodeEvent.Include(c => c.Location)
                                                       .Include(c => c.Lectures)
                                                       .ToListAsync() :
                                     await db.CodeEvent.Include(c => c.Location)
                                                       .ToListAsync();
        }

        public async Task<CodeEvent?> GetAsync(string name, bool includeLectures = false)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace", nameof(name));
            }

            var query = db.CodeEvent
                    .Include(c => c.Location)
                    .AsQueryable();

            if(includeLectures)
            {
                query = query.Include(c => c.Lectures);
            }


            return await query.FirstOrDefaultAsync(c => c.Name == name);

        }

        public async Task AddAsync(CodeEvent codeEvent)
        {
            if (codeEvent is null)
            {
                throw new ArgumentNullException(nameof(codeEvent));
            }

            await db.AddAsync(codeEvent);
        }
    }
}
