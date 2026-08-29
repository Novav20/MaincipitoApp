# MaincipitoApp — Home Hospitalization & Clinical Care Platform

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=flat&logo=dotnet)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-12.0-239120?style=flat&logo=csharp)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![Database](https://img.shields.io/badge/Database-SQL_Server_2022-CC292B?style=flat&logo=microsoftsqlserver)](https://www.microsoft.com/en-us/sql-server)
[![Docker](https://img.shields.io/badge/Container-Docker-2496ED?style=flat&logo=docker)](https://www.docker.com/)
[![Tests](https://img.shields.io/badge/Tests-xUnit_%2B_FluentAssertions-informational?style=flat)](https://xunit.net/)

MaincipitoApp is a layered healthcare enterprise web application designed for outpatient home care management, clinical history tracking, vital signs monitoring, and healthcare personnel assignment.

Originally developed as an academic foundation, this repository has been refactored and modernized to **.NET 8**, implementing **Clean Architecture / N-Tier separation**, the **Generic Repository Pattern with 100% Async/Await semantics**, and containerized persistence using **Microsoft SQL Server 2022**.

---

## 🏛️ Architecture & Solution Layout

The solution is strictly organized into `src/` (production code) and `tests/` (automated test suites) following Domain-Driven Design (DDD) layering:

```text
Maincipito/
├── src/
│   ├── Maincipito.Domain/          # Pure Domain layer: Entities, Enums, and Repository Contracts (DIP)
│   ├── Maincipito.Persistence/     # Infrastructure layer: EF Core 8 DbContext, Repositories, Migrations
│   └── Maincipito.Web/             # Presentation layer: ASP.NET Core 8 Razor Pages & Identity UI
└── tests/
    ├── Maincipito.Domain.Tests/    # Unit tests: Entity invariants & DataAnnotations validation
    └── Maincipito.Persistence.Tests/# Integration tests: EF Core In-Memory Repository operations
```

### Key Architectural Highlights
1. **Dependency Inversion Principle (DIP):** Repository contracts (`IRepository<T>`, `IPatientRepository`, etc.) live in the **Domain** layer, while concrete implementations and EF Core dependencies are isolated inside **Persistence**.
2. **Modern C# 12 Standards:** File-scoped namespaces, Nullable Reference Types (`<Nullable>enable</Nullable>`), Primary Constructors, Collection Expressions (`[]`), and Pattern Matching.
3. **Async-First Data Access:** All repository methods leverage `Task<T>`, `CancellationToken`, `AsNoTracking()`, and asynchronous EF Core execution (`ToListAsync`, `FirstOrDefaultAsync`, `FindAsync`).
4. **Security & Secrets Governance:** Database credentials and connection strings are managed via `dotnet user-secrets` in development and environment variables (`.env`) for Docker Compose, avoiding hardcoded secrets.

---

## 🛠️ Technology Stack

| Layer / Concern | Technology |
| :--- | :--- |
| **Runtime & Language** | .NET 8 SDK / C# 12 |
| **Web Presentation** | ASP.NET Core 8 Razor Pages (Minimal Hosting in `Program.cs`) |
| **Authentication & Security** | ASP.NET Core Identity (Cookie-based auth & EF Core Stores) |
| **ORM & Data Access** | Entity Framework Core 8.0 (`Microsoft.EntityFrameworkCore.SqlServer`) |
| **Database** | Microsoft SQL Server 2022 (Linux Container via Docker Compose) |
| **Testing Frameworks** | xUnit, FluentAssertions, EF Core In-Memory Database |
| **DevOps & Tooling** | Docker, Docker Compose, EF Core Design-Time Factory |

---

## 🚀 Getting Started

### Prerequisites
* [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
* [Docker Desktop](https://www.docker.com/products/docker-desktop) or Docker Engine

### 1. Clone the repository
```bash
git clone https://github.com/Novav20/MaincipitoApp.git
cd MaincipitoApp
```

### 2. Configure Environment & Start SQL Server
```bash
# Copy the environment template
cp .env.example .env

# Start SQL Server 2022 container in background
docker compose up -d
```

### 3. Initialize User Secrets (Development Connection String)
```bash
# Configure Web Project Secrets
dotnet user-secrets init --project src/Maincipito.Web
dotnet user-secrets set "ConnectionStrings:MyAppContext" "Server=localhost,1433;Database=MaincipitoAppDb;User Id=sa;Password=MaincipitoStrongPass2026!;TrustServerCertificate=True;MultipleActiveResultSets=true" --project src/Maincipito.Web
dotnet user-secrets set "ConnectionStrings:IdentityDataContextConnection" "Server=localhost,1433;Database=MaincipitoIdentityDb;User Id=sa;Password=MaincipitoStrongPass2026!;TrustServerCertificate=True;MultipleActiveResultSets=true" --project src/Maincipito.Web

# Configure Persistence Project Secrets (for Design-Time Migrations)
dotnet user-secrets init --project src/Maincipito.Persistence
dotnet user-secrets set "ConnectionStrings:MyAppContext" "Server=localhost,1433;Database=MaincipitoAppDb;User Id=sa;Password=MaincipitoStrongPass2026!;TrustServerCertificate=True;MultipleActiveResultSets=true" --project src/Maincipito.Persistence
```

### 4. Apply Database Migrations
```bash
# Apply Domain Migrations
dotnet ef database update --project src/Maincipito.Persistence --startup-project src/Maincipito.Persistence --context MaincipitoDbContext

# Apply Identity Security Migrations
dotnet ef database update --project src/Maincipito.Web --startup-project src/Maincipito.Web --context IdentityDataContext
```

### 5. Run the Application
```bash
dotnet run --project src/Maincipito.Web
```
Navigate to `http://localhost:5000` (or `https://localhost:5001`).

---

## 🧪 Running Automated Tests

Run the full test suite across all test projects:

```bash
dotnet test
```

---

## 📄 License
This project is open-source and available under the [MIT License](LICENSE).