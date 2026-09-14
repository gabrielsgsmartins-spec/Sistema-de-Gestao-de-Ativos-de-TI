using Microsoft.EntityFrameworkCore;
using SistemadeGestãodeAtivosdeTI.Models;
using SistemadeGestãodeAtivosdeTI.Repositorios.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SistemadeGestãodeAtivosdeTI.Repositorios
{
    public class FuncionarioRepositorio : IFuncionarioRepositorio
    {
        private readonly DbContext _bancoContext;

        public FuncionarioRepositorio(DbContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public List<FuncionarioModel> ListarTodos()
        {
            return _bancoContext.Set<FuncionarioModel>().ToList();
        }

        public FuncionarioModel? BuscarPorId(int id)
        {
            return _bancoContext.Set<FuncionarioModel>().FirstOrDefault(f => f.Id == id);
        }

        public void Adicionar(FuncionarioModel funcionario)
        {
            _bancoContext.Set<FuncionarioModel>().Add(funcionario);
            _bancoContext.SaveChanges();
        }

        public void Editar(FuncionarioModel funcionario)
        {
            _bancoContext.Set<FuncionarioModel>().Update(funcionario);
            _bancoContext.SaveChanges();
        }

        public void Excluir(int id)
        {
            var funcionario = BuscarPorId(id);
            if (funcionario != null)
            {
                _bancoContext.Set<FuncionarioModel>().Remove(funcionario);
                _bancoContext.SaveChanges();
            }
        }
    }
}