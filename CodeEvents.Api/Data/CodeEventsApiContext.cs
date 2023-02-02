using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CodeEvents.Api.Core.Entities;

namespace CodeEvents.Api.Data
{
    public class CodeEventsApiContext : DbContext
    {
        public DbSet<CodeEvent> CodeEvent => Set<CodeEvent>();
        public CodeEventsApiContext (DbContextOptions<CodeEventsApiContext> options)
            : base(options)
        {
        }

    }
}
