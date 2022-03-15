using MimeKit;
using System.Collections.Generic;
using System.Linq;

namespace UsuariosApi.Models
{
    public class Mensagem
    {
        //MailboxAddress = tipo email
        public List<MailboxAddress> Destinatarios { get; set; }
        public string Assunto { get; set; }
        public string Conteudo { get; set; }

        //conteudo no nosso caso é o código de ativação
        public Mensagem(IEnumerable<string> destinatarios, string assunto, int usuarioId, string code)
        {
            Destinatarios = new List<MailboxAddress>();
            Destinatarios.AddRange(destinatarios.Select(d => new MailboxAddress("", d)));

            Assunto = assunto;
            Conteudo = $"https://localhost:6001/ativa?UsuarioId={usuarioId}&CodigoDeAtivacao={code}";
        }
    }
}
