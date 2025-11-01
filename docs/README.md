# Estudo: ASP.NET Core Web API - Padrão de Repositório

## 📚 Objetivo do Projeto
Este projeto tem como objetivo estudar e implementar o **Padrão de Repositório (Repository Pattern)** em uma aplicação ASP.NET Core Web API, seguindo as melhores práticas de arquitetura e design patterns.

## 🎯 O que é o Padrão de Repositório?
O Repository Pattern é um padrão de design que encapsula a lógica necessária para acessar fontes de dados. Ele centraliza a funcionalidade comum de acesso a dados, fornecendo melhor manutenibilidade e desacoplando a infraestrutura ou tecnologia usada para acessar bancos de dados da camada de modelo de domínio.

### Vantagens:
- ✅ **Testabilidade**: Facilita a criação de testes unitários
- ✅ **Manutenibilidade**: Código mais organizado e fácil de manter
- ✅ **Flexibilidade**: Facilita mudanças na fonte de dados
- ✅ **Separação de Responsabilidades**: Separa a lógica de negócio do acesso a dados

## 🏗️ Estrutura do Projeto

### Projetos Criados:
1. **AccountOwnerServer** - Projeto principal da Web API
2. **Contracts** - Interfaces e contratos do sistema
3. **LoggerService** - Serviço de logging

### Arquitetura Implementada:
```
📁 Repository Pattern/
├── 📁 docs/                    # Documentação do projeto
├── 📁 AccountOwnerServer/       # Web API principal
├── 📁 Contracts/               # Interfaces e contratos
├── 📁 LoggerService/           # Serviço de logging
└── AccountOwnerServer.sln      # Solution file
```

## � Comandos Utilizados para Criar a Estrutura

### 1. Criação da Solution e Projetos

```powershell
# Criar pasta do projeto
mkdir "Repository Pattern"
cd "Repository Pattern"

# Criar a solution
dotnet new sln -n AccountOwnerServer

# Criar projeto Web API principal
dotnet new webapi -n AccountOwnerServer

# Criar projeto de bibliotecas de classe
dotnet new classlib -n Contracts
dotnet new classlib -n LoggerService

# Adicionar projetos à solution
dotnet sln AccountOwnerServer.sln add AccountOwnerServer/AccountOwnerServer.csproj
dotnet sln AccountOwnerServer.sln add Contracts/Contracts.csproj
dotnet sln AccountOwnerServer.sln add LoggerService/LoggerService.csproj
```

### 2. Criar Pasta de Documentação

```powershell
# Criar pasta docs
mkdir docs
```

### 📝 Explicação dos Comandos:

#### **Comandos de Solution (.sln)**
- `dotnet new sln -n AccountOwnerServer`: Cria uma nova solution com o nome especificado
- `dotnet sln add [projeto]`: Adiciona um projeto existente à solution
- **Finalidade**: A solution agrupa múltiplos projetos relacionados e facilita o build e gerenciamento

#### **Comandos de Projeto**
- `dotnet new webapi -n [nome]`: Cria um novo projeto ASP.NET Core Web API
  - Inclui controllers, Program.cs, appsettings.json
  - Configuração padrão para API REST

- `dotnet new classlib -n [nome]`: Cria uma biblioteca de classes (.dll)
  - Projeto que pode ser referenciado por outros projetos
  - Usado para interfaces, modelos, serviços compartilhados

#### **Estrutura de Pastas**
- `mkdir [nome]`: Cria uma nova pasta
- Cada projeto é criado em sua própria pasta
- Separação clara de responsabilidades por projeto

### 3. Configuração de Referências entre Projetos

```powershell
# AccountOwnerServer precisa referenciar Contracts e LoggerService
cd AccountOwnerServer
dotnet add reference ../Contracts/Contracts.csproj
dotnet add reference ../LoggerService/LoggerService.csproj

# Voltar para a raiz
cd ..
```

### 📚 **Por que essa Estrutura?**

1. **Separação de Camadas**: Cada projeto tem uma responsabilidade específica
2. **Reutilização**: Projetos de biblioteca podem ser referenciados por outros projetos
3. **Testabilidade**: Facilita a criação de testes isolados
4. **Manutenibilidade**: Mudanças em uma camada não afetam outras diretamente

### 🔧 **Comandos Úteis para Verificação**

```powershell
# Listar projetos na solution
dotnet sln list

# Verificar dependências de um projeto
dotnet list [projeto] reference

# Build de toda a solution
dotnet build

# Executar o projeto principal
dotnet run --project AccountOwnerServer
```

## �📋 Progresso de Implementação

### ✅ Etapas Concluídas:

#### 1. Configuração Inicial do Projeto
- [x] Criação da solution `AccountOwnerServer.sln`
- [x] Projeto principal `AccountOwnerServer` (ASP.NET Core Web API)
- [x] Projeto `Contracts` para interfaces
- [x] Projeto `LoggerService` para logging
- [x] Configuração básica do `Program.cs`

#### 2. Implementação das Interfaces Base
- [x] **IRepositoryBase<T>** - Interface genérica base para repositórios
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

#### 3. Interfaces Específicas do Domínio
- [x] **IAccountRepository** - Interface para operações de Account
- [x] **IOwnerRepository** - Interface para operações de Owner
- [x] **IRepositoryManager** - Interface para gerenciar todos os repositórios

#### 4. Padrão Manager
- [x] Implementação do `IRepositoryManager` que agrupa todos os repositórios
- [x] Método `Save()` para controle de transações

### 🔄 Próximas Etapas Planejadas:

#### 5. Modelos de Dados (Entities)
- [ ] Criar pasta `Entities`
- [ ] Implementar modelo `Owner`
- [ ] Implementar modelo `Account`
- [ ] Configurar relacionamentos entre entidades

#### 6. Contexto do Banco de Dados
- [ ] Instalar Entity Framework Core
- [ ] Criar `RepositoryContext` herdando de `DbContext`
- [ ] Configurar connection string
- [ ] Implementar DbSets para as entidades

#### 7. Implementação dos Repositórios
- [ ] Criar projeto `Repository`
- [ ] Implementar `RepositoryBase<T>` concreta
- [ ] Implementar `AccountRepository`
- [ ] Implementar `OwnerRepository`
- [ ] Implementar `RepositoryManager`

#### 8. Configuração de Dependências
- [ ] Configurar injeção de dependência no `Program.cs`
- [ ] Registrar repositórios no container DI
- [ ] Configurar Entity Framework

#### 9. Controllers
- [ ] Criar `OwnersController`
- [ ] Criar `AccountsController`
- [ ] Implementar endpoints CRUD

#### 10. Testes
- [ ] Criar projeto de testes unitários
- [ ] Implementar testes para repositórios
- [ ] Implementar testes para controllers

## 🛠️ Tecnologias Utilizadas
- **ASP.NET Core 9.0** - Framework web
- **Entity Framework Core** (planejado) - ORM para acesso a dados
- **SQL Server** (planejado) - Banco de dados
- **xUnit** (planejado) - Framework de testes

## 📖 Conceitos Aprendidos

### 1. Separação de Responsabilidades
Aprendeu-se a importância de separar as responsabilidades em diferentes projetos:
- **Contracts**: Apenas interfaces, sem implementação
- **Entities**: Modelos de dados
- **Repository**: Implementação do acesso a dados
- **API**: Controllers e configuração

### 2. Interface Segregation Principle (ISP)
Implementação de interfaces específicas (`IAccountRepository`, `IOwnerRepository`) ao invés de uma interface monolítica.

### 3. Dependency Inversion Principle (DIP)
Uso de interfaces para abstrair as implementações concretas, permitindo maior flexibilidade e testabilidade.

### 4. Generic Repository Pattern
Implementação de um repositório genérico (`IRepositoryBase<T>`) que pode ser reutilizado por diferentes entidades.

## 📝 Anotações e Observações

### Boas Práticas Identificadas:
1. **Uso de Expressões Lambda**: Para queries flexíveis no `FindByCondition`
2. **Controle de Tracking**: Parâmetro `trackChanges` para otimização de performance
3. **Padrão Manager**: Centralização do acesso aos repositórios
4. **Separação em Projetos**: Organização clara da arquitetura

### Dúvidas para Pesquisar:
- [ ] Quando usar `trackChanges = true` vs `trackChanges = false`?
- [ ] Como implementar Unit of Work pattern junto com Repository?
- [ ] Melhores práticas para tratamento de exceções nos repositórios

## 🔗 Recursos de Estudo
- [Microsoft Docs - Repository Pattern](https://docs.microsoft.com/en-us/dotnet/architecture/)
- [Code Maze - Repository Pattern Tutorial](https://code-maze.com/)
- [Entity Framework Core Documentation](https://docs.microsoft.com/en-us/ef/core/)

---

**Data de Início**: 31 de Outubro de 2025
**Última Atualização**: 31 de Outubro de 2025
**Status**: Em Progresso 🚧
