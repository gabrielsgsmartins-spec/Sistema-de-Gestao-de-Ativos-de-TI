using SistemadeGestãodeAtivosdeTI.Models;
using SistemadeGestãodeAtivosdeTI.Repositorios.Interfaces;

namespace SistemadeGestãodeAtivosdeTI.Services
{
    public class ManutencaoService
    {
        private readonly IManutencaoRepositorio _manutencaoRepositorio;

        public ManutencaoService(IManutencaoRepositorio manutencaoRepositorio)
        {
            _manutencaoRepositorio = manutencaoRepositorio;
        }

        public void Cadastrar(ManutencaoModel manutencao, int equipamentoId)
        {
            manutencao.EquipamentoId = equipamentoId;

            if (manutencao.EquipamentoId <= 0)
            {
                throw new Exception("Equipamento inválido.");
            }

            if (string.IsNullOrWhiteSpace(manutencao.DescricaoProblema))
            {
                throw new Exception("Informe o problema do equipamento.");
            }

            if (manutencao.DataInicio > DateTime.Now)
            {
                throw new Exception("A data de início não pode ser futura.");
            }

            manutencao.Concluida = false;

            _manutencaoRepositorio.Adicionar(manutencao, equipamentoId);
        }

        public void Finalizar(int id)
        {
            var manutencao = _manutencaoRepositorio.BuscarPorId(id);

            if (manutencao == null)
            {
                throw new Exception("Manutenção não encontrada.");
            }

            if (manutencao.Concluida)
            {
                throw new Exception("Essa manutenção já foi finalizada.");
            }

            manutencao.DataFim = DateTime.Now;
            manutencao.Concluida = true;

            _manutencaoRepositorio.Editar(manutencao);
        }

        public void Excluir(int id)
        {
            var manutencao = _manutencaoRepositorio.BuscarPorId(id);

            if (manutencao == null)
            {
                throw new Exception("Manutenção não encontrada.");
            }

            if (!manutencao.Concluida)
            {
                throw new Exception("Não é possível excluir uma manutenção em andamento.");
            }

            _manutencaoRepositorio.Excluir(manutencao);
        }

        public ManutencaoModel BuscarPorId(int id)
        {
            return _manutencaoRepositorio.BuscarPorId(id);
        }

        public List<ManutencaoModel> ListarTodos()
        {
            return _manutencaoRepositorio.ListarTodos();
        }
    }
}