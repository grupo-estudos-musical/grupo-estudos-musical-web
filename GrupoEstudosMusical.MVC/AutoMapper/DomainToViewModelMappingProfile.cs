using AutoMapper;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.MVC.Models;

namespace GrupoEstudosMusical.MVC.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Turma, TurmaVM>();
            CreateMap<Aluno, AlunoVM>();
            CreateMap<Professor, ProfessorVM>();
            CreateMap<Modulo, ModuloVM>();
            CreateMap<Ocorrencia, OcorrenciaVM>();
            CreateMap<Matricula, MatriculaVM>();
            CreateMap<Matricula, MatriculaListaVM>();
            CreateMap<Avaliacao, AvaliacaoVM>();
        }
    }
}