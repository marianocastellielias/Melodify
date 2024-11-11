using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        [HttpPost("authenticate")]
        public IActionResult Authentication([FromBody] AuthenticationValidate authenticationValidate)
        {
            string token = _authenticationService.Authenticate(authenticationValidate);

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Credenciales incorrectas");
            }

            return Ok(token);
        }
    }
}
