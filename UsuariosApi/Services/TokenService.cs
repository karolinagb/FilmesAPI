using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    public class TokenService
    {
        public Token CreateToken(IdentityUser<int> usuario)
        {
            //direitos do usuário
            Claim[] direitos = new Claim[]
            {
                new Claim("username", usuario.UserName),
                new Claim("id", usuario.Id.ToString())
            };

            //chave para criptografar o token
            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("aaafdfdsfjdsfskdjbbbfjdfsdfksdfdjs"));

            //gerar credenciais
            var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            //Gerar token
            var token = new JwtSecurityToken(
                claims: direitos, 
                signingCredentials: credenciais,
                expires: DateTime.UtcNow.AddHours(1)
                );

            //Transformar o token em string
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new Token(tokenString);
        }
    }
}
