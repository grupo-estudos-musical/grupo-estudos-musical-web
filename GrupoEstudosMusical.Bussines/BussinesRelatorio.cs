using GrupoEstudosMusical.Models.Interfaces.Bussines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoEstudosMusical.Bussines
{
    public class BussinesRelatorio : IBussinesRelatorio
    {
        public BussinesRelatorio(IBussinesMatricula bussinesMatricula, IBussinesOcorrencia bussinesOcorrencia,
            IBussinesInstrumentoDoAluno bussinesInstrumentoDoAluno)
        {
            _bussinesMatricula = bussinesMatricula;
            _bussinesOcorrencia = bussinesOcorrencia;
            _bussinesInstrumentoDoAluno = bussinesInstrumentoDoAluno;
        }
        private readonly IBussinesMatricula _bussinesMatricula;
        private readonly IBussinesOcorrencia _bussinesOcorrencia;
        private readonly IBussinesInstrumentoDoAluno _bussinesInstrumentoDoAluno;
        
        public async Task<bool> VerificarExistenciaDeDadosParaRelatorio(int indiceParaRelatorio, int tipoDeRelatorio)
        {
            switch (tipoDeRelatorio)
            {
                case 1:
                    await _bussinesMatricula.ObterBoletimAluno(indiceParaRelatorio);
                    return true;
                case 2:
                    await _bussinesInstrumentoDoAluno.ObterDadosDeEmprestimos(indiceParaRelatorio);
                    return true;
                case 3:
                    await _bussinesOcorrencia.ObterOcorrenciasParaRelatorio(indiceParaRelatorio);
                    return true;
                case 4:
                    await _bussinesMatricula.ObterBoletimDaTurma(indiceParaRelatorio);
                    return true;
                default:                   
                    break;
            }
            return false;
        }
    }
}
