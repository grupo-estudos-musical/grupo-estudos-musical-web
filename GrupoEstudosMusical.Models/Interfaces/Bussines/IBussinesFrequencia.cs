using GrupoEstudosMusical.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrupoEstudosMusical.Models.Interfaces.Bussines
{
    public interface IBussinesFrequencia : IBussinesGeneric<Frequencia>
    {
        void Inserir(List<Frequencia> frequencias);
    }
}
