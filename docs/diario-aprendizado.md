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

### ⏰ Tempo Investido Hoje: 3 horas
### 🎯 Progresso Geral: 45% concluído ⬆️

**Próxima sessão**: Criação do RepositoryContext e implementação dos repositórios concretos
