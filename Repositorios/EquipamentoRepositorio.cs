using Microsoft.EntityFrameworkCore;
using SistemadeGestãodeAtivosdeTI.Models;
using SistemadeGestãodeAtivosdeTI.Repositorios.Interfaces;
using SistemaGestaoAtivos.Models;


namespace SistemadeGestãodeAtivosdeTI.Repositorios
{
    public class EquipamentoRepositorio : IEquipamentoRepositorio
    {
        private readonly DbContext _bancoContext;

        public EquipamentoRepositorio(DbContext bancoContext)
        {
            _bancoContext = bancoContext;
        }


        public List<EquipamentoModel> ListarTodos()
        {
            return _bancoContext.Set<EquipamentoModel>()
                .Include(e => e.Marca)
                .ToList();
        }

        public EquipamentoModel? BuscarPorId(int id)
        {
            return _bancoContext.Set<EquipamentoModel>().FirstOrDefault(e => e.Id == id);
        }

        public void Adicionar(EquipamentoModel equipamento)
        {
            _bancoContext.Set<EquipamentoModel>().Add(equipamento);
            _bancoContext.SaveChanges();
        }

        public void Editar(EquipamentoModel equipamento)
        {
            _bancoContext.Set<EquipamentoModel>().Update(equipamento);
            _bancoContext.SaveChanges();
        }

        public void Excluir(int id)
        {
            var equipamento = BuscarPorId(id);
            if (equipamento != null)
            {
                _bancoContext.Set<EquipamentoModel>().Remove(equipamento);
                _bancoContext.SaveChanges();
            }
        }

        public EquipamentoModel? BuscarPorNumeroSerie(string numeroSerie)
        {
            return _bancoContext.Set<EquipamentoModel>().FirstOrDefault(e => e.NumeroSerie == numeroSerie);
        }
    }
}
