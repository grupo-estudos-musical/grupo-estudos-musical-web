using GrupoEstudosMusical.Bussines.Exceptions;
using GrupoEstudosMusical.Models;
using GrupoEstudosMusical.Models.Interfaces.Bussines;
using GrupoEstudosMusical.Models.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoEstudosMusical.Bussines
{
    public class BussinesInventario : BussinesGeneric<Inventario>, IBussinesInventario
    {
        public BussinesInventario(IRepositoryInventario repository) : base(repository)
        {
            _repositoryInventario = repository;
        }
        readonly IRepositoryInventario _repositoryInventario;

        public List<Inventario> ObterTodosItensDoInventario() =>
            _repositoryInventario.ObterTodosItensDoInventario();

        public async Task ModificarInventario(Guid idInventario, int quantidadeMinima, int quantidadeDisponivel)
        {
            var inventario = _repositoryInventario.ObterPorIdGuid(idInventario);
            inventario.QuantidadeDisponivel = quantidadeDisponivel;
            inventario.EstoqueMinimo = quantidadeMinima;
            await _repositoryInventario.AlterarAsync(inventario);
        }

        void ValidarModificacaoInventario(Inventario entity)
        {
            if (entity.QuantidadeDisponivel < 0 || entity.EstoqueMinimo < 0)
                throw new CrudInventarioException("o estoque minímo e a quantidade disponível não pode ser inferior a 0!");

        }

        public async Task SubtrairOuAdicionarInventario(Guid inventarioID, bool subtrair)
        {
            var inventario = _repositoryInventario.ObterPorIdGuid(inventarioID);
            inventario.QuantidadeDisponivel = subtrair == true ? inventario.QuantidadeDisponivel -= 1 : inventario.QuantidadeDisponivel += 1;
            await _repositoryInventario.AlterarAsync(inventario);
        }
    }
}
