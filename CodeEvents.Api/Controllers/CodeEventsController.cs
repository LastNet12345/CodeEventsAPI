using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CodeEvents.Api.Core.Entities;
using CodeEvents.Api.Data;

namespace CodeEvents.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodeEventsController : ControllerBase
    {
        private readonly CodeEventsApiContext _context;

        public CodeEventsController(CodeEventsApiContext context)
        {
            _context = context;
        }

        // GET: api/CodeEvents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CodeEvent>>> GetCodeEvent()
        {
          if (_context.CodeEvent == null)
          {
              return NotFound();
          }
            return await _context.CodeEvent.ToListAsync();
        }

    }
}
