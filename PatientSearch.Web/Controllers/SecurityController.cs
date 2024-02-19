using Microsoft.AspNetCore.Mvc;
using PatientSearch.Application.Interfaces;

namespace PatientSearch.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IPatientService _patientService;
        public SecurityController(ITokenService tokenService, IPatientService patientService)
        {
            _tokenService = tokenService;
            _patientService = patientService;
        }
        [HttpGet("Token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Authenticate([FromQuery] string username, [FromQuery] string password)
        {
            if (await _patientService.ValidateUser(username, password))
            {
                return Ok(new { accesstoken = _tokenService.CreateJwtSecurityToken(username) });
            }
            else
            {
                return BadRequest("Please enter valide credentials!");
            }
        }
    }
}

