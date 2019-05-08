using System;

namespace GrupoEstudosMusical.Bussines.Exceptions
{
    public class CrudEmprestimoException: Exception
    {
        public CrudEmprestimoException() { }
        public override string Message { get; }
        public CrudEmprestimoException(string mensagem)
        {
            Message = mensagem;
        }
    }
}
