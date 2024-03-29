﻿using CodeEvents.Api.Core.Entities;
using CodeEvents.Api.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CodeEvents.Api.Data.Repositories
{
    public class LecturesRepository : ILecturesRepository
    {
        private readonly CodeEventsApiContext db;

        public LecturesRepository(CodeEventsApiContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<Lecture>> GetAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            return await db.Lecture.Where(l => l.CodeEvent.Name == name).ToListAsync();
        }

        public async Task<Lecture?> GetAsync(string name, int id)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            return await db.Lecture.Where(l => l.CodeEvent.Name == name)
                                  .FirstOrDefaultAsync(l => l.Id == id);

        }

        public async Task AddAsync(Lecture lecture)
        {
            if (lecture is null)
            {
                throw new ArgumentNullException(nameof(lecture));
            }

            await db.Lecture.AddAsync(lecture);
        }
    }
}
