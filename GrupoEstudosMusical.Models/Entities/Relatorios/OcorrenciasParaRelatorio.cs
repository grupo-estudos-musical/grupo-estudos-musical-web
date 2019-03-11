

using System;

namespace GrupoEstudosMusical.Models.Entities.Relatorios
{
    public class OcorrenciasParaRelatorio
    {
        public OcorrenciasParaRelatorio(string nomeAluno, string titulo, DateTime dataOcorrido, string observacao, string nomeProfessor, int codigoOcorrencia)
        {
            NomeDoAluno = nomeAluno;
            Titulo = titulo;
            DataDoOcorrido = dataOcorrido;
            Observacao = observacao;
            NomeProfessor = nomeProfessor;
            CodigoOcorrencia = codigoOcorrencia;
        }
        public int CodigoOcorrencia { get; private set; }
        public string NomeDoAluno { get; private set; }
        public string Titulo { get;  private set; }
        public DateTime  DataDoOcorrido { get; private set; }
        public string Observacao { get; private set; }
        public string NomeProfessor { get; private set; }
    }
}
