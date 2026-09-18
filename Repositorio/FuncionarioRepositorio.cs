using Microsoft.EntityFrameworkCore;
using SistemadeGestãodeAtivosdeTI.Data;
using SistemadeGestãodeAtivosdeTI.Models;
using SistemadeGestãodeAtivosdeTI.Repositorios.Interfaces;

namespace SistemadeGestãodeAtivosdeTI.Repositorios
{
    public class FuncionarioRepositorio : IFuncionarioRepositorio
    {
        private readonly ApplicationDbContext _bancoContext;

        public FuncionarioRepositorio(ApplicationDbContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public List<FuncionarioModel> ListarTodos()
        {
            return _bancoContext.Funcionarios
                .Include(f => f.Equipamentos)
                .ToList();
        }
        public FuncionarioModel? BuscarPorId(int id)
        {
            return _bancoContext.Funcionarios.FirstOrDefault(f => f.Id == id);
        }

        public void Adicionar(FuncionarioModel funcionario)
        {
            _bancoContext.Funcionarios.Add(funcionario);
            _bancoContext.SaveChanges();
        }

        public void Editar(FuncionarioModel funcionario)
        {
            _bancoContext.Funcionarios.Update(funcionario);
            _bancoContext.SaveChanges();
        }

        public void Excluir(int id)
        {
            var funcionario = BuscarPorId(id);
            if (funcionario != null)
            {
                _bancoContext.Funcionarios.Remove(funcionario);
                _bancoContext.SaveChanges();
            }
        }

        public FuncionarioModel? BuscarPorCpf(string cpf)
        {
            return _bancoContext.Funcionarios.FirstOrDefault(f => f.Cpf == cpf);
        }

        public FuncionarioModel? BuscarDispositivos(int id)
        {
            return _bancoContext.Funcionarios
                .Include(f => f.Equipamentos)
                .FirstOrDefault(f => f.Id == id);
        }
    }
}