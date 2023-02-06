using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeEvents.Api.Controllers
{
    [Route("api/events/{name}/lectures")]
    [ApiController]
    public class LecturesController : ControllerBase
    {
        public LecturesController()
        {

        }
    }
}
