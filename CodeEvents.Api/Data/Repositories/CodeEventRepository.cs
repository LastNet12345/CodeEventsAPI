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

        public async Task<IEnumerable<CodeEvent>> GetAsync()
        {
            return await db.CodeEvent.ToListAsync();
        }
    }
}
