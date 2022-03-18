using FilmesAPI.Data.Dtos;
using FilmesAPI.Services;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {

        private readonly FilmeService _filmeService;

        public FilmeController(FilmeService filmeService)
        {
            _filmeService = filmeService;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult AdicionaFilme([FromBody] CreateFilmeDto filmeDto)
        {
            ReadFilmeDto readDto = _filmeService.AdicionaFilme(filmeDto);
            return CreatedAtAction(nameof(RecuperarFilmesPorId), new { id = readDto.Id }, readDto);
        }

        [HttpGet]
        [Authorize(Roles = "admin, regular", Policy = "IdadeMinima")]
        public IActionResult RecuperaFilmes([FromQuery] int? classificacaoEtaria = null)
        {
            List<ReadFilmeDto> readDto = _filmeService.RecuperaFilmes(classificacaoEtaria);

            if(readDto != null)
            {
                return Ok(readDto);
            }
          
            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult RecuperarFilmesPorId(int id)
        {
            ReadFilmeDto readDto = _filmeService.RecuperaFilmesPorId(id);
           
            if(readDto != null)
            {
                return Ok(readDto);
            }

            return NotFound("Recurso não encontrado");
        }

        [HttpPut("{id}")]
        public ActionResult AtualizaFilme(int id, [FromBody] UpdateFilmeDto filmeNovo)
        {
            Result resultado = _filmeService.AtualizaFilme(id, filmeNovo);

            if (resultado.IsFailed)
            {
                return NotFound("Filme não encontrado");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult RemoverFilme(int id)
        {
            Result resultado = _filmeService.RemoverFilme(id);

            if (resultado.IsFailed)
            {
                return NotFound("Filme não encontrado");
            }

            return NoContent();
        }
    }
}
