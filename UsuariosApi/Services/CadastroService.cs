using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UsuariosApi.Data.Dtos.Usuario;
using UsuariosApi.Data.Request;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    public class CadastroService
    {
        private IMapper _mapper;
        private UserManager<CustomIdentityUser> _userManager;
        private readonly EmailService _emailService;

        public CadastroService(IMapper mapper, UserManager<CustomIdentityUser> userManager, 
            EmailService emailService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _emailService = emailService;
        }

        public Result CadastraUsuario(CreateUsuarioDto createDto)
        {
            Usuario usuario = _mapper.Map<Usuario>(createDto);
            
            CustomIdentityUser usuarioIdentity = _mapper.Map<CustomIdentityUser>(usuario);
            
            Task<IdentityResult> resultadoIdentity = _userManager.CreateAsync(usuarioIdentity, createDto.Password);

            _userManager.AddToRoleAsync(usuarioIdentity, "regular");
            
            if (resultadoIdentity.Result.Succeeded) 
            {
                //Result = pega o resultado e aguarda por ele ser preenchido na variavel code
                var code = _userManager.GenerateEmailConfirmationTokenAsync(usuarioIdentity).Result;

                //Para evitar que caracteres sejam convertidos nós encodamos o code no http
                var encodedCode = HttpUtility.UrlEncode(code);

                _emailService.EnviarEmail(new[] { usuarioIdentity.Email },  "Link de ativação", 
                    usuarioIdentity.Id, encodedCode);

                return Result.Ok().WithSuccess(code);
            } 

            return Result.Fail("Falha ao cadastrar usuário");
        }

        public Result AtivaContaUsuario(AtivaContaRequest request)
        {
            var customIdentityUser = _userManager.Users.FirstOrDefault(x => x.Id == request.UsuarioId);

            var identityResult = _userManager.ConfirmEmailAsync(customIdentityUser, request.CodigoDeAtivacao).Result;

            if (identityResult.Succeeded)
            {
                return Result.Ok();
            }

            return Result.Fail("Falha ao ativar conta de usuário");
        }
    }
}
