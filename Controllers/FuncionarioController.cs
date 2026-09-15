using Microsoft.AspNetCore.Mvc;
using SistemadeGestãodeAtivosdeTI.Data;
using SistemadeGestãodeAtivosdeTI.Models;
using SistemadeGestãodeAtivosdeTI.Repositorios.Interfaces;
using SistemadeGestãodeAtivosdeTI.Services;

namespace SistemadeGestãodeAtivosdeTI.Controllers
{
    public class FuncionarioController : Controller
    {
        private readonly ApplicationDbContext _bancoContext;
        private readonly FuncionarioService _funcionarioService;
        private readonly IFuncionarioRepositorio _funcionarioRepositorio;

        public FuncionarioController(
            FuncionarioService funcionarioService,
            IFuncionarioRepositorio funcionarioRepositorio,
            ApplicationDbContext bancoContext)
        {
            _funcionarioService = funcionarioService;
            _funcionarioRepositorio = funcionarioRepositorio;
            _bancoContext = bancoContext;
        }

        public IActionResult Index()
        {
            var listaFuncionarios = _funcionarioRepositorio.ListarTodos();

            if (listaFuncionarios == null || !listaFuncionarios.Any())
            {
                TempData["MensagemErro"] = "Nenhum funcionário encontrado.";
            }

            return View(listaFuncionarios);
        }

        [HttpGet]
        public IActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Adicionar(FuncionarioModel funcionario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _funcionarioService.Cadastrar(funcionario);

                    TempData["MensagemSucesso"] =
                        "Funcionário cadastrado com sucesso!";

                    return RedirectToAction("Index");
                }

                return View(funcionario);
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;

                return View(funcionario);
            }
        }

        [HttpGet]
        public IActionResult Dispositivos(int id)
        {
            var funcionario = _funcionarioRepositorio.BuscarPorId(id);

            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }
    }
}