using Bogus.DataSets;
using CodeEvents.Api.Core.DTOs;
using CodeEvents.Api.Core.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace CodeEvents.Api.Filters
{
    public class EventExistsFilter : ActionFilterAttribute
    {
        private readonly string actionArg;
        private readonly IUnitOfWork uow;
        private readonly ProblemDetailsFactory problemDetailsFactory;

        public EventExistsFilter(string actionArg, IUnitOfWork uow, ProblemDetailsFactory problemDetailsFactory)
        {
            this.actionArg = actionArg;
            this.uow = uow;
            this.problemDetailsFactory = problemDetailsFactory;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if(context.ActionArguments.TryGetValue(actionArg, out object? dto))
            {
                if(dto is CreateCodeEventDto createCodeEventDto)
                {
                    var codeEvent = await uow.CodeEventRepository.GetAsync(createCodeEventDto.Name);
                    if(codeEvent is not null)
                    {
                        context.Result = new BadRequestObjectResult(problemDetailsFactory.CreateProblemDetails(context.HttpContext,
                                                                         StatusCodes.Status400BadRequest,
                                                                         title: "Codeevent already exists",
                                                                         detail: $"The codeevent {createCodeEventDto.Name}  exists"));
                    }
                }
            }

            await base.OnActionExecutionAsync(context, next);
        }
    }
}
