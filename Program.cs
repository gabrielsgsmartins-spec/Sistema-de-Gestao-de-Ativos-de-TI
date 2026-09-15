using Microsoft.EntityFrameworkCore;
using SistemadeGestãodeAtivosdeTI.Repositorios;
using SistemadeGestãodeAtivosdeTI.Repositorios.Interfaces;
using SistemadeGestãodeAtivosdeTI.Services;
using SistemadeGestãodeAtivosdeTI.Data;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("ConexaoPadrao")
    )
);

builder.Services.AddScoped<IEquipamentoRepositorio, EquipamentoRepositorio>();
builder.Services.AddScoped<IFuncionarioRepositorio, FuncionarioRepositorio>();
builder.Services.AddScoped<IManutencaoRepositorio, ManutencaoRepositorio>();

builder.Services.AddScoped<EquipamentoService>();
builder.Services.AddScoped<FuncionarioService>();
builder.Services.AddScoped<ManutencaoService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
)
.WithStaticAssets();

app.Run();