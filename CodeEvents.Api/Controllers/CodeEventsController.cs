﻿using AutoMapper;
using CodeEvents.Api.Core.DTOs;
using CodeEvents.Api.Core.Entities;
using CodeEvents.Api.Core.Repositories;
using CodeEvents.Api.Data;
using CodeEvents.Api.Data.Repositories;
using CodeEvents.Api.Filters;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CodeEvents.Api.Controllers
{
    [Route("api/events")]
    [ApiController]
    [Produces("application/json", "application/xml")]
    public class CodeEventsController : ControllerBase
    {
        private readonly IMapper mapper;
        private IUnitOfWork uow;

        public CodeEventsController(CodeEventsApiContext db, IMapper mapper)
        {
            uow = new UnitOfWork(db);
            this.mapper = mapper;
            var x = "Kalle";
        }


        /// <summary>
        /// Get all events
        /// </summary>
        /// <param name="includeLectures">Set to true to include lectures for each event</param>
        /// <returns>Name, lenght, date, location info</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CodeEventDto>>> GetCodeEvent(bool includeLectures)
        {
            return BadRequest();
            var events = await uow.CodeEventRepository.GetAsync(includeLectures);
            var dto = mapper.Map<IEnumerable<CodeEventDto>>(events);
            return Ok(dto);
        }

        [HttpGet]
        [Route("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CodeEventDto>> GetCodeEvent(string name, bool includeLectures)
        {
            if (string.IsNullOrWhiteSpace(name)) return BadRequest();

            var codeevent = await uow.CodeEventRepository.GetAsync(name, includeLectures);

            if (codeevent == null) return NotFound();

            var dto = mapper.Map<CodeEventDto>(codeevent);

            return Ok(dto);
        }

        [HttpPost]
        [TypeFilter(typeof(EventExistsFilter), Arguments = new object[] { "dto" })]
        [Consumes("appliction/json", "application/xml")]
        public async Task<ActionResult<CodeEventDto>> CreateCodeEvent(CreateCodeEventDto dto)
        {
            //if(await uow.CodeEventRepository.GetAsync(dto.Name) != null)
            //{
            //    ModelState.AddModelError("Name", "Name exists");
            //    return BadRequest(ModelState);
            //}

            var codeEvent = mapper.Map<CodeEvent>(dto);
            await uow.CodeEventRepository.AddAsync(codeEvent);
            await uow.CompleteAsync();

            return CreatedAtAction(nameof(GetCodeEvent), new { name = codeEvent.Name }, mapper.Map<CodeEventDto>(dto));
        }

        [HttpPut("{name}")]
        public async Task<ActionResult<CodeEventDto>> PutEvent(string name, CodeEventDto dto)
        {
            var codeEvent = await uow.CodeEventRepository.GetAsync(name);
            if (codeEvent == null) return NotFound();

            mapper.Map(dto, codeEvent);
            await uow.CompleteAsync();

            return Ok(mapper.Map<CodeEventDto>(codeEvent));
        }

        [HttpPatch("{name}")]
        public async Task<ActionResult<CodeEventDto>> PatchEvent(string name, JsonPatchDocument<CodeEventDto> patchDocument)
        {
            var codeEvent = await uow.CodeEventRepository.GetAsync(name, true);
            if (codeEvent == null) return NotFound();

            var dto = mapper.Map<CodeEventDto>(codeEvent);

            patchDocument.ApplyTo(dto, ModelState);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            mapper.Map(dto, codeEvent);
            await uow.CompleteAsync();

            return Ok(mapper.Map<CodeEventDto>(codeEvent));
        }

    }
}
