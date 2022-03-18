using FluentResults;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using UsuariosApi.Data.Request;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    public class LoginService
    {
        private readonly SignInManager<CustomIdentityUser> _signInManager;

        private readonly TokenService _tokenServico;

        public LoginService(SignInManager<CustomIdentityUser> signInManager, TokenService tokenServico)
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

                var token = _tokenServico.CreateToken(usuario, _signInManager
                    .UserManager.GetRolesAsync(usuario).Result
                    .FirstOrDefault()
                    );

                //Returnando Ok e algum valor de suceso junto
                return Result.Ok().WithSuccess(token.Value);
            }

            return Result.Fail(resultIdentity.Result.ToString());
        }

        public Result ResetSenhaUsuario(EfetuaResetRequest request)
        {
            CustomIdentityUser usuarioIdentity = RecuperaUsuarioPorEmail(request.Email);

            var resultadoIdentity = _signInManager.UserManager
                .ResetPasswordAsync(usuarioIdentity, request.CodigoDeRecuperacao, request.Password).Result;

            if (resultadoIdentity.Succeeded)
            {
                return Result.Ok().WithSuccess("Senha redefinida com sucesso!");
            }

            return Result.Fail("Houve um erro na operação");
        }


        public Result SolicitaResetSenhaUsuario(SolicitaResetRequest request)
        {
            CustomIdentityUser usuarioIdentity = RecuperaUsuarioPorEmail(request.Email);

            if (usuarioIdentity != null)
            {
                var codigoDeRecuperacao = _signInManager.UserManager
                    .GeneratePasswordResetTokenAsync(usuarioIdentity).Result;

                return Result.Ok().WithSuccess(codigoDeRecuperacao);
            }
            return Result.Fail("Falha ao solicitar redefinição de senha");
        }
        private CustomIdentityUser RecuperaUsuarioPorEmail(string email)
        {
            return _signInManager.UserManager.Users
                          .FirstOrDefault(x => x.Email == email);
        }
    }
}
