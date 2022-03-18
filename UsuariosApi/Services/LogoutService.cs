using FluentResults;
using Microsoft.AspNetCore.Identity;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    public class LogoutService
    {
        private SignInManager<CustomIdentityUser> _sigInManager;

        public LogoutService(SignInManager<CustomIdentityUser> sigInManager)
        {
            _sigInManager = sigInManager;
        }

        public Result DeslogaUsuario()
        {
            var resultIdentity = _sigInManager.SignOutAsync();

            //Retornado com sucesso
            if (resultIdentity.IsCompletedSuccessfully)
            {
                return Result.Ok();
            }

            return Result.Fail("Logout falhou");
        }
    }
}
