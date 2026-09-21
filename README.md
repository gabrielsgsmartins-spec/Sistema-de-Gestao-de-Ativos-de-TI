# 🖥️ Sistema de Gestão de Ativos de TI

Sistema web desenvolvido em **ASP.NET Core MVC** para gerenciamento de ativos de Tecnologia da Informação, funcionários, equipamentos e manutenções.

O projeto foi desenvolvido com foco em **organização de código, arquitetura em camadas, autenticação e autorização utilizando ASP.NET Core Identity e persistência de dados com Entity Framework Core**.

---

## 🚀 Sobre o projeto

O **Sistema de Gestão de Ativos de TI** permite centralizar o controle dos equipamentos de uma organização, possibilitando acompanhar:

* 👨‍💼 Funcionários
* 💻 Equipamentos
* 🔧 Manutenções
* 🔄 Atribuição de equipamentos
* 📦 Status dos ativos
* 🔐 Usuários e autenticação
* 👥 Perfis de acesso

O sistema também possui regras de negócio para impedir operações inválidas, como atribuir um equipamento que já está em uso ou excluir equipamentos que estão em manutenção.

---

## 🛠️ Tecnologias utilizadas

* **C#**
* **.NET 10**
* **ASP.NET Core MVC**
* **ASP.NET Core Identity**
* **Entity Framework Core**
* **SQL Server**
* **Bootstrap**
* **Razor Views**
* **LINQ**
* **Repository Pattern**
* **Service Layer**
* **Dependency Injection**

---

## 🏗️ Arquitetura

O projeto utiliza uma organização baseada em **arquitetura em camadas**, separando responsabilidades entre Controllers, Services, Repositories, Models e acesso aos dados.

```text
SistemaDeGestaoDeAtivosDeTI
│
├── Controllers
│   ├── AccountController
│   ├── EquipamentoController
│   ├── FuncionarioController
│   └── HomeController
│
├── Services
│   ├── EquipamentoService
│   ├── FuncionarioService
│   └── ManutencaoService
│
├── Repositorio
│   ├── EquipamentoRepositorio
│   ├── FuncionarioRepositorio
│   └── ManutencaoRepositorio
│
├── Data
│   ├── ApplicationDbContext
│   └── SeedIdentity
│
├── Models
│   ├── UsuarioModel
│   ├── EquipamentoModel
│   ├── FuncionarioModel
│   └── ManutencaoModel
│
├── ViewModels
│   ├── LoginViewModel
│   ├── RegistroViewModel
│   └── AlterarSenhaViewModel
│
├── Views
│   ├── Account
│   ├── Equipamento
│   ├── Funcionario
│   └── Home
│
└── wwwroot
    ├── css
    └── js
```

### Fluxo principal

```text
                Usuário
                   │
                   ▼
              Controller
                   │
                   ▼
                Service
                   │
                   ▼
              Repository
                   │
                   ▼
            Entity Framework
                   │
                   ▼
              SQL Server
```

O objetivo dessa separação é evitar que toda a lógica fique concentrada nos Controllers.

---

🔐 Autenticação e autorização

A autenticação é implementada utilizando **ASP.NET Core Identity**.

O sistema utiliza:

* `UserManager`
* `SignInManager`
* `RoleManager`
* Cookies de autenticação
* Roles
* [Authorize]
* Controle de acesso negado
* Bloqueio após tentativas de login
* Alteração de senha
* Hash de senhas através do Identity

### Fluxo de login

E-mail + Senha
      │
      ▼
AccountController
      │
      ▼
SignInManager
      │
      ▼
ASP.NET Core Identity
      │
      ▼
Autenticação
      │
      ▼
Cookie
      │
      ▼
Acesso ao sistema


O sistema também possui diferentes perfis:


Admin
Tecnico
Usuario




 👥 Controle de usuários

O usuário é baseado no `IdentityUser` através de uma classe personalizada:


public class UsuarioModel : IdentityUser
{
    public string? NomeCompleto { get; set; }

    public DateTime DataCadastro { get; set; }

    public int? FuncionarioId { get; set; }
}


Isso permite utilizar toda a infraestrutura de autenticação do ASP.NET Core Identity e ainda adicionar informações específicas da aplicação.



💻 Gerenciamento de equipamentos

O sistema permite cadastrar e gerenciar diferentes tipos de equipamentos:

* Notebook
* Desktop
* Monitor
* Celular
* Tablet
* Impressora
* Teclado
* Mouse

Cada equipamento possui informações como:

* Marca
* Modelo
* Número de série
* Data de compra
* Valor de compra
* Funcionário responsável
* Status

### Status dos equipamentos


Disponível
Em uso
Em manutenção
Descartado




 👨‍💼 Gerenciamento de funcionários

É possível cadastrar funcionários com informações como:

* Nome
* CPF
* Cargo
* Departamento
* Equipamentos atribuídos

O sistema também possui regras para impedir, por exemplo, o cadastro de funcionários com CPF duplicado.


🔧 Controle de manutenção

O sistema possui gerenciamento de manutenções dos equipamentos.

É possível:

* Registrar problemas
* Iniciar uma manutenção
* Finalizar uma manutenção
* Registrar observações
* Consultar manutenções
* Excluir manutenções finalizadas

Também existem regras de negócio para impedir operações inconsistentes.



 🧠 Regras de negócio

As regras de negócio são concentradas principalmente na camada de **Services**.

Exemplo:


public void Atribuir(int equipamentoId, int funcionarioId)
{
    var equipamento = _equipamentoRepositorio.BuscarPorId(equipamentoId);

    if (equipamento == null)
        throw new Exception("Equipamento não encontrado.");

    if (equipamento.Status != StatusEquipamentoEnum.Disponível)
        throw new Exception("O equipamento não está disponível.");

}

Dessa forma, o Controller fica responsável principalmente pelo fluxo HTTP, enquanto o Service concentra as regras da aplicação.



 🗄️ Banco de dados

O projeto utiliza:

SQL Server + Entity Framework Core

O `ApplicationDbContext` herda de:


IdentityDbContext<UsuarioModel>


permitindo que as tabelas do Identity e as entidades da aplicação sejam gerenciadas pelo mesmo contexto.

Principais entidades:

text
Usuario
Funcionario
Equipamento
Manutencao




 🔌 Dependency Injection

O projeto utiliza o sistema nativo de **Dependency Injection do ASP.NET Core**.

Exemplo:

builder.Services.AddScoped<IFuncionarioRepositorio, FuncionarioRepositorio>();
builder.Services.AddScoped<IEquipamentoRepositorio, EquipamentoRepositorio>();
builder.Services.AddScoped<FuncionarioService>();
builder.Services.AddScoped<EquipamentoService>();


Os Controllers recebem suas dependências através do construtor:

```csharp
public FuncionarioController(
    FuncionarioService funcionarioService,
    IFuncionarioRepositorio funcionarioRepositorio)
{
    _funcionarioService = funcionarioService;
    _funcionarioRepositorio = funcionarioRepositorio;
}



⚙️ Como executar o projeto
 1. Pré-requisitos

Instale:

* [.NET 10 SDK](https://dotnet.microsoft.com/)
* SQL Server
* Visual Studio 2026 ou outra IDE compatível
* Git

 2. Clone o projeto

bash
git clone URL_DO_SEU_REPOSITORIO


Entre na pasta:

bash
cd SistemadeGestaoDeAtivosDeTI


### 3. Configure a conexão com o banco

No appsettings.json configure a Connection String:

json
{
  "ConnectionStrings": {
    "ConexaoPadrao": "Server=SEU_SERVIDOR;Database=GestaoAtivosTI;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}

> Não publique senhas ou informações sensíveis da conexão no GitHub.

### 4. Execute as migrations

```bash
dotnet ef database update
```

### 5. Execute a aplicação

```bash
dotnet run
```

Ou execute diretamente pelo Visual Studio.

---

## 🔑 Usuário administrador

O projeto possui uma rotina de inicialização de usuários e roles através do `SeedIdentity`.

As roles utilizadas são:

```text
Admin
Tecnico
Usuario
```

> Em ambientes reais, credenciais administrativas devem ser configuradas de forma segura e não devem ficar expostas no código-fonte.

---

## 📌 Principais funcionalidades

| Funcionalidade                 | Status |
| ------------------------------ | ------ |
| Login                          | ✅      |
| Registro de usuários           | ✅      |
| Logout                         | ✅      |
| Alteração de senha             | ✅      |
| Controle de acesso             | ✅      |
| Roles                          | ✅      |
| Cadastro de funcionários       | ✅      |
| Cadastro de equipamentos       | ✅      |
| Atribuição de equipamentos     | ✅      |
| Devolução de equipamentos      | ✅      |
| Controle de manutenção         | ✅      |
| Validação de regras de negócio | ✅      |
| Entity Framework Core          | ✅      |
| SQL Server                     | ✅      |
| Repository Pattern             | ✅      |
| Service Layer                  | ✅      |

---

## 🎯 Objetivos de aprendizado

Este projeto também tem como objetivo aprofundar conhecimentos em desenvolvimento backend com .NET, principalmente:

* ASP.NET Core MVC
* C#
* Entity Framework Core
* ASP.NET Core Identity
* Autenticação e autorização
* Arquitetura em camadas
* Repository Pattern
* Service Layer
* Dependency Injection
* Banco de dados relacional
* Boas práticas de desenvolvimento

---

## 📚 Próximos passos

Algumas evoluções planejadas para o projeto:

* [ ] Melhorar o sistema de permissões
* [ ] Implementar Policies do Identity
* [ ] Criar dashboard com indicadores
* [ ] Adicionar filtros e paginação
* [ ] Melhorar tratamento global de exceções
* [ ] Implementar logs da aplicação
* [ ] Criar testes unitários
* [ ] Criar testes de integração
* [ ] Melhorar responsividade da interface
* [ ] Implementar auditoria das alterações
* [ ] Melhorar separação entre interfaces e implementações
* [ ] Evoluir a arquitetura conforme o projeto crescer

---

👨‍💻 Autor

Gabriel Sartori

Projeto desenvolvido para estudo e prática de desenvolvimento de aplicações web utilizando o ecossistema **.NET / ASP.NET Core**.
