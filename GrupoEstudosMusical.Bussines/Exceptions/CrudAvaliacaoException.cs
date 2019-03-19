
using System;

namespace GrupoEstudosMusical.Bussines.Exceptions
{
    public class CrudAvaliacaoException : Exception
    {
        public CrudAvaliacaoException(){}
        public override string Message { get; }
        public CrudAvaliacaoException(string mensagem)
        {
            Message = mensagem;
        }
    }
}
