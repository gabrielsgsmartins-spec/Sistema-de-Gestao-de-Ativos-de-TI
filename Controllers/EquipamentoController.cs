using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemadeGestãodeAtivosdeTI.Data;
using SistemadeGestãodeAtivosdeTI.Enums;
using SistemadeGestãodeAtivosdeTI.Models;
using SistemadeGestãodeAtivosdeTI.Repositorios.Interfaces;
using SistemadeGestãodeAtivosdeTI.Services;

namespace SistemadeGestãodeAtivosdeTI.Controllers
{
    [Authorize]
    public class EquipamentoController : Controller
    {
        private readonly ApplicationDbContext _bancoContext;
        private readonly EquipamentoService _equipamentoService;
        private readonly IEquipamentoRepositorio _equipamentoRepositorio;

        public EquipamentoController(
            EquipamentoService equipamentoService,
            IEquipamentoRepositorio equipamentoRepositorio,
            ApplicationDbContext bancoContext)
        {
            _equipamentoService = equipamentoService;
            _equipamentoRepositorio = equipamentoRepositorio;
            _bancoContext = bancoContext;
        }

        [HttpGet]
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
        [ValidateAntiForgeryToken]
        public IActionResult Adicionar(EquipamentoModel equipamento)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Tipos = Enum.GetValues(typeof(TipoEquipamentoEnum));

                    return View(equipamento);
                }

                _equipamentoService.Cadastrar(equipamento);

                TempData["MensagemSucesso"] =
                    "Equipamento cadastrado com sucesso!";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] =
                    $"Erro ao cadastrar: {erro.InnerException?.Message ?? erro.Message}";

                ViewBag.Tipos = Enum.GetValues(typeof(TipoEquipamentoEnum));

                return View(equipamento);
            }
        }

        [HttpGet]
        public IActionResult EmManutencao(int id)
        {
            var equipamento = _equipamentoRepositorio.BuscarPorId(id);

            if (equipamento == null)
            {
                TempData["MensagemErro"] =
                    "Equipamento não encontrado.";

                return RedirectToAction(nameof(Index));
            }

            return View(equipamento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EmManutencao(int id, string motivo)
        {
            try
            {
                _equipamentoService.EmManutencao(id, motivo);

                TempData["MensagemSucesso"] =
                    "Equipamento enviado para manutenção com sucesso!";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] =
                    $"Erro ao enviar para manutenção: {erro.InnerException?.Message ?? erro.Message}";

                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult FinalizarManutencao(int id)
        {
            try
            {
                var equipamento = _equipamentoRepositorio.BuscarPorId(id);

                if (equipamento == null)
                {
                    TempData["MensagemErro"] =
                        "Equipamento não encontrado.";

                    return RedirectToAction(nameof(Index));
                }

                _equipamentoService.FinalizarManutencao(id);

                TempData["MensagemSucesso"] =
                    "Equipamento retornado da manutenção e pronto para uso!";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] =
                    $"Erro ao finalizar manutenção: {erro.InnerException?.Message ?? erro.Message}";

                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public IActionResult Atribuir(int id)
        {
            var equipamento = _equipamentoRepositorio.BuscarPorId(id);

            if (equipamento == null)
            {
                TempData["MensagemErro"] =
                    "Equipamento não encontrado.";

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Funcionarios = _bancoContext.Funcionarios.ToList();

            return View(equipamento);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Tecnico")]
        [ValidateAntiForgeryToken]
        public IActionResult Atribuir(
            int equipamentoId,
            int funcionarioId)
        {
            try
            {
                _equipamentoService.Atribuir(
                    equipamentoId,
                    funcionarioId);

                TempData["MensagemSucesso"] =
                    "Equipamento atribuído com sucesso!";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] =
                    $"Erro ao atribuir: {erro.InnerException?.Message ?? erro.Message}";

                return RedirectToAction(
                    nameof(Atribuir),
                    new { id = equipamentoId });
            }
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var equipamento = _equipamentoRepositorio.BuscarPorId(id);

            if (equipamento == null)
            {
                TempData["MensagemErro"] =
                    "Equipamento não encontrado.";

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Tipos =
                Enum.GetValues(typeof(TipoEquipamentoEnum));

            return View(equipamento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(EquipamentoModel equipamento)
        {
            try
            {
                if (equipamento == null)
                {
                    return NotFound();
                }

                if (!ModelState.IsValid)
                {
                    ViewBag.Tipos =
                        Enum.GetValues(typeof(TipoEquipamentoEnum));

                    return View(equipamento);
                }

                var equipamentoBanco =
                    _equipamentoRepositorio.BuscarPorId(equipamento.Id);

                if (equipamentoBanco == null)
                {
                    TempData["MensagemErro"] =
                        "Equipamento não encontrado.";

                    return RedirectToAction(nameof(Index));
                }

                equipamentoBanco.TipoEquipamento =
                    equipamento.TipoEquipamento;

                equipamentoBanco.Marca =
                    equipamento.Marca;

                equipamentoBanco.Modelo =
                    equipamento.Modelo;

                equipamentoBanco.NumeroSerie =
                    equipamento.NumeroSerie;

                equipamentoBanco.DataCompra =
                    equipamento.DataCompra;

                equipamentoBanco.ValorCompra =
                    equipamento.ValorCompra;

                equipamentoBanco.Status =
                    equipamento.Status;

                _equipamentoRepositorio.Editar(equipamentoBanco);

                TempData["MensagemSucesso"] =
                    "Equipamento editado com sucesso!";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] =
                    $"Erro ao editar: {erro.InnerException?.Message ?? erro.Message}";

                ViewBag.Tipos =
                    Enum.GetValues(typeof(TipoEquipamentoEnum));

                return View(equipamento);
            }
        }
    }
}