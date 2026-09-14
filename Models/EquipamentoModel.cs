using SistemadeGestãodeAtivosdeTI.Enums;
using SistemadeGestãodeAtivosdeTI.Models;
using System.ComponentModel.DataAnnotations;

namespace SistemaGestaoAtivos.Models
{
    public class EquipamentoModel
    {
        // Chave primária
        public int Id { get; set; }


        // Tipo do equipamento
        public TipoEquipamentoEnum TipoEquipamento { get; set; }


        // Informações do equipamento
        [Required(ErrorMessage = "Informe a marca.")]
        [StringLength(100)]
        public string Marca { get; set; }

        [Required(ErrorMessage = "Informe o modelo.")]
        [StringLength(100)]
        public string Modelo { get; set; }

        [Required(ErrorMessage = "Informe o número de série.")]
        [StringLength(100)]
        public string NumeroSerie { get; set; }


        // Informações da compra
        [Required(ErrorMessage = "Informe a data de compra.")]
        public DateTime DataCompra { get; set; }

        [Range(0.01, double.MaxValue,
            ErrorMessage = "O valor deve ser maior que zero.")]
        public decimal ValorCompra { get; set; }


        // Status do equipamento
        public StatusEquipamentoEnum Status { get; set; }


        // Funcionário que está utilizando o equipamento
        public int? FuncionarioId { get; set; }

        public FuncionarioModel? Funcionario { get; set; }


        // Data de cadastro no sistema
        public DateTime DataCadastro { get; set; } = DateTime.Now;
    }
}