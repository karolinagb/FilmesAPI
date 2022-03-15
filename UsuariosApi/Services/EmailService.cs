using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    public class EmailService
    {
        private IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void EnviarEmail(string[] destinatarios, string assunto, int usuarioId, string code)
        {
            //Compor uma mensagem
            Mensagem mensagem = new Mensagem(destinatarios, assunto, usuarioId, code);

            //Converter a mensagem em um e-mail em si
            var mensagemDeEmail = CriaCorpoDoEmail(mensagem);

            //Enviar e-mail propriamente dito
            Enviar(mensagemDeEmail);
        }


        //Converter a mensagem para enviar ela por email
        private MimeMessage CriaCorpoDoEmail(Mensagem mensagem)
        {
            var mensagemDeEmail = new MimeMessage();

            mensagemDeEmail.From.Add(new MailboxAddress("", 
                _configuration.GetValue<string>("EmailSettings:From")));

            mensagemDeEmail.To.AddRange(mensagem.Destinatarios);

            mensagemDeEmail.Subject = mensagem.Assunto;

            //Não podemos colocar diretamente a string
            //TextPart é um texto do tipo Mime que nosso email vai aceitar
            mensagemDeEmail.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            {
                Text = mensagem.Conteudo
            };

            return mensagemDeEmail;
        }
        private void Enviar(MimeMessage mensagemDeEmail)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    //conexao com algum provedor de e-mail
                    client.Connect(_configuration.GetValue<string>("EmailSettings:SmtpServer"),
                        _configuration.GetValue<int>("EmailSettings:Port"), true);

                    //Removendo autenticação mais complexa
                    client.AuthenticationMechanisms.Remove("XOUATH2");

                    client.Authenticate(_configuration.GetValue<string>("EmailSettings:From"),
                        _configuration.GetValue<string>("EmailSettings:Password"));

                    client.Send(mensagemDeEmail);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
    }
}
