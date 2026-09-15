using Microsoft.EntityFrameworkCore;
using SistemadeGestãodeAtivosdeTI.Models;

namespace SistemadeGestãodeAtivosdeTI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<EquipamentoModel> Equipamentos { get; set; }

        public DbSet<FuncionarioModel> Funcionarios { get; set; }

        public DbSet<ManutencaoModel> Manutencoes { get; set; }
    }
}