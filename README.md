# 📘 Job Posting MVC Application

A .NET 8 MVC Web Application for Managing Employers, Jobs, and Applicants

**Status:** Coursework application, not a production hiring platform. See the [portfolio case study](https://www.cameron-basham.com/projects/job-posting-mvc) for recorded application screens labeled **“Local demonstration with fictional data.”** No installation is needed to view that walkthrough.

## 📚 Overview

This project is a Job Posting Management System built using ASP.NET Core MVC and Entity Framework Core.
It provides record-management screens to:

- Manage Employers
- Create and edit Job Listings
- Manage Applicants
- Model an applicant/job relationship in the database; there is no implemented Apply-to-Job or application-tracking screen.

The application demonstrates MVC architecture, EF Core models and code-first migrations, CRUD operations, dependency injection, and relational modeling.

This project was completed as part of a school assignment to reinforce ASP.NET Core MVC fundamentals and relational data modeling.

## 🚀 Features

### ✔ Employers

- Create, edit, delete employers
- View all employers (Home page)
- Each employer can be associated with multiple jobs

### ✔ Jobs

- Create jobs assigned to an employer
- View jobs with employer names
- Edit & delete jobs
- One-to-many relationship (Employer → Jobs)

### ✔ Applicants

- Create, edit, delete applicants
- Store applicant name & email
- Applicant management is separate from job applications.

### ✔ Many-to-Many: Applicants ↔ Jobs

The `ApplicantJob` join entity models a potential many-to-many relationship using `ApplicantId` and `JobId`. The current controllers and views do not create, edit, or list application submissions. This is schema support for a future feature, not an implemented application workflow.

## Data Model

### 🔹 Employer

- Id
- Name
- Jobs (`ICollection<Job>`)

### 🔹 Job

- Id
- Title
- Description
- EmployerId
- Employer (navigation)
- ApplicantJobs (navigation)

### 🔹 Applicant

- Id
- Name
- Email
- ApplicantJobs (navigation)

### 🔹 ApplicantJob (Join Table)

- ApplicantId
- JobId
- Navigation to Applicant
- Navigation to Job

## 🧰 Tech Stack

| Technology | Purpose |
| --- | --- |
| .NET 8 | Core application framework |
| ASP.NET Core MVC | Controllers, routing, views |
| Entity Framework Core 6.0.2; Design/Relational references 6.0.0; Pomelo 6.0.0 | ORM and database access |
| MySQL 8 | Relational database; `Program.cs` declares server version 8.0.37 |
| Razor Views | UI rendering |
| Dependency Injection | Service management |
| C# | Primary development language |

## ⚙️ Installation & Setup

**Historical reproduction only.** [.NET 8 support ended November 10, 2026](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core), and [EF Core 6 support ended November 12, 2024](https://learn.microsoft.com/en-us/ef/core/what-is-new/). These versions describe the existing project, not a recommendation for a new deployment. Plan a separately reviewed upgrade to a supported .NET/EF Core/MySQL-provider combination before production use; no upgrade is included here.

For local reproduction, prepare a toolchain that can build `net8.0`, an ASP.NET Core 8 runtime, a separately installed MySQL server/database, and compatible EF Core 6 tooling. A newer SDK alone does not guarantee the older application or EF tool runtime is available. The retained Windows capture used MySQL 8.0.37; other platform/version combinations are not established by that demonstration.

The commands below are source-checked instructions, not a verified fresh-machine setup recipe. Run them from the repository root unless shown otherwise. Restore/build/tool commands can download packages and write caches/output; migration and startup commands can contact the configured database.

1️⃣ Clone the Project

```powershell
git clone https://github.com/Dumpy250/JobPosting.git
cd JobPosting
```

2️⃣ Configure Database Connection

The tracked `appsettings.json` and `appsettings.Development.json` deliberately leave
`ConnectionStrings:DefaultConnection` empty. Do not put database credentials in
these files or commit local configuration containing credentials.

Before running the application or applying migrations, configure the environment
variable `ConnectionStrings__DefaultConnection` through your local development
process settings or your deployment platform's secret manager. Supply the full
MySQL connection string there; the double underscore maps to the configuration
key `ConnectionStrings:DefaultConnection`. ASP.NET Core's default configuration
loads this environment override without code changes.

Connection-string shape only (replace placeholders through your local process settings, not in committed files or shell history):

```text
Server=YOUR_DB_HOST;Port=YOUR_DB_PORT;Database=YOUR_DEDICATED_DB;User ID=YOUR_DB_USER;Password=YOUR_DB_PASSWORD;
```

Use a dedicated, least-privilege database account and a newly rotated credential.
Do not paste credentials into shell commands, shell history, screenshots, or logs.
The application checks the database connection on startup, so do not start it
until the environment is configured for a database you are authorized to access.

`bin/` and `obj/` are generated output, ignored recursively at every project depth.
Keep them out of commits, including copied application settings and publish output.
Deleting tracked copies does not invalidate exposed credentials or remove them
from Git history. The owner must rotate the exposed credential everywhere it was
reused and separately address historical exposure.

3️⃣ Prepare Compatible Entity Framework CLI

Use EF Core **6.x** tooling for this historical project, not an unversioned install of the current CLI. The retained capture used an existing `dotnet-ef` **6.0.36** with process-specific runtime compatibility settings; those settings are not part of the repository or a general installation recipe. Check the tool's own runtime requirements separately from the application's .NET 8 target. See Microsoft's [EF CLI documentation](https://learn.microsoft.com/en-us/ef/core/cli/dotnet).

In an already prepared environment, check the selected tool before any database operation:

```powershell
dotnet ef --version
```

4️⃣ Apply Migrations

**This is a database write, not a connectivity check.** Use only a disposable development database you are authorized to modify. The target database name comes from `ConnectionStrings__DefaultConnection`; it is not fixed to `JobPostingDb`. EF tooling may build and execute startup code, so configure the intended connection first.

```powershell
dotnet ef database update
```

This applies the checked-in migration `20240505053744_InitialCreate`, creating its tables (`Employers`, `Jobs`, `Applicants`, `ApplicantJobs`) and migration history. It may also create the configured database if it does not exist and the account has permission. Normal application startup does not apply migrations automatically.

**Portability caveat:** the migration uses the mixed-case names above, while current model mappings use lowercase names. MySQL [identifier case sensitivity](https://dev.mysql.com/doc/refman/8.0/en/identifier-case-sensitivity.html) differs by platform/configuration. Reconcile this before using a case-sensitive server; do not change a shared server's settings as a shortcut. The migration also predates some current field-length mappings, so it is not a complete match for the current model.

5️⃣ Run the Application

```powershell
dotnet run --launch-profile http
```

The checked-in `http` profile requests `http://localhost:5073/`; use the actual listening URL printed by the application. The separate `https` profile also requests `https://localhost:7069/`. HTTPS redirection middleware is present, but the HTTP profile is not evidence of a working TLS deployment.

Startup opens a database connection and logs success or failure; a listening web server does not prove the database is usable. ASP.NET Core can also persist Data Protection keys outside the repository using its [default key storage](https://learn.microsoft.com/en-us/aspnet/core/security/data-protection/configuration/default-settings?view=aspnetcore-8.0). This checkout does not contain the isolated capture's explicit key-storage configuration.

## 🧪 How the Application Behaves

The Home Page lists all employers.

Jobs can only be created if at least one employer exists.

Applicants are managed at `/Applicants` (not linked in the shared navigation). `ApplicantJobs` describes the schema relationship only; there is no Apply-to-Job interface.

Controllers use a dependency-injected EF Core context and asynchronous queries for most data operations.

The program performs a database connection test on startup; errors are logged rather than stopping startup.

The Jobs list displays employer names; the Job Details view displays `EmployerId`. No sign-in or administrator-role restriction is configured, despite the administrative nature of the forms. Keep this coursework application local rather than exposing it publicly.

## Recorded Demonstration and Limits

The portfolio walkthrough shows real create/list/detail screens with fictional records: two employers, three jobs, two applicants, and no applicant/job links. Its isolated copy used the same CRUD, models, views and migration source, with a capture-only `Program.cs` change for Data Protection key storage. It is recorded evidence, not a live hosted application or proof that an unmodified fresh checkout is fully isolated.

Create, edit, delete and details actions exist in source, but the recorded walkthrough does not establish comprehensive edit/delete, validation, security or cross-platform regression coverage. No automated test project is included in this source revision.
