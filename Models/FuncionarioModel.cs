using SistemadeGestãodeAtivosdeTI.Enums;
using System.ComponentModel.DataAnnotations;

namespace SistemadeGestãodeAtivosdeTI.Models
{
    public class FuncionarioModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o nome do funcionário.")]
        [StringLength(100)]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "Informe o cargo do funcionário.")]
        [StringLength(100)]
        public string? Cargo { get; set; }

        [Required(ErrorMessage = "Informe o CPF do funcionário.")]
    
        public string? Cpf { get; set; }

        public DepartamentoEnum Departamento { get; set; }

        public List<EquipamentoModel> Equipamentos { get; set; } = new List<EquipamentoModel>();

        public DateTime DataCadastro { get; set; } = DateTime.Now;
    }
}