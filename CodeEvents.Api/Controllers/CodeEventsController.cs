using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CodeEvents.Api.Core.Entities;
using CodeEvents.Api.Data;
using CodeEvents.Api.Data.Repositories;
using CodeEvents.Api.Core.DTOs;
using AutoMapper;

namespace CodeEvents.Api.Controllers
{
    [Route("api/events")]
    [ApiController]
    public class CodeEventsController : ControllerBase
    {
        private readonly IMapper mapper;
        private UnitOfWork uow;

        public CodeEventsController(CodeEventsApiContext db, IMapper mapper)
        {
            uow = new UnitOfWork(db);
            this.mapper = mapper;
        }

        // GET: api/CodeEvents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CodeEventDto>>> GetCodeEvent(bool includeLectures)
        {
            var events = await uow.CodeEventRepository.GetAsync(includeLectures);
            var dto = mapper.Map<IEnumerable<CodeEventDto>>(events); 
            return Ok(dto);
        }
        
    }
}
