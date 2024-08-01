using ClassRoom.Application.Models;
using ClassRoom.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClassRoom.Api.Controllers
{
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ClassRoomController(ClassRoomService service) : ControllerBase
    {
        private readonly ClassRoomService _service = service ?? throw new ArgumentNullException(nameof(service));

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<ClassRoomDto>>> GetAllAsync()
        {
            return Ok(await _service.GetClassRoomsAsync());
        }

        [HttpGet("{classRoomId:guid}", Name = nameof(GetByIdAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ClassRoomDto>> GetByIdAsync(Guid classRoomId)
        {
            var classRoom = await _service.GetClassRoomByIdAsync(classRoomId);
            if (classRoom == null)
            {
                return NotFound();
            }

            return Ok(classRoom);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ClassRoomDto>> CreateclassRoomAsync(CreateClassRoomRequest request)
        {
            var classRoom = await _service.CreateClassRoomAsync(request);
            return CreatedAtRoute(classRoom.Id, classRoom);
        }

        [HttpPut("{classRoomId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ClassRoomDto>> UpdateclassRoomAsync(UpdateClassRoomRequest request, Guid classRoomId)
        {
            var classRoom = await _service.GetClassRoomByIdAsync(classRoomId);
            if (classRoom == null)
            {
                return NotFound();
            }

            request.Id = classRoomId;
            var updated = await _service.UpdateClassRoomAsync(request);
            return Ok(updated);
        }

        [HttpDelete("{classRoomId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ClassRoomDto>> DeleteclassRoomAsync(Guid classRoomId)
        {
            var classRoom = await _service.GetClassRoomByIdAsync(classRoomId);
            if (classRoom == null)
            {
                return NotFound();
            }

            await _service.RemoveClassRoomAsync(classRoomId);
            return NoContent();
        }
    }
}
