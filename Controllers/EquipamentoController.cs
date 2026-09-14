using Microsoft.AspNetCore.Mvc;
using SistemadeGestãodeAtivosdeTI.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using SistemadeGestãodeAtivosdeTI.Enums;
using SistemadeGestãodeAtivosdeTI.Models;
using SistemaGestaoAtivos.Models;

namespace SistemadeGestãodeAtivosdeTI.Controllers
{
    public class EquipamentoController : Controller
    {
        private readonly DbContext _bancoContext;
        private readonly EquipamentoService _equipamentoService;
        private readonly IEquipamentoRepositorio _equipamentoRepositorio;
        public EquipamentoController(EquipamentoService equipamentoService, IEquipamentoRepositorio equipamentoRepositorio, DbContext bancoContext)
        {
            _equipamentoService = equipamentoService;
            _equipamentoRepositorio = equipamentoRepositorio;
            _bancoContext = bancoContext;
        }


        public IActionResult Index()
        {
            // Busca todos os equipamentos cadastrados
            var listaEquipamentos = _equipamentoRepositorio.ListarTodos();

            // Passa a lista para a View
            return View(listaEquipamentos);
        }


        [HttpGet]
        public IActionResult Adicionar()
        {
            ViewBag.Tipos = _equipamentoRepositorio.ListarTodos();

            return View();
        }


        [HttpPost]
        public IActionResult Adicionar(EquipamentoModel equipamento)
        {
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        _equipamentoService.Cadastrar(equipamento);
                        TempData["MensagemSucesso"] = "Equipamento cadastrado com sucesso!";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Tipos = _equipamentoRepositorio.ListarTodos();
                        return View(equipamento);
                    }

                }
                catch (Exception erro)
                {
                    TempData["MensagemErro"] =
                        $"Erro ao cadastrar: {erro.InnerException?.Message ?? erro.Message}";

                    return View(equipamento);
                }
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
            ViewBag.Funcionarios = _bancoContext.Set<FuncionarioModel>().ToList();
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
                TempData["MensagemErro"] =
                    $"Erro ao atribuir: {erro.InnerException?.Message ?? erro.Message}";
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
            ViewBag.Tipos = _equipamentoRepositorio.ListarTodos();
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
                else
                {
                    _equipamentoService.Atualizar(equipamento);
                    TempData["MensagemSucesso"] = "Equipamento editado com sucesso!";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] =
                    $"Erro ao editar: {erro.InnerException?.Message ?? erro.Message}";

                return View(equipamento);
            }
        }
    }
}