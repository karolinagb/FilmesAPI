﻿using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.Dtos
{
    public class UpdateFilmeDto
    {

        [Required(ErrorMessage = "O campo de título é obrigatório")]
        public string Titulo { get; set; }

        [StringLength(100, ErrorMessage = "O nome do diretor não pode exceder 100 caracteres")]
        public string Diretor { get; set; }

        public string Genero { get; set; }

        [Range(1, 600, ErrorMessage = "A duração deve ter no mínimo 1 minuto e no máximo 600.")]
        public int Duracao { get; set; }
        public int ClassificacaoEtaria { get; set; }
    }
}
