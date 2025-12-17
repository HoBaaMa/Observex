using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using SafetyVision.Application.DTOs.Violations;
using SafetyVision.Application.Interfaces;
using SafetyVision.Core.Enums;

namespace SafetyVision.API.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ViolationsController : ControllerBase
    {
        private readonly IViolationService _violationService;
        public ViolationsController(IViolationService violationService)
        {
            _violationService = violationService;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<ViolationDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllViolations()
        {
            var violations = await _violationService.GetAllAsync();
            return Ok(violations.Value);
        }
        // TODO: Fix the date input format
        [HttpGet("date/{date:datetime}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<ViolationDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllViolationsByDate(DateTime date)
        {
            var violations = await _violationService.GetViolationsByDateAsync(date);
            return Ok(violations.Value);
        }
        [HttpGet("worker/{workerId:Guid}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<ViolationDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllViolationsForWorker(Guid workerId)
        {
            var result = await _violationService.GetWorkerViolationsByIdAsync(workerId);
            if (!result.IsSuccess)
            {
                return result.ErrorType switch
                {
                    ErrorType.NotFound => NotFound(result.Errors),
                    _ => StatusCode(500, result.Errors)
                };
            }
            return Ok(result.Value);
        }
        [HttpGet("{id:Guid}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ViolationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetViolationById(Guid id)
        {
            var result = await _violationService.GetByIdAsync(id);
            if (!result.IsSuccess)
            {
                return result.ErrorType switch
                {
                    ErrorType.NotFound => NotFound(result.Errors),
                    _ => StatusCode(500, result.Errors)
                };
            }
            return Ok(result.Value);
        }
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ViolationDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddViolation([FromBody] PostAddViolationDto dto, [FromHeader(Name = "X-Officer-ID")] Guid officerId) 
            // In production, get the officerId from JWT claims
        {
            var result = await _violationService.CreateAsyncWithNotification(dto, officerId);
            if (!result.IsSuccess)
            {
                return result.ErrorType switch
                {
                    ErrorType.NotFound => NotFound(result.Errors),
                    _ => StatusCode(500, result.Errors)
                };
            }

            return CreatedAtAction(nameof(GetViolationById), new { Id = result.Value!.Id }, result.Value);
        }
    }
}
