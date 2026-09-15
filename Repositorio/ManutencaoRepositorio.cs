using SistemadeGestãodeAtivosdeTI.Data;
using SistemadeGestãodeAtivosdeTI.Models;
using SistemadeGestãodeAtivosdeTI.Repositorios.Interfaces;

namespace SistemadeGestãodeAtivosdeTI.Repositorios
{
    public class ManutencaoRepositorio : IManutencaoRepositorio
    {
        private readonly ApplicationDbContext _context;

        public ManutencaoRepositorio(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<ManutencaoModel> ListarTodos()
        {
            return _context.Manutencoes.ToList();
        }

        public ManutencaoModel BuscarPorId(int id)
        {
            return _context.Manutencoes.FirstOrDefault(x => x.Id == id);
        }

        public void Adicionar(ManutencaoModel manutencao)
        {
            _context.Manutencoes.Add(manutencao);
            _context.SaveChanges();
        }

        public void Editar(ManutencaoModel manutencao)
        {
            _context.Manutencoes.Update(manutencao);
            _context.SaveChanges();
        }

        public void Excluir(ManutencaoModel manutencao)
        {
            _context.Manutencoes.Remove(manutencao);
            _context.SaveChanges();
        }
    }
}