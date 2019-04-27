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
            CreateMap<OcorrenciaVM, Ocorrencia>();
            CreateMap<MatriculaVM, Matricula>();
            CreateMap<AvaliacaoVM, Avaliacao>();
            CreateMap<AvaliacaoTurmaVM, AvaliacaoTurma>();
            CreateMap<ChamadaVM, Chamada>();
            CreateMap<FrequenciaVM, Frequencia>();
            CreateMap<AulaVM, Aula>();
        }
    }
}