using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaGestaoAtivos.Models;


namespace SistemadeGestãodeAtivosdeTI.Repositorios.Interfaces
{
    public interface IEquipamentoRepositorio
    {
        List<EquipamentoModel> ListarTodos();
        EquipamentoModel? BuscarPorId(int id);
        void Adicionar(EquipamentoModel equipamento);
        void Editar(EquipamentoModel equipamento);
        void Excluir(int id);
        EquipamentoModel? BuscarPorNumeroSerie(string numeroSerie);

    }
}