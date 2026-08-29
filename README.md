# MaincipitoApp — Plataforma de Hospitalización Domiciliaria y Gestión Clínica

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=flat&logo=dotnet)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-12.0-239120?style=flat&logo=csharp)](https://docs.microsoft.com/es-es/dotnet/csharp/)
[![Base de Datos](https://img.shields.io/badge/Database-SQL_Server_2022-CC292B?style=flat&logo=microsoftsqlserver)](https://www.microsoft.com/es-es/sql-server)
[![Docker](https://img.shields.io/badge/Contenedor-Docker-2496ED?style=flat&logo=docker)](https://www.docker.com/)
[![Pruebas](https://img.shields.io/badge/Pruebas-xUnit_%2B_FluentAssertions-informational?style=flat)](https://xunit.net/)
[![Misión TIC](https://img.shields.io/badge/Misi%C3%B3n_TIC_2022-Universidad_de_Caldas-008080?style=flat)](https://www.ucaldas.edu.co/)

**MaincipitoApp** es una aplicación web empresarial diseñada para la gestión clínica de pacientes ambulatorios en programas de hospitalización en casa, seguimiento de historias clínicas, registro y monitoreo de parámetros fisiológicos (signos vitales) y asignación de personal médico.

Originado como proyecto integrador en el programa **Misión TIC 2022 (Universidad de Caldas)**, este repositorio ha sido completamente refactorizado y modernizado a **.NET 8**, adoptando **Arquitectura  N-Capas**, el **Patrón de Repositorio Genérico 100% Asíncrono (`Async/Await`)**, gobernanza de seguridad con **Secret Manager (`user-secrets`)** y persistencia en contenedor **Microsoft SQL Server 2022 con Docker Compose**.

---

## 🏛️ Arquitectura y Estructura de la Solución

La solución está estructurada bajo principios de separación de responsabilidades y **Domain-Driven Design (DDD)**, dividida formalmente en `src/` (código de producción) y `tests/` (suite de pruebas automatizadas):

```text
Maincipito/
├── src/
│   ├── Maincipito.Domain/          # Capa de Dominio pura: Entidades, Enums, Contratos de Repositorios (DIP)
│   ├── Maincipito.Persistence/     # Capa de Infraestructura: DbContext EF Core 8, Repositorios Async, Migraciones
│   └── Maincipito.Web/             # Capa de Presentación: ASP.NET Core 8 Razor Pages, Identity UI y Tokens de Diseño
└── tests/
    ├── Maincipito.Domain.Tests/    # Pruebas unitarias: Invariantes de dominio y validación DataAnnotations
    └── Maincipito.Persistence.Tests/# Pruebas de integración: Repositorios aislados con EF Core InMemory DB
```

### Principales Aspectos de Ingeniería
1. **Principio de Inversión de Dependencias (DIP):** Las interfaces de persistencia (`IRepository<T>`, `IPatientRepository`, etc.) residen en la capa de **Dominio**, mientras que las implementaciones de base de datos están aisladas en **Persistencia**, garantizando desacoplamiento del framework.
2. **C# 12 Moderno:** Adopción de *File-scoped namespaces*, análisis de nulabilidad estricto (`<Nullable>enable</Nullable>`), *Primary Constructors*, *Collection Expressions* (`[]`) y *Pattern Matching*.
3. **Acceso a Datos Asíncrono de Alto Rendimiento:** Consultas optimizadas con `Task<T>`, `CancellationToken`, `AsNoTracking()` para lecturas y mitigación de explosión cartesiana mediante `QuerySplittingBehavior.SplitQuery`.
4. **Seguridad y Gestión de Secretos:** Eliminación total de credenciales en código fuente mediante `dotnet user-secrets` en desarrollo y parametrización con `.env` para Docker.

---

## 🛠️ Stack Tecnológico

| Componente | Tecnología |
| :--- | :--- |
| **Lenguaje y Runtime** | C# 12 / .NET 8.0 SDK |
| **Presentación Web** | ASP.NET Core 8 Razor Pages (*Minimal Hosting* en `Program.cs`) |
| **Autenticación y Seguridad** | ASP.NET Core Identity (Autenticación basada en cookies) |
| **ORM y Persistencia** | Entity Framework Core 8.0 (`Microsoft.EntityFrameworkCore.SqlServer`) |
| **Motor de Base de Datos** | Microsoft SQL Server 2022 (Linux Container vía Docker Compose) |
| **Suite de Pruebas** | xUnit, FluentAssertions, EF Core In-Memory Database Provider |
| **Diseño y UI** | Bootstrap 5, FontAwesome, Tokens de Diseño CSS con numeración tabular |

---

## 🚀 Guía de Instalación y Ejecución Local

### Prerrequisitos
* [.NET 8.0 SDK](https://dotnet.microsoft.com/es-es/download/dotnet/8.0)
* [Docker Engine](https://docs.docker.com/engine/install/) y Docker Compose

### 1. Clonar el repositorio
```bash
git clone https://github.com/Novav20/MaincipitoApp.git
cd MaincipitoApp
```

### 2. Configurar Entorno e Iniciar SQL Server
```bash
# Copiar plantilla de variables de entorno
cp .env.example .env

# Levantar el contenedor de SQL Server 2022 en segundo plano
docker compose up -d
```

### 3. Configurar Secretos de Desarrollo (User Secrets)
```bash
# Configurar secretos del proyecto Web
dotnet user-secrets init --project src/Maincipito.Web
dotnet user-secrets set "ConnectionStrings:MyAppContext" "Server=localhost,1433;Database=MaincipitoAppDb;User Id=sa;Password=MaincipitoStrongPass2026!;TrustServerCertificate=True;MultipleActiveResultSets=true" --project src/Maincipito.Web
dotnet user-secrets set "ConnectionStrings:IdentityDataContextConnection" "Server=localhost,1433;Database=MaincipitoIdentityDb;User Id=sa;Password=MaincipitoStrongPass2026!;TrustServerCertificate=True;MultipleActiveResultSets=true" --project src/Maincipito.Web

# Configurar secretos del proyecto Persistence (para migraciones en tiempo de diseño)
dotnet user-secrets init --project src/Maincipito.Persistence
dotnet user-secrets set "ConnectionStrings:MyAppContext" "Server=localhost,1433;Database=MaincipitoAppDb;User Id=sa;Password=MaincipitoStrongPass2026!;TrustServerCertificate=True;MultipleActiveResultSets=true" --project src/Maincipito.Persistence
```

### 4. Aplicar Migraciones de Base de Datos
```bash
# Aplicar migraciones del Dominio
dotnet ef database update --project src/Maincipito.Persistence --startup-project src/Maincipito.Persistence --context MaincipitoDbContext

# Aplicar migraciones de Seguridad e Identidad
dotnet ef database update --project src/Maincipito.Web --startup-project src/Maincipito.Web --context IdentityDataContext
```

### 5. Iniciar la Aplicación Web
```bash
dotnet run --project src/Maincipito.Web
```
Abre en tu navegador: **`http://localhost:5000`** (o `https://localhost:5001`).

---

## 🧪 Ejecución de Pruebas Automatizadas

Ejecuta la suite completa de pruebas unitarias y de integración:

```bash
dotnet test
```

---

## 📄 Licencia
Este proyecto es de código abierto y está disponible bajo la licencia [MIT](LICENSE).