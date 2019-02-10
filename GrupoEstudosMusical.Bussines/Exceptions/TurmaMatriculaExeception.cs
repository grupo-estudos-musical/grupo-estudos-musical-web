using System;

namespace GrupoEstudosMusical.Bussines.Exceptions
{
    public class TurmaMatriculaExeception : Exception
    {
        public string Mensagem { get; }

        public TurmaMatriculaExeception() { }

        public TurmaMatriculaExeception(string mensagem)
        {
            Mensagem = mensagem;
        }

    }
}
