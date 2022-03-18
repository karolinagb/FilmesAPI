using Microsoft.AspNetCore.Authorization;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FilmesAPI.Authorization
{
    //Classe que manipula a autorização requerida
    //ou seja
    //Essa classe vai fazer a vericação com a data de nascimento da pessoa
    public class IdadeMinimaHandler : AuthorizationHandler<IdadeMinimaRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, 
            IdadeMinimaRequirement requirement)
        {
            //Se o usuario nao tiver a claim de data de nascimento, retornamos uma tarefa concluida
            //Concluida no sentido de que nao autorizamos nada
            if(!context.User.HasClaim(c => c.Type == ClaimTypes.DateOfBirth))
            {
                return Task.CompletedTask;
            }

            //Converte a data de nascimento em DateTime pois ela venha em formato de texto do token
            //Pegando o valor da claim de data de nascimento
            DateTime dataNascimento = Convert.ToDateTime(context.User.FindFirst(c =>
                c.Type == ClaimTypes.DateOfBirth).Value);

            var idadeObtida = DateTime.Today.Year - dataNascimento.Year;

            //Pode ser que a pessoa não tenha feito aniversário nesse ano

            //se a dataNascimento for maior que a data atual (do ano que ela nasceu)
            if(dataNascimento > DateTime.Today.AddYears(-idadeObtida))
            {
                //decrementa a idade porque a pessoa nao fez aniversario
                //idade real dela
                idadeObtida--;
            }


            if(idadeObtida >= requirement.IdadeMinima)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
