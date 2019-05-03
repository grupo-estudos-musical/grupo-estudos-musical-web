using System;


namespace GrupoEstudosMusical.Bussines.Exceptions
{
    public class CrudInventarioException : Exception
    {
        public CrudInventarioException() { }
        public override string Message { get; }
        public CrudInventarioException(string mensagem)
        {
            Message = mensagem;
        }
    }
}
