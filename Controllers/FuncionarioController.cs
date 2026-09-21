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

        public IActionResult Index(string pesquisa)
        {
            var listaFuncionarios = _funcionarioRepositorio.ListarTodos();

            if (!string.IsNullOrWhiteSpace(pesquisa))
            {
                listaFuncionarios = listaFuncionarios
                    .Where(f => f.Nome.Contains(pesquisa))
                    .ToList();
            }

            if (listaFuncionarios == null || !listaFuncionarios.Any())
            {
                TempData["MensagemErro"] = "Nenhum funcionário encontrado.";
            }

            return View(listaFuncionarios);
        }


        [HttpGet]
        public IActionResult Editar(int id)
        {
            var funcionario = _funcionarioRepositorio.BuscarPorId(id);
            if (funcionario == null)
            {
                TempData["MensagemErro"] = "Funcionário não encontrado.";
                return RedirectToAction("Index");
            }
            return View(funcionario);
        }


        [HttpPost]
        public IActionResult Editar(FuncionarioModel funcionario)
        {
            try
            {
                if (funcionario == null)
                {
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    return View(funcionario);
                }
                var funcionarioBanco =
                    _funcionarioRepositorio.BuscarPorId(funcionario.Id);
                if (funcionarioBanco == null)
                {
                    TempData["MensagemErro"] =
                        "Funcionário não encontrado.";
                    return RedirectToAction("Index");
                }
                funcionarioBanco.Nome = funcionario.Nome;
                funcionarioBanco.Cpf = funcionario.Cpf;
                funcionarioBanco.Cargo = funcionario.Cargo;
                _funcionarioRepositorio.Editar(funcionarioBanco);
                TempData["MensagemSucesso"] =
                    "Funcionário editado com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] =
                    $"Erro ao editar: {erro.InnerException?.Message ?? erro.Message}";
                return View(funcionario);
            }
        }
        [HttpGet]
        [Authorize(Roles = "Admin,Tecnico")]
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
            var funcionario = _funcionarioService.BuscarDispositivos(id);

            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        public IActionResult BUscarFuncionario(int id)
        {
            var funcionario = _funcionarioRepositorio.BuscarPorId(id);

            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        [HttpGet]
        public IActionResult EditarCargo(int id)
        {
            var funcionario = _funcionarioRepositorio.BuscarPorId(id);

            if (funcionario == null)
            {
                TempData["MensagemErro"] = "Funcionário não encontrado.";
                return RedirectToAction("Index");
            }

            ViewBag.Tipos = Enum.GetValues(typeof(TipoEquipamentoEnum));

            return View(funcionario);
        }

        [HttpPost]
        public IActionResult EditarCargo(FuncionarioModel funcionario)
        {
            try
            {
                if (funcionario == null)
                {
                    return NotFound();
                }

                if (!ModelState.IsValid)
                {
                    ViewBag.Tipos = Enum.GetValues(typeof(TipoEquipamentoEnum));
                    return View(funcionario);
                }

                var funcionarioBanco =
                    _funcionarioRepositorio.BuscarPorId(funcionario.Id);

                if (funcionarioBanco == null)
                {
                    TempData["MensagemErro"] =
                        "Funcionário não encontrado.";

                    return RedirectToAction("Index");
                }

                funcionarioBanco.Cargo = funcionario.Cargo;

                _funcionarioRepositorio.Editar(funcionarioBanco);

                TempData["MensagemSucesso"] =
                    "Cargo do funcionário editado com sucesso!";

                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] =
                    $"Erro ao editar: {erro.InnerException?.Message ?? erro.Message}";

                return View(funcionario);
            }
        }

        // GET
        // Abre a tela de confirmação da exclusão
        [HttpGet]
        public IActionResult Excluir(int id)
        {
            var funcionario = _funcionarioRepositorio.BuscarPorId(id);

            if (funcionario == null)
            {
                TempData["MensagemErro"] =
                    "Funcionário não encontrado.";

                return RedirectToAction("Index");
            }

            return View(funcionario);
        }

        // POST
        // Confirma e realiza a exclusão
        [HttpPost]
        public IActionResult ExcluirConfirmacao(int id)
        {
            try
            {
                var excluido = _funcionarioRepositorio.Excluir(id);

                if (!excluido)
                {
                    TempData["MensagemErro"] =
                        "Não foi possível excluir o funcionário. Verifique se ele possui equipamentos vinculados.";

                    return RedirectToAction("Index");
                }

                TempData["MensagemSucesso"] =
                    "Funcionário excluído com sucesso!";

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] =
                    $"Erro ao excluir funcionário: {ex.InnerException?.Message ?? ex.Message}";

                return RedirectToAction("Index");
            }
        }
    }
}
