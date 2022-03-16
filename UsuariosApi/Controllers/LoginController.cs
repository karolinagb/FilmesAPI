using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
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

        [HttpPost("/solicita-reset")]
        public IActionResult SolicitaResetSenhaUsuario(SolicitaResetRequest request)
        {
            Result result = _loginService.SolicitaResetSenhaUsuario(request);

            if (result.IsFailed)
            {
                return Unauthorized(result.Errors);
            }

            return Ok(result.Successes);
        }

        [HttpPost("/efetua-reset")]
        public IActionResult ResetaSenhaUsuario(EfetuaResetRequest request)
        {
            Result result = _loginService.ResetSenhaUsuario(request);

            if (result.IsFailed)
            {
                return Unauthorized(result.Errors);
            }

            return Ok(result.Successes);
        }


    }
}
