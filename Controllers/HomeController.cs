using Microsoft.AspNetCore.Mvc;
using SistemadeGestãodeAtivosdeTI.Data;
using SistemadeGestãodeAtivosdeTI.Enums;

namespace SistemadeGestãodeAtivosdeTI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _bancoContext;

        public HomeController(ApplicationDbContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public IActionResult Index()
        {
            var equipamentos = _bancoContext.Equipamentos.ToList();

            ViewBag.TotalProdutos = equipamentos.Count;

            if (equipamentos.Count == 0)
            {
                ViewBag.Mensagem = "Nenhum equipamento cadastrado.";
            }

            if (equipamentos.Any(x => x.Status == StatusEquipamentoEnum.EmManutencao))
            {
                ViewBag.Mensagem = "Existem equipamentos em manutenção.";
            }

            return View(equipamentos);
        }
    }
}