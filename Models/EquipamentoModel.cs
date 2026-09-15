using SistemadeGestãodeAtivosdeTI.Enums;
using System.ComponentModel.DataAnnotations;

namespace SistemadeGestãodeAtivosdeTI.Models
{
    public class EquipamentoModel
    {
        public int Id { get; set; }

        public TipoEquipamentoEnum TipoEquipamento { get; set; }

        [Required(ErrorMessage = "Informe a marca.")]
        [StringLength(100)]
        public string? Marca { get; set; }

        [Required(ErrorMessage = "Informe o modelo.")]
        [StringLength(100)]
        public string? Modelo { get; set; }

        [Required(ErrorMessage = "Informe o número de série.")]
        [StringLength(100)]
        public string? NumeroSerie { get; set; }

        [Required(ErrorMessage = "Informe a data de compra.")]
        public DateTime DataCompra { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero.")]
        public decimal ValorCompra { get; set; }

        public StatusEquipamentoEnum Status { get; set; }

        public int? FuncionarioId { get; set; }

        public FuncionarioModel? Funcionario { get; set; }

        public DateTime DataCadastro { get; set; } = DateTime.Now;
    }
}