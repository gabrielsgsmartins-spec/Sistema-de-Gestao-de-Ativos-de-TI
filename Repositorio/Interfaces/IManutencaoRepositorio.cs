using SistemadeGestãodeAtivosdeTI.Models;

namespace SistemadeGestãodeAtivosdeTI.Repositorios.Interfaces
{
    public interface IManutencaoRepositorio
    {
        List<ManutencaoModel> ListarTodos();
        ManutencaoModel BuscarPorId(int id);
        void Adicionar(ManutencaoModel manutencao);
        void Editar(ManutencaoModel manutencao);
        void Excluir(ManutencaoModel manutencao);
    }
}