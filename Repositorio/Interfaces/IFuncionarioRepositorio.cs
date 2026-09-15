using SistemadeGestãodeAtivosdeTI.Models;
using System.Collections.Generic;

namespace SistemadeGestãodeAtivosdeTI.Repositorios.Interfaces
{
    public interface IFuncionarioRepositorio
    {
        List<FuncionarioModel> ListarTodos();
        FuncionarioModel? BuscarPorId(int id);
        void Adicionar(FuncionarioModel funcionario);
        void Editar(FuncionarioModel funcionario);
        void Excluir(int id);

        FuncionarioModel? BuscarPorCpf(string cpf);

    }
}