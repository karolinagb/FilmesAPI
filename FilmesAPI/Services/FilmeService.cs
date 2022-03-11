using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FilmesAPI.Services
{
    public class FilmeService
    {
        private readonly FilmeDbContext _context;
        private readonly IMapper _mapper;

        public FilmeService(FilmeDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ReadFilmeDto AdicionaFilme(CreateFilmeDto filmeDto)
        {
            var filme = _mapper.Map<Filme>(filmeDto);
            _context.Filmes.Add(filme);
            _context.SaveChanges();

            return _mapper.Map<ReadFilmeDto>(filme);
        }

        public List<ReadFilmeDto> RecuperaFilmes(int? classificacaoEtaria)
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
                return readDto;
            }

            return null;
        }

        public ReadFilmeDto RecuperaFilmesPorId(int id)
        {
            var filme = _context.Filmes.FirstOrDefault(x => x.Id == id);
            if (filme != null)
            {
                ReadFilmeDto readFilmeDto = _mapper.Map<ReadFilmeDto>(filme);
                return readFilmeDto;
            }

            return null;
        }

        public Result AtualizaFilme(int id, UpdateFilmeDto filmeNovo)
        {
            var filme = _context.Filmes.FirstOrDefault(x => x.Id == id);

            if (filme == null)
            {
                return Result.Fail("Filme não encontrado");
            }

            //filme = _mapper.Map<Filme>(filmeNovo);
            _mapper.Map(filmeNovo, filme);

            _context.Update(filme);
            _context.SaveChanges();

            return Result.Ok();
        }

        public Result RemoverFilme(int id)
        {
            var filme = _context.Filmes.FirstOrDefault(x => x.Id == id);

            if (filme == null)
            {
                return Result.Fail("Filme não encontrado");
            }

            _context.Remove(filme);
            _context.SaveChanges();

            return Result.Ok();
        }
    }
}
