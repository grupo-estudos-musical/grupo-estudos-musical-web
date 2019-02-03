using AutoMapper;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.MVC.Models;

namespace GrupoEstudosMusical.MVC.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Aluno, AlunoVM>();
            CreateMap<Professor, ProfessorVM>();
            CreateMap<Modulo, ModuloVM>();
        }
    }
}