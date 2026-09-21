using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SistemadeGestãodeAtivosdeTI.Data;
using SistemadeGestãodeAtivosdeTI.Models;
using SistemadeGestãodeAtivosdeTI.Repositorios;
using SistemadeGestãodeAtivosdeTI.Repositorios.Interfaces;
using SistemadeGestãodeAtivosdeTI.Services;

var builder = WebApplication.CreateBuilder(args);

// Banco de dados
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("ConexaoPadrao")));

// Identity
builder.Services
    .AddIdentity<UsuarioModel, IdentityRole>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Configuração do Login
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AcessoNegado";
});

// MVC
builder.Services.AddControllersWithViews();

// Repositórios
builder.Services.AddScoped<IFuncionarioRepositorio, FuncionarioRepositorio>();
builder.Services.AddScoped<IEquipamentoRepositorio, EquipamentoRepositorio>();
builder.Services.AddScoped<IManutencaoRepositorio, ManutencaoRepositorio>();

// Serviços
builder.Services.AddScoped<FuncionarioService>();
builder.Services.AddScoped<EquipamentoService>();

var app = builder.Build();

// Tratamento de erros
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();  

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

// Primeira tela = Login
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}")
    .WithStaticAssets();

app.Run();