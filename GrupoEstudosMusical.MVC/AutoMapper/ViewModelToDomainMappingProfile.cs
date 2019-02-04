using AutoMapper;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.MVC.Models;

namespace GrupoEstudosMusical.MVC.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ModuloVM, Modulo>();
            CreateMap<AlunoVM, Aluno>();
            CreateMap<ProfessorVM, Professor>();
            CreateMap<TurmaVM, Turma>();
        }
    }
}