using AutoMapper;
using CodeEvents.Api.Core.DTOs;
using CodeEvents.Api.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace CodeEvents.Api.Controllers
{
    [Route("api/events/{name}/lectures")]
    [ApiController]
    public class LecturesController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly ProblemDetailsFactory problemDetailsFactory;
        private readonly IMapper mapper;

        public LecturesController(IUnitOfWork uow, ProblemDetailsFactory problemDetailsFactory, IMapper mapper)
        {
            this.uow = uow;
            this.problemDetailsFactory = problemDetailsFactory;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LectureDto>>> GetLecturesForEvent(string name)
        {
            if(await uow.CodeEventRepository.GetAsync(name) is null)
            {
                return NotFound(problemDetailsFactory.CreateProblemDetails(HttpContext,
                                                                          StatusCodes.Status404NotFound,
                                                                          title: "Codeevent ´not exists",
                                                                          detail: $"The codeevent {name} doesent exist"));
            }

            var lectures = await uow.LecturesRepository.GetAsync(name);
            return Ok(mapper.Map<IEnumerable<LectureDto>>(lectures));
        } 
        
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<IEnumerable<LectureDto>>> GetLecture(string name, int id)
        {
            if(await uow.CodeEventRepository.GetAsync(name) is null)
            {
                return NotFound(problemDetailsFactory.CreateProblemDetails(HttpContext,
                                                                          StatusCodes.Status404NotFound,
                                                                          title: "Codeevent ´not exists",
                                                                          detail: $"The codeevent {name} doesent exist"));
            }

            var lecture = await uow.LecturesRepository.GetAsync(name, id);

            if (lecture == null) return NotFound();

            return Ok(mapper.Map<LectureDto>(lecture));
        }
    }
}
