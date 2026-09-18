using Microsoft.AspNetCore.Mvc;
using SistemadeGestãodeAtivosdeTI.Data;
using SistemadeGestãodeAtivosdeTI.Enums;
using SistemadeGestãodeAtivosdeTI.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SistemadeGestãodeAtivosdeTI.Data;
using SistemadeGestãodeAtivosdeTI.Models;
using SistemadeGestãodeAtivosdeTI.Repositorios.Interfaces;

namespace SistemadeGestãodeAtivosdeTI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _bancoContext;
        private readonly IFuncionarioRepositorio _funcionarioRepositorio;

        public HomeController(ApplicationDbContext bancoContext, IFuncionarioRepositorio funcionarioRepositorio)
        {
            _bancoContext = bancoContext;
            _funcionarioRepositorio = funcionarioRepositorio;
        }

        public IActionResult Index()
        {
            var equipamentos = _bancoContext.Equipamentos.ToList();

            var funcionarios = _funcionarioRepositorio.ListarTodos();

            ViewBag.TotalEquipamentos = equipamentos.Count;

            ViewBag.TotalFuncionarios = funcionarios.Count;

            ViewBag.Disponiveis = equipamentos.Count(x =>
                x.Status == StatusEquipamentoEnum.Disponível);

            ViewBag.EmUso = equipamentos.Count(x =>
                x.Status == StatusEquipamentoEnum.EmUso);

            ViewBag.EmManutencao = equipamentos.Count(x =>
                x.Status == StatusEquipamentoEnum.EmManutencao);

            ViewBag.Funcionarios = funcionarios;

            return View(equipamentos);
        }
    }
}