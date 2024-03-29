﻿using CodeEvents.Api.Core.Entities;

namespace CodeEvents.Api.Core.Repositories
{
    public interface ICodeEventRepository
    {
        Task AddAsync(CodeEvent codeEvent);
        Task<IEnumerable<CodeEvent>> GetAsync(bool includeLectures = false);
        Task<CodeEvent?> GetAsync(string name, bool includeLectures = false);
    }
}