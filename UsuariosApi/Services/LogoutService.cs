using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsuariosApi.Services
{
    public class LogoutService
    {
        private SignInManager<IdentityUser<int>> _sigInManager;

        public LogoutService(SignInManager<IdentityUser<int>> sigInManager)
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
