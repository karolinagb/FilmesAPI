using FluentResults;
using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Data.Request;
using UsuariosApi.Services;

namespace UsuariosApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly LoginService _loginService;

        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public IActionResult LogarUsuario(LoginRequest request)
        {
            Result result = _loginService.LogarUsuario(request);

            if (result.IsFailed)
            {
                return Unauthorized(result.Errors);
            }

            //Retornos de sucesso do result
            return Ok(result.Successes);
        }
    }
}
