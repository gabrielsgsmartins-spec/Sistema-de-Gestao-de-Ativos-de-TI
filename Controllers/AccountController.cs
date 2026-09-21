using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SistemadeGestãodeAtivosdeTI.Models;
using SistemadeGestãodeAtivosdeTI.ViewModels;

namespace SistemadeGestãodeAtivosdeTI.Controllers
{
    [Authorize] // por padrão
    public class AccountController : Controller
    {
        private readonly UserManager<UsuarioModel> _userManager;
        private readonly SignInManager<UsuarioModel> _signInManager;

     
        public AccountController(
            UserManager<UsuarioModel> userManager,
            SignInManager<UsuarioModel> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

//LOGINN
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl = null)
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("Index", "Home");

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (!ModelState.IsValid)
                return View(model);

            // eu uso o e-mail como UserName
            var resultado = await _signInManager.PasswordSignInAsync(
                userName: model.Email,
                password: model.Senha,
                isPersistent: model.LembrarMe,
                lockoutOnFailure: true);

            if (resultado.Succeeded)
            {
                
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);

                return RedirectToAction("Index", "Home");
            }

            if (resultado.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty,
                    "Conta bloqueada temporariamente por excesso de tentativas. Tente em 5 minutos.");
                return View(model);
            }

            
            ModelState.AddModelError(string.Empty, "E-mail ou senha inválidos.");
            return View(model);
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registrar(RegistroViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var usuario = new UsuarioModel
            {
                UserName = model.Email,  
                Email = model.Email,
                NomeCompleto = model.NomeCompleto,
                DataCadastro = DateTime.Now
            };

           
            var resultado = await _userManager.CreateAsync(usuario, model.Senha);

            if (resultado.Succeeded)
            {
                // Todo novo usuário entra comoUsuario
                await _userManager.AddToRoleAsync(usuario, "Usuario");

                await _signInManager.SignInAsync(usuario, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }

            foreach (var erro in resultado.Errors)
                ModelState.AddModelError(string.Empty, erro.Description);

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        // ---------------- ACESSO NEGADO ----------------

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AcessoNegado()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AlterarSenha()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AlterarSenha(AlterarSenhaViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var usuario = await _userManager.GetUserAsync(User);
            if (usuario == null)
                return RedirectToAction("Login");

            var resultado = await _userManager.ChangePasswordAsync(
                usuario, model.SenhaAtual, model.NovaSenha);

            if (!resultado.Succeeded)
            {
                foreach (var erro in resultado.Errors)
                    ModelState.AddModelError(string.Empty, erro.Description);
                return View(model);
            }

            // Atualiza o cookie para a sessão não cair.
            await _signInManager.RefreshSignInAsync(usuario);

            TempData["Mensagem"] = "Senha alterada com sucesso.";
            return RedirectToAction("Index", "Home");
        }
    }
}