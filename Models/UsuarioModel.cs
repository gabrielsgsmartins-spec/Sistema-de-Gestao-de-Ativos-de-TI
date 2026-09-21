using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SistemadeGestãodeAtivosdeTI.Models
{
    // Herda de IdentityUser: ganha Id, UserName, Email, PasswordHash, etc. de graça.
    public class UsuarioModel : IdentityUser
    {
        [Required(ErrorMessage = "Informe o nome completo.")]
        [StringLength(100)]
        public string? NomeCompleto { get; set; }

        public DateTime DataCadastro { get; set; } = DateTime.Now;

        // Opcional: liga o login a um funcionário já cadastrado no sistema.
        // Deixe assim por enquanto; a Etapa 12 explica como usar.
        public int? FuncionarioId { get; set; }
        public FuncionarioModel? Funcionario { get; set; }
    }
}