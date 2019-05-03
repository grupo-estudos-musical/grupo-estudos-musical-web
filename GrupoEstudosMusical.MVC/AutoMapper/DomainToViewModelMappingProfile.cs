using AutoMapper;
using GrupoEstudosMusical.Models;
using GrupoEstudosMusical.Models.Entities;
using GrupoEstudosMusical.MVC.Models;
using static GrupoEstudosMusical.Models.Entities.Instrumento;

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
            CreateMap<AvaliacaoTurma, AvaliacaoTurmaVM>();
            CreateMap<Chamada, ChamadaVM>();
            CreateMap<Frequencia, FrequenciaVM>();
            CreateMap<Aula, AulaVM>();
            CreateMap<Instrumento, InstrumentoVM>();
            CreateMap<Fabricante, FabricanteVM>();
            CreateMap<InstrumentoDoAluno, InstrumentoDoAlunoVM>();
        }
    }
}