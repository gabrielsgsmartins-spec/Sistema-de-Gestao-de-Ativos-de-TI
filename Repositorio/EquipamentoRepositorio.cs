using Microsoft.EntityFrameworkCore;
using SistemadeGestãodeAtivosdeTI.Data;
using SistemadeGestãodeAtivosdeTI.Models;
using SistemadeGestãodeAtivosdeTI.Repositorios.Interfaces;

namespace SistemadeGestãodeAtivosdeTI.Repositorios
{
    public class EquipamentoRepositorio : IEquipamentoRepositorio
    {
        private readonly ApplicationDbContext _bancoContext;

        public EquipamentoRepositorio(ApplicationDbContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public List<EquipamentoModel> ListarTodos()
        {
            return _bancoContext.Equipamentos
                .Include(e => e.Funcionario)
                .ToList();
        }

        public EquipamentoModel? BuscarPorId(int id)
        {
            return _bancoContext.Equipamentos.FirstOrDefault(e => e.Id == id);
        }

        public void Adicionar(EquipamentoModel equipamento)
        {
            _bancoContext.Equipamentos.Add(equipamento);
            _bancoContext.SaveChanges();
        }

        public void Editar(EquipamentoModel equipamento)
        {
            _bancoContext.Equipamentos.Update(equipamento);
            _bancoContext.SaveChanges();
        }

        public void Excluir(int id)
        {
            var equipamento = BuscarPorId(id);
            if (equipamento != null)
            {
                _bancoContext.Equipamentos.Remove(equipamento);
                _bancoContext.SaveChanges();
            }
        }

        public EquipamentoModel? BuscarPorNumeroSerie(string numeroSerie)
        {
            return _bancoContext.Equipamentos.FirstOrDefault(e => e.NumeroSerie == numeroSerie);
        }
    }
}