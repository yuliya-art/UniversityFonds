using ClassRoom.Application.Models;
using ClassRoom.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClassRoom.Api.Controllers
{
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ClassRoomTypesController(ClassRoomTypeService service) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<ClassRoomTypeDto>>> GetAllAsync()
        {
            return Ok(await service.GetClassRoomTypesAsync());
        }
    }
}
