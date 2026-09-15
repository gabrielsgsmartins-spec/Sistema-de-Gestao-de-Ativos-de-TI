using SistemadeGestãodeAtivosdeTI.Enums;

namespace SistemadeGestãodeAtivosdeTI.Models
{
    public class ManutencaoModel
    {
        public int Id { get; set; }

        public int EquipamentoId { get; set; }

        public EquipamentoModel? Equipamento { get; set; }

        public string? DescricaoProblema { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public string? Observacao { get; set; }

        public bool Concluida { get; set; }
    }
}