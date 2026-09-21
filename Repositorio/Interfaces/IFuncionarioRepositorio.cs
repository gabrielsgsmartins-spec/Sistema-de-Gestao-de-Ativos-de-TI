using SistemadeGestãodeAtivosdeTI.Models;
using System.Collections.Generic;

namespace SistemadeGestãodeAtivosdeTI.Repositorios.Interfaces
{
    public interface IFuncionarioRepositorio
    {
        List<FuncionarioModel> ListarTodos();
        FuncionarioModel? BuscarDispositivos(int funcionarioId);
        FuncionarioModel? BuscarPorId(int id);
        void Adicionar(FuncionarioModel funcionario);
        void Editar(FuncionarioModel funcionario);
        bool Excluir(int id);

        FuncionarioModel? BuscarPorCpf(string cpf);


    }
}