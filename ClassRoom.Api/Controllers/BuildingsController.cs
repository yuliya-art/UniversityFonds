using ClassRoom.Application.Models;
using ClassRoom.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClassRoom.Api.Controllers
{
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class BuildingsController(BuildingService service) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<BuildingDto>>> GetAllAsync()
        {
            return Ok(await service.GetBuildings());
        }
    }
}
