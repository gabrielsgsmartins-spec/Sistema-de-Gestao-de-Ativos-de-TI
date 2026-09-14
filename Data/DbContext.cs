using Microsoft.EntityFrameworkCore;
using SistemadeGestãodeAtivosdeTI.Models;
using SistemaGestaoAtivos.Models;

namespace SistemaGestaoAtivos.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<EquipamentoModel> Equipamentos { get; set; }

        public DbSet<FuncionarioModel> Funcionarios { get; set; }
    }
}