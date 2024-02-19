using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PatientSearch.Application.Dto;
using PatientSearch.Application.Interfaces;
using PatientSearch.Application.Request;

namespace PatientSearch.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly ILogger<PatientController> _logger;
        public PatientController(IPatientService patientService ,ILogger<PatientController> logger)
        {
            _patientService = patientService;
            _logger = logger;
        }

        [HttpGet("SearchPatients")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Error))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PatientDetails))]
        public async Task<IActionResult> SearchPatients([FromQuery] PatientRequest patientRequest)
        {
            _logger.LogInformation("Patient serch request starting!");
            var result = await _patientService.SearchPatients(patientRequest);
            return Ok(result);
        }
    }
}