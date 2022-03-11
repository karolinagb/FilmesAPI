using AutoMapper;
using FilmesApi.Data.Dtos.Sessao;
using FilmesAPI.Models;

namespace FilmesApi.Profiles
{
    public class SessaoProfile : Profile
    {
        public SessaoProfile()
        {
            CreateMap<CreateSessaoDto, Sessao>();
            CreateMap<Sessao, ReadSessaoDto>()
                .ForMember(x => x.HorarioDeInicio, opts => opts
                .MapFrom(x => x.HorarioEncerramento.AddMinutes(x.Filme.Duracao * (-1))));
        }
    }
}
