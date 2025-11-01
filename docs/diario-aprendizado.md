# Log de Aprendizado - Repository Pattern

## 📅 31 de Outubro de 2025

### 🎯 Meta do Dia
Configurar a estrutura inicial do projeto e implementar as interfaces base do Repository Pattern.

### ✅ O que foi Implementado Hoje:

#### 1. Estrutura Inicial do Projeto
- Criação da solution `AccountOwnerServer.sln`
- Configuração de 4 projetos:
  - `AccountOwnerServer` (Web API)
  - `Contracts` (Interfaces)
  - `LoggerService` (Logging)
  - `Entities` (Modelos de dados) ✨ **NOVO**

**💻 Comandos Executados:**
```powershell
# 1. Criação da pasta e solution
mkdir "Repository Pattern"
cd "Repository Pattern"
dotnet new sln -n AccountOwnerServer

# 2. Criação dos projetos
dotnet new webapi -n AccountOwnerServer      # Web API principal
dotnet new classlib -n Contracts            # Interfaces
dotnet new classlib -n LoggerService         # Logging
dotnet new classlib -n Entities             # Modelos de dados ✨

# 3. Adição dos projetos à solution
dotnet sln add AccountOwnerServer/AccountOwnerServer.csproj
dotnet sln add Contracts/Contracts.csproj
dotnet sln add LoggerService/LoggerService.csproj
dotnet sln add Entities/Entities.csproj     # ✨ NOVO

# 4. Configuração de referências
cd AccountOwnerServer
dotnet add reference ../Contracts/Contracts.csproj
dotnet add reference ../LoggerService/LoggerService.csproj
dotnet add reference ../Entities/Entities.csproj        # ✨ NOVO
cd ..

# 5. Instalação do Entity Framework Core ✨ NOVO
cd AccountOwnerServer
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
cd ..

# 5. Criação da documentação
mkdir docs
```

**📝 O que cada comando faz:**
- `dotnet new sln`: Cria uma solution que agrupa projetos relacionados
- `dotnet new webapi`: Cria projeto ASP.NET Core Web API com estrutura padrão
- `dotnet new classlib`: Cria biblioteca de classes para código compartilhado
- `dotnet sln add`: Adiciona projeto à solution para gerenciamento conjunto
- `dotnet add reference`: Cria referência entre projetos (dependência)

#### 2. Interface Genérica Base - `IRepositoryBase<T>`
```csharp
public interface IRepositoryBase<T>
{
    IQueryable<T> FindAll(bool trackChanges);
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
}
```

**Pontos-chave aprendidos:**
- **Generic Interface**: Permite reutilização para diferentes entidades
- **IQueryable**: Permite composição de queries (lazy loading)
- **Expression<Func<T, bool>>**: Para queries dinâmicas e flexíveis
- **trackChanges**: Controle de performance no Entity Framework

#### 3. Interfaces Específicas do Domínio
- `IAccountRepository`: Para operações específicas de Account
- `IOwnerRepository`: Para operações específicas de Owner

**Conceito aprendido**: Interface Segregation Principle - interfaces pequenas e específicas são melhores que uma interface grande e genérica.

#### 4. Interface Manager - `IRepositoryManager`
```csharp
public interface IRepositoryManager
{
    IAccountRepository Account { get; }
    IOwnerRepository Owner { get; }
    void Save();
}
```

**Benefícios identificados:**
- Centraliza acesso a todos os repositórios
- Controla transações através do método `Save()`
- Facilita injeção de dependência

#### 5. Criação das Entidades ✨ **NOVO**
- **Owner.cs**: Entidade principal com propriedades básicas
  ```csharp
  public class Owner
  {
      public Guid Id { get; set; }
      public string? Name { get; set; }
      public DateTime DateOfBirth { get; set; }
      public string? Address { get; set; }
      public ICollection<Account>? Accounts { get; set; } // Navigation property
  }
  ```

- **Account.cs**: Entidade relacionada com Owner
  ```csharp
  public class Account
  {
      public Guid Id { get; set; }
      public DateTime DateCreated { get; set; }
      public string? AccountType { get; set; }
      public Guid OwnerId { get; set; }      // Foreign key
      public Owner? Owner { get; set; }      // Navigation property
  }
  ```

**Conceitos aplicados:**
- **Data Annotations**: Validações e configurações de banco
- **Navigation Properties**: Relacionamento one-to-many
- **Foreign Key**: Chave estrangeira explícita
- **Nullable Reference Types**: Uso de `?` para propriedades opcionais

#### 6. Configuração do Entity Framework Core ✨ **NOVO**
- Instalação dos pacotes principais:
  - `Microsoft.EntityFrameworkCore.SqlServer` (9.0.10)
  - `Microsoft.EntityFrameworkCore.Tools` (9.0.10)
- Configuração de referências entre projetos

### 🤔 Reflexões do Dia

#### O que funcionou bem:
1. **Separação clara de responsabilidades** entre os projetos
2. **Nomenclatura consistente** das interfaces e métodos
3. **Estrutura flexível** que permite futuras expansões

#### Desafios encontrados:
1. **Decisão sobre trackChanges**: Quando usar true vs false?
   - **Aprendizado**: `true` para operações de escrita, `false` para leitura
2. **Granularidade das interfaces**: Muito genérico vs muito específico?
   - **Solução**: Combinar interface genérica base + interfaces específicas

#### Dúvidas que surgiram:
- Como implementar o Unit of Work pattern junto com Repository?
- Qual a melhor forma de tratar exceções nos repositórios?
- Como fazer paginação eficiente com IQueryable?

### 📚 Novos Conceitos Aprendidos

#### 1. Repository Pattern Benefits
- **Testabilidade**: Facilita mocking nas interfaces
- **Flexibilidade**: Troca de implementação sem afetar o código cliente
- **Centralização**: Lógica de acesso a dados em um local

#### 2. Generic Programming em C#
- Uso de `<T>` para criar código reutilizável
- Constraints em generics (para futuro estudo)

#### 3. Expression Trees
- `Expression<Func<T, bool>>` permite queries dinâmicas
- Diferença entre `Func<T, bool>` e `Expression<Func<T, bool>>`

### 🎯 Plano para Amanhã

#### Prioridade Alta:
1. **Criar modelos de dados (Entities)**
   - Owner entity
   - Account entity
   - Relacionamentos entre eles

2. **Configurar Entity Framework**
   - Instalar packages necessários
   - Criar RepositoryContext
   - Configurar connection string

#### Prioridade Média:
3. **Implementar repositórios concretos**
   - RepositoryBase<T> implementation
   - AccountRepository implementation
   - OwnerRepository implementation

### 💡 Insights Importantes
1. **Repository ≠ Service**: Repository é apenas para acesso a dados, não lógica de negócio
2. **Interface First**: Sempre começar pela interface, depois implementação
3. **SOLID Principles**: Cada interface deve ter uma responsabilidade específica

### 📖 Material para Estudar
- [ ] Entity Framework Core relationships
- [ ] Unit of Work pattern
- [ ] AutoMapper para DTOs
- [ ] Async/await patterns em repositórios

---

## 📅 1 de Novembro de 2025

### 🎯 Meta do Dia
Configurar o banco de dados Oracle e implementar ServiceExtensions para organização da arquitetura.

### ✅ O que foi Implementado Hoje:

#### 1. Configuração do Entity Framework Core com Oracle ✨ **NOVO**

**💻 Comandos Executados:**
```powershell
# 1. Instalação do Entity Framework Core base (já estava)
cd Entities
dotnet add package Microsoft.EntityFrameworkCore

# 2. Instalação do provedor Oracle
dotnet add package Oracle.EntityFrameworkCore

# 3. Substituição do SQL Server por Oracle no projeto principal
cd ../AccountOwnerServer
dotnet remove package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Oracle.EntityFrameworkCore
```

**📦 Pacotes Instalados:**
- `Microsoft.EntityFrameworkCore` (9.0.10)
- `Oracle.EntityFrameworkCore` (9.23.26000)
- `Oracle.ManagedDataAccess.Core` (23.26.0) - dependência automática

#### 2. Configuração do RepositoryContext ✨ **ATUALIZADO**
```csharp
using Microsoft.EntityFrameworkCore;
using Entities.Models;

namespace Entities;

public class RepositoryContext : DbContext
{
    public RepositoryContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Owner> Owners { get; set; }
    public DbSet<Account> Accounts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Owner>()
            .HasMany(o => o.Accounts)
            .WithOne(a => a.Owner)
            .HasForeignKey(a => a.OwnerId);

        base.OnModelCreating(modelBuilder);
    }
}
```

**Conceitos aplicados:**
- **UseOracle()**: Método de extensão do provedor Oracle
- **Fluent API**: Configuração de relacionamentos no OnModelCreating
- **HasMany/WithOne**: Relacionamento One-to-Many configurado

#### 3. Implementação do Pattern ServiceExtensions ✨ **NOVO**

**ServiceExtensions.cs** - Organização e modularização dos serviços:
```csharp
public static class ServiceExtensions
{
    public static void ConfigureOracleContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<Entities.RepositoryContext>(options =>
            options.UseOracle(connectionString));
    }

    public static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader());
        });
    }

    public static void ConfigureLoggerService(this IServiceCollection services)
    {
        services.AddSingleton<ILoggerManager, LoggerManager>();
    }
}
```

**Benefícios identificados:**
- ✅ **Separation of Concerns**: Cada método tem uma responsabilidade
- ✅ **Clean Program.cs**: Configuração mais limpa e legível
- ✅ **Reutilização**: Extensions podem ser testadas independentemente
- ✅ **Manutenibilidade**: Fácil de modificar configurações específicas

#### 4. Configuração de Connection Strings ✨ **NOVO**

**appsettings.json** (Produção):
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=localhost:1521/XE;User Id=your_username;Password=your_password;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

**appsettings.Development.json** (Desenvolvimento):
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=localhost:1521/XE;User Id=dev_user;Password=dev_password;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.EntityFrameworkCore.Database.Command": "Information"
    }
  }
}
```

**Conceitos aplicados:**
- **Configuration Hierarchy**: Development sobrescreve Production
- **Connection String Format**: Formato específico do Oracle
- **EF Core Logging**: Log de comandos SQL para debug

#### 5. Program.cs Atualizado ✨ **IMPLEMENTAÇÃO ATUAL**
```csharp
using Microsoft.EntityFrameworkCore;
using Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure Oracle Database (implementação direta)
builder.Services.AddDbContext<RepositoryContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure CORS (implementação direta)
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseRouting();
app.MapControllers();

await app.RunAsync();
```

**📝 Nota**: ServiceExtensions existem e estão configuradas, mas atualmente o Program.cs usa configuração direta por questões de compatibilidade de namespace.

### 🧠 Conceitos Importantes Aprendidos

#### 1. **CORS (Cross-Origin Resource Sharing)**
**O que é**: Política de segurança que controla acesso a recursos entre diferentes origens.

**Por que precisamos**:
- ✅ Frontend em `localhost:3000` → API em `localhost:5001`
- ✅ Aplicações SPA (React, Angular, Vue)
- ✅ Integração com aplicações de terceiros
- ✅ Desenvolvimento local

**Níveis de Permissão**:
```csharp
// 🚨 DESENVOLVIMENTO (muito permissivo)
.AllowAnyOrigin()
.AllowAnyMethod()
.AllowAnyHeader()

// ✅ PRODUÇÃO (mais seguro)
.WithOrigins("https://www.meusite.com")
.WithMethods("GET", "POST", "PUT", "DELETE")
.WithHeaders("Content-Type", "Authorization")
```

#### 2. **Oracle vs SQL Server - Diferenças Práticas**
| Aspecto | SQL Server | Oracle |
|---------|------------|---------|
| **Connection String** | `Server=.;Database=DB;` | `Data Source=localhost:1521/XE;` |
| **Package** | `Microsoft.EntityFrameworkCore.SqlServer` | `Oracle.EntityFrameworkCore` |
| **Method** | `.UseSqlServer()` | `.UseOracle()` |
| **Default Port** | 1433 | 1521 |

#### 3. **ServiceExtensions Pattern - Benefícios**
- **Testabilidade**: Cada extensão pode ser testada isoladamente
- **Modularidade**: Configurações organizadas por responsabilidade
- **Clean Code**: Program.cs mais limpo e legível
- **Reutilização**: Extensions podem ser usadas em outros projetos

### 🤔 Reflexões do Dia

#### O que funcionou bem:
1. **Migração SQL Server → Oracle** foi suave com Entity Framework
2. **ServiceExtensions** deixou o código muito mais organizado
3. **Configuração hierárquica** (appsettings) funciona perfeitamente

#### Desafios encontrados:
1. **Problemas de Namespace**: Conflitos entre `Entities` e `Entities.Models`
   - **Solução**: Organização clara dos usings e namespaces
2. **Connection String Format**: Oracle tem formato específico
   - **Aprendizado**: Sempre verificar documentação do provedor

#### Dúvidas que surgiram:
- Como configurar Oracle Connection Pooling para performance?
- Quais são as melhores práticas de segurança para CORS em produção?
- Como implementar Health Checks para conexão Oracle?

### 📚 Novos Conceitos Técnicos

#### 1. **Extension Methods Avançados**
```csharp
public static void ConfigureXXX(this IServiceCollection services, IConfiguration configuration)
```
- **this IServiceCollection**: Permite chamada fluente
- **Injeção de IConfiguration**: Acesso às configurações

#### 2. **Oracle Entity Framework Provider**
- **Managed Data Access**: Provider oficial da Oracle
- **Auto Dependencies**: Instala Oracle.ManagedDataAccess.Core automaticamente
- **Version Compatibility**: EF Core 9.0 + Oracle 9.23

#### 3. **CORS Pipeline Order**
```csharp
app.UseHttpsRedirection();  // 1. HTTPS primeiro
app.UseCors("CorsPolicy");  // 2. CORS antes do routing
app.UseRouting();           // 3. Routing depois
```

### 🎯 Próximos Passos Identificados

#### Prioridade Alta:
1. **Implementar Repository Base Concreto**
   - RepositoryBase<T> com Oracle
   - Async/await patterns
   - Error handling específico para Oracle

2. **Configurar Migrations**
   - `dotnet ef migrations add InitialCreate`
   - Testar criação de tabelas no Oracle

#### Prioridade Média:
3. **Implementar Health Checks**
   - Verificação de conexão com Oracle
   - Endpoint de status da aplicação

4. **Configurar Logging Avançado**
   - NLog ou Serilog
   - Logs específicos para operações de banco

### 💡 Insights Importantes
1. **Provider Independence**: Entity Framework abstrai diferenças entre bancos
2. **Configuration Pattern**: ServiceExtensions é padrão em projetos .NET modernos
3. **Security First**: CORS deve ser configurado desde o início, mas ajustado para produção
4. **Environment-Specific Config**: Configurações diferentes por ambiente são essenciais

### 📖 Material para Estudar Próximo
- [ ] Oracle-specific EF Core features
- [ ] Advanced CORS scenarios
- [ ] Connection pooling with Oracle
- [ ] Performance tuning Oracle + EF Core

---

### ⏰ Tempo Investido Hoje: 2 horas
### 🎯 Progresso Geral: 65% concluído ⬆️

#### 6. Implementações Adicionais Descobertas ✨ **COMPLETAS MAS NÃO DOCUMENTADAS**

##### **LoggerService Completo com NLog**
```csharp
public class LoggerManager : ILoggerManager
{
    private static ILogger logger = LogManager.GetCurrentClassLogger();

    public void LogDebug(string message) => logger.Debug(message);
    public void LogError(string message) => logger.Error(message);
    public void LogInfo(string message) => logger.Info(message);
    public void LogWarn(string message) => logger.Warn(message);
}
```

**Pacotes instalados:**
- `NLog.Extensions.Logging` (5.3.14)
- Integração completa com ILoggerManager interface

##### **Data Annotations Avançadas nos Modelos**
```csharp
[Table("Owner")]
public class Owner
{
    public Guid OwnerId { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    [StringLength(60, ErrorMessage = "Name cannot be longer than 60 characters.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Date of Birth is required.")]
    public DateTime DateOfBirth { get; set; }

    [Required(ErrorMessage = "Address is required.")]
    [StringLength(100, ErrorMessage = "Address cannot be longer than 100 characters.")]
    public string Address { get; set; } // ⚠️ Warning: Nullable issue

    public ICollection<Models.Account>? Accounts { get; set; }
}
```

**Conceitos implementados:**
- **Table Mapping**: `[Table("Owner")]` define nome da tabela
- **Validation Attributes**: Required, StringLength com mensagens customizadas
- **Navigation Properties**: Coleção tipada para relacionamento 1:N

##### **ServiceExtensions Completo (Disponível mas não usado)**
```csharp
public static class ServiceExtensions
{
    public static void ConfigureOracleContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<Entities.RepositoryContext>(options =>
            options.UseOracle(configuration.GetConnectionString("DefaultConnection")));
    }

    public static void ConfigureCors(this IServiceCollection services) { /* implementado */ }

    public static void ConfigureLoggerService(this IServiceCollection services)
    {
        services.AddSingleton<ILoggerManager, LoggerManager>();
    }

    public static void ConfigureIISIntegration(this IServiceCollection services)
    {
        services.Configure<IISOptions>(options => { });
    }
}
```

### 🐛 **Issues Identificadas que Precisam de Correção**

#### 1. **Nullable Reference Types Warnings**
```csharp
// ⚠️ PROBLEMA: Address não pode ser null mas não está marcada como nullable
public string Address { get; set; }

// ⚠️ PROBLEMA: AccountType não pode ser null mas não está marcada como nullable
public string AccountType { get; set; }
```

#### 2. **Namespace Inconsistência**
```csharp
// No Account.cs - referência desnecessária
public Models.Owner? Owner { get; set; }

// No Owner.cs - referência desnecessária
public ICollection<Models.Account>? Accounts { get; set; }
```

### 🎯 **Status Real vs Documentado**

| Componente | Documentado | Implementado | Status |
|------------|-------------|--------------|---------|
| **Estrutura Base** | ✅ | ✅ | ✅ Completo |
| **Oracle Config** | ✅ | ✅ | ✅ Completo |
| **CORS** | ✅ | ✅ | ✅ Completo |
| **ServiceExtensions** | ✅ | ✅ | ⚠️ Existe mas não usado |
| **LoggerService + NLog** | ❌ | ✅ | ⚠️ Não documentado |
| **Data Annotations** | ❌ | ✅ | ⚠️ Não documentado |
| **Build Success** | ❌ | ✅ | ⚠️ Funcionando |

**Próxima sessão**: Corrigir warnings de nullable types e implementar repositórios concretos
