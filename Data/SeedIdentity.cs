using Microsoft.AspNetCore.Identity;
using SistemadeGestãodeAtivosdeTI.Models;

namespace SistemadeGestãodeAtivosdeTI.Data
{
    public static class SeedIdentity
    {
        public const string RoleAdmin = "Admin";
        public const string RoleTecnico = "Tecnico";
        public const string RoleUsuario = "Usuario";

        public static async Task ExecutarAsync(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = services.GetRequiredService<UserManager<UsuarioModel>>();

            // 1. Cria os papéis, se ainda não existirem.
            foreach (var role in new[] { RoleAdmin, RoleTecnico, RoleUsuario })
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            //  vou criar um administrador .
            const string emailAdmin = "admin@sistema.com";
            const string senhaAdmin = "Admin@123";

            var admin = await userManager.FindByEmailAsync(emailAdmin);
            if (admin == null)
            {
                admin = new UsuarioModel
                {
                    UserName = emailAdmin,
                    Email = emailAdmin,
                    NomeCompleto = "Administrador do Sistema",
                    EmailConfirmed = true,
                    DataCadastro = DateTime.Now
                };

                var resultado = await userManager.CreateAsync(admin, senhaAdmin);

                if (resultado.Succeeded)
                    await userManager.AddToRoleAsync(admin, RoleAdmin);
            }
        }
    }
}