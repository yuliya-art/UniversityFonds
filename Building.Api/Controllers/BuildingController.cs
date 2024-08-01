using Building.Application.Models;
using Building.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Building.Api.Controllers
{
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class BuildingsController: ControllerBase
    {
        private readonly BuildingsService _buildingsService;

        public BuildingsController(BuildingsService buildingsService)
        {
            _buildingsService = buildingsService ?? throw new ArgumentNullException(nameof(buildingsService));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<BuildingDto>>> GetBuildingsAsync()
        {
            return Ok(await _buildingsService.GetBuildingsAsync());
        }

        [HttpGet("{buildingId:guid}", Name = nameof(GetBuildingAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BuildingDto>> GetBuildingAsync(Guid buildingId)
        {
            var building = await _buildingsService.GetBuildingByIdAsync(buildingId);
            if (building == null)
            {
                return NotFound();
            }

            return Ok(building);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BuildingDto>> CreateBuildingAsync(CreateBuildingRequest request)
        {
            var building = await _buildingsService.CreateBuildingAsync(request);
            return CreatedAtRoute(building.Id, building);
        }

        [HttpPut("{buildingId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BuildingDto>> UpdateBuildingAsync(UpdateBuildingRequest request, Guid buildingId)
        {
            var building = await _buildingsService.GetBuildingByIdAsync(buildingId);
            if (building == null)
            {
                return NotFound();
            }

            request.Id = buildingId;
            var updated = await _buildingsService.UpdateBuildingAsync(request);
            return Ok(updated);
        }

        [HttpDelete("{buildingId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteBuildingAsync(Guid buildingId)
        {
            var building = await _buildingsService.GetBuildingByIdAsync(buildingId);
            if (building == null)
            {
                return NotFound();
            }

            await _buildingsService.RemoveBuildingAsync(buildingId);
            return NoContent();
        }
    }
}
