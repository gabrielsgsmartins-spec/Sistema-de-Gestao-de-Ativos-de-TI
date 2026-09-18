using SistemadeGestãodeAtivosdeTI.Models;
using SistemadeGestãodeAtivosdeTI.Repositorios.Interfaces;

namespace SistemadeGestãodeAtivosdeTI.Services
{
    public class FuncionarioService
    {
        private readonly IFuncionarioRepositorio _funcionarioRepositorio;

        public FuncionarioService(IFuncionarioRepositorio funcionarioRepositorio)
        {
            _funcionarioRepositorio = funcionarioRepositorio;
        }

        public void Cadastrar(FuncionarioModel funcionario)
        {
            var funcionarioExistente = _funcionarioRepositorio.BuscarPorCpf(funcionario.Cpf);

            if (funcionarioExistente != null)
            {
                throw new Exception("Já existe um funcionário com esse CPF.");
            }

            if (string.IsNullOrWhiteSpace(funcionario.Nome))
            {
                throw new Exception("Nome do funcionário é obrigatório.");
            }

            _funcionarioRepositorio.Adicionar(funcionario);
        }

        public void Editar(FuncionarioModel funcionario)
        {
            var funcionarioExistente = _funcionarioRepositorio.BuscarPorId(funcionario.Id);

            if (funcionarioExistente == null)
            {
                throw new Exception("Funcionário não encontrado.");
            }

            var funcionarioComMesmoCpf = _funcionarioRepositorio.BuscarPorCpf(funcionario.Cpf);

            if (funcionarioComMesmoCpf != null && funcionarioComMesmoCpf.Id != funcionario.Id)
            {
                throw new Exception("Já existe outro funcionário com esse CPF.");
            }

            _funcionarioRepositorio.Editar(funcionario);
        }

        public void Excluir(int id)
        {
            var funcionarioExistente = _funcionarioRepositorio.BuscarPorId(id);

            if (funcionarioExistente == null)
            {
                throw new Exception("Funcionário não encontrado.");
            }

            if (funcionarioExistente.Equipamentos.Count > 0)
            {
                throw new Exception("Não é possível excluir um funcionário que possui equipamentos cadastrados.");
            }

            _funcionarioRepositorio.Excluir(id);
        }

        public List<FuncionarioModel> ListarTodos()
        {
            return _funcionarioRepositorio.ListarTodos();
        }

        public FuncionarioModel PodeReceberEquipamento(int funcionarioId)
        {
            var funcionario = _funcionarioRepositorio.BuscarPorId(funcionarioId);

            if (funcionario == null)
            {
                throw new Exception("Funcionário não encontrado.");
            }

            if (funcionario.Equipamentos.Count >= 3)
            {
                throw new Exception("O funcionário já possui 3 equipamentos cadastrados");
            }

            return funcionario;
        }

        public FuncionarioModel? BuscarDispositivos(int id)
        {
            return _funcionarioRepositorio.BuscarDispositivos(id);
        }
    }
}
