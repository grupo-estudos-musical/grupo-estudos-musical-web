using System;

namespace GrupoEstudosMusical.Bussines.Exceptions
{
    public class CrudTurmaException:Exception
    {
        public override string Message { get; }
        public CrudTurmaException(){}
        public CrudTurmaException(string mensagem)
        {
            Message = mensagem;
        }
    }
}
