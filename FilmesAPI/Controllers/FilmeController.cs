using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {

        private readonly FilmeDbContext _context;
        private readonly IMapper _mapper;

        public FilmeController(FilmeDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public ActionResult AdicionaFilme([FromBody] CreateFilmeDto filmeDto)
        {
            var filme = _mapper.Map<Filme>(filmeDto);
            _context.Filmes.Add(filme);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperarFilmesPorId), new { id = filme.Id }, filme);
        }

        [HttpGet]
        public IActionResult RecuperaFilmes([FromQuery] int? classificacaoEtaria = null)
        {
            List<Filme> filmes;
            if (classificacaoEtaria == null)
            {
                filmes = _context.Filmes.ToList();
            }
            else
            {
                filmes = _context
                .Filmes.Where(filme => filme.ClassificacaoEtaria <= classificacaoEtaria).ToList();
            }

            if (filmes != null)
            {
                List<ReadFilmeDto> readDto = _mapper.Map<List<ReadFilmeDto>>(filmes);
                return Ok(readDto);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult RecuperarFilmesPorId(int id)
        {
            var filme = _context.Filmes.FirstOrDefault(x => x.Id == id);
            if (filme != null)
            {
                ReadFilmeDto readFilmeDto = _mapper.Map<ReadFilmeDto>(filme);
                return Ok(readFilmeDto);
            }

            return NotFound();
        }

        [HttpPut("{id}")]
        public ActionResult AtualizaFilme(int id, [FromBody] UpdateFilmeDto filmeNovo)
        {
            var filme = _context.Filmes.FirstOrDefault(x => x.Id == id);

            if(filme == null)
            {
                return NotFound("Recurso não encontrado");
            }

            //filme = _mapper.Map<Filme>(filmeNovo);
            _mapper.Map(filmeNovo, filme);

            _context.Update(filme);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult RemoverFilme(int id)
        {
            var filme = _context.Filmes.FirstOrDefault(x => x.Id == id);

            if (filme == null)
            {
                return NotFound("Filme não existe");
            }

            _context.Remove(filme);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
