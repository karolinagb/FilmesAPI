using FilmesAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FilmesApi.Models
{
    public class Endereco
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public int Numero { get; set; }

        //[JsonIgnore] - Resolve o problema de informação cíclica
        //Ignorar a propriedade Cinema de endereço no carregamento das propriedades
        [JsonIgnore]
        public virtual Cinema Cinema { get; set; }
    }
}
