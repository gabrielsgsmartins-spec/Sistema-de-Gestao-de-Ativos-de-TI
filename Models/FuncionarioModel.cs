using SistemadeGestãodeAtivosdeTI.Enums;
using SistemaGestaoAtivos.Models;
using System.ComponentModel.DataAnnotations;

namespace SistemadeGestãodeAtivosdeTI.Models
{
    public class FuncionarioModel
    {
        // Chave primária
        public int Id { get; set; }


        // Dados do funcionário
        [Required(ErrorMessage = "Informe o nome do funcionário.")]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o cargo do funcionário.")]
        [StringLength(100)]
        public string Cargo { get; set; }


        // Departamento
        public DepartamentoEnum Departamento { get; set; }


        // Equipamentos utilizados pelo funcionário
        public List<EquipamentoModel> Equipamentos { get; set; }
            = new List<EquipamentoModel>();


        // Data de cadastro
        public DateTime DataCadastro { get; set; } = DateTime.Now;
    }
}