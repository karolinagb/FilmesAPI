using FluentResults;
using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Services;

namespace UsuariosApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogoutController : ControllerBase
    {
        private readonly LogoutService _logoutservice;

        public LogoutController(LogoutService logoutservice)
        {
            _logoutservice = logoutservice;
        }

        [HttpPost]
        public IActionResult DeslogaUsuario()
        {
            Result result = _logoutservice.DeslogaUsuario();

            if (result.IsFailed)
            {
                return Unauthorized(result.Errors);
            }

            return Ok(result.Successes);
        }
    }
}
