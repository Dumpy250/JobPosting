📘 Job Posting MVC Application

A .NET 8 MVC Web Application for Managing Employers, Jobs, and Applicants

📚 Overview


This project is a Job Posting Management System built using ASP.NET Core MVC and Entity Framework Core.
It allows administrators to:

Manage Employers

Create and edit Job Listings

Manage Applicants

Track which Applicants applied to which Jobs (via a many-to-many relationship)

The application demonstrates clean MVC architecture, database-first thinking, CRUD operations, dependency injection, relational modeling, and modern .NET 8 development patterns.

This project was completed as part of a school assignment to reinforce ASP.NET Core MVC fundamentals and relational data modeling.

 🚀 Features

 
✔ Employers

Create, edit, delete employers

View all employers (Home page)

Each employer can post multiple jobs

✔ Jobs

Create jobs assigned to an employer

View jobs with employer names

Edit & delete jobs

Many-to-one relationship (Employer → Jobs)

✔ Applicants

Create, edit, delete applicants

Store applicant name & email

Ready for “Apply to Job” functionality

✔ Many-to-Many: Applicants ↔ Jobs

The application includes a join table ApplicantJobs to support:

Many applicants applying to many jobs

Tracking which applicant applied to which job

Extensible for future features



🔹 Employer

Id

Name

Jobs (ICollection<Job>)

🔹 Job

Id

Title

Description

EmployerId

Employer (navigation)

ApplicantJobs (navigation)

🔹 Applicant

Id

Name

Email

ApplicantJobs (navigation)

🔹 ApplicantJob (Join Table)

ApplicantId

JobId

Navigation to Applicant

Navigation to Job


🧰 Tech Stack
Technology	Purpose

.NET 8	Core application framework

ASP.NET Core MVC	Controllers, routing, views

Entity Framework Core 6 (Pomelo for MySQL)	ORM and database access

MySQL 8	Relational database

Razor Views	UI rendering

Dependency Injection	Service management

C#	Primary development language

⚙️ Installation & Setup

1️⃣ Clone the Project

``git clone <your repo url>
cd JobPosting
``

2️⃣ Configure Database Connection

Edit appsettings.json:

``"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=JobPostingDb;User=<username>;Password=<password>;"
}
``

3️⃣ Install Entity Framework CLI (if needed)

``dotnet tool install --global dotnet-ef
``

4️⃣ Apply Migrations

``dotnet ef database update
``

This will:

Create the JobPostingDb database

Create all tables (Employers, Jobs, Applicants, ApplicantJobs)

5️⃣ Run the Application

``dotnet run
``

The app will be available at:
http://localhost:5073/

🧪 How the Application Behaves

The Home Page lists all employers.

Jobs can only be created if at least one employer exists.

Applicants are independent entities but can be linked to jobs through ApplicantJobs.

Controllers use EF Core with async queries and dependency injection.

The program performs a database connection test on startup.
