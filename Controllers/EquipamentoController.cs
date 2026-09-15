using Microsoft.AspNetCore.Mvc;
using SistemadeGestãodeAtivosdeTI.Data;
using SistemadeGestãodeAtivosdeTI.Enums;
using SistemadeGestãodeAtivosdeTI.Models;
using SistemadeGestãodeAtivosdeTI.Repositorios.Interfaces;
using SistemadeGestãodeAtivosdeTI.Services;

namespace SistemadeGestãodeAtivosdeTI.Controllers
{
    public class EquipamentoController : Controller
    {
        private readonly ApplicationDbContext _bancoContext;
        private readonly EquipamentoService _equipamentoService;
        private readonly IEquipamentoRepositorio _equipamentoRepositorio;

        public EquipamentoController(EquipamentoService equipamentoService, IEquipamentoRepositorio equipamentoRepositorio, ApplicationDbContext bancoContext)
        {
            _equipamentoService = equipamentoService;
            _equipamentoRepositorio = equipamentoRepositorio;
            _bancoContext = bancoContext;
        }

        public IActionResult Index()
        {
            var listaEquipamentos = _equipamentoRepositorio.ListarTodos();
            return View(listaEquipamentos);
        }

        [HttpGet]
        public IActionResult Adicionar()
        {
            ViewBag.Tipos = Enum.GetValues(typeof(TipoEquipamentoEnum));
            return View();
        }

        [HttpPost]
        public IActionResult Adicionar(EquipamentoModel equipamento)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _equipamentoService.Cadastrar(equipamento);
                    TempData["MensagemSucesso"] = "Equipamento cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }

                ViewBag.Tipos = Enum.GetValues(typeof(TipoEquipamentoEnum));
                return View("Adicionar", equipamento);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Erro ao cadastrar: {erro.InnerException?.Message ?? erro.Message}";
                return View(equipamento);
            }
        }

        [HttpGet]
        public IActionResult Atribuir(int id)
        {
            var equipamento = _equipamentoRepositorio.BuscarPorId(id);
            if (equipamento == null)
            {
                TempData["MensagemErro"] = "Equipamento não encontrado.";
                return RedirectToAction("Index");
            }

            ViewBag.Funcionarios = _bancoContext.Funcionarios.ToList();
            return View(equipamento);
        }

        [HttpPost]
        public IActionResult Atribuir(int equipamentoId, int funcionarioId)
        {
            try
            {
                _equipamentoService.Atribuir(equipamentoId, funcionarioId);
                TempData["MensagemSucesso"] = "Equipamento atribuído com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Erro ao atribuir: {erro.InnerException?.Message ?? erro.Message}";
                return RedirectToAction("Atribuir", new { id = equipamentoId });
            }
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var equipamento = _equipamentoRepositorio.BuscarPorId(id);
            if (equipamento == null)
            {
                TempData["MensagemErro"] = "Equipamento não encontrado.";
                return RedirectToAction("Index");
            }

            ViewBag.Tipos = Enum.GetValues(typeof(TipoEquipamentoEnum));
            return View(equipamento);
        }

        [HttpPost]
        public IActionResult Editar(EquipamentoModel equipamento)
        {
            try
            {
                if (equipamento == null)
                {
                    return NotFound();
                }

                _equipamentoService.Atualizar(equipamento);
                TempData["MensagemSucesso"] = "Equipamento editado com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Erro ao editar: {erro.InnerException?.Message ?? erro.Message}";
                return View(equipamento);
            }
        }
    }
}