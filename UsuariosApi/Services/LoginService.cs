using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using UsuariosApi.Data.Request;

namespace UsuariosApi.Services
{
    public class LoginService
    {
        private readonly SignInManager<IdentityUser<int>> _signInManager;

        private readonly TokenService _tokenServico;

        public LoginService(SignInManager<IdentityUser<int>> signInManager, TokenService tokenServico)
        {
            _signInManager = signInManager;
            _tokenServico = tokenServico;
        }

        public Result LogarUsuario(LoginRequest request)
        {
            var resultIdentity = _signInManager.PasswordSignInAsync(request.UserName,
                request.Password, isPersistent: false, lockoutOnFailure: false);

            if (resultIdentity.Result.Succeeded)
            {
                //Dá para acessar o UserManager a partir do signInManager
                var usuario = _signInManager.UserManager.Users
                    .FirstOrDefault(x =>
                        x.NormalizedUserName == request.UserName.ToUpper());

                var token = _tokenServico.CreateToken(usuario);

                //Returnando Ok e algum valor de suceso junto
                return Result.Ok().WithSuccess(token.Value);
            }

            return Result.Fail("Login Falhou");
        }
    }
}
