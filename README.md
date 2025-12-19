# 🏋️ Gym Management System

[![.NET](https://img.shields.io/badge/.NET-10-blue?logo=dotnet)](https://dotnet.microsoft.com/en-us/) 
[![C#](https://img.shields.io/badge/C%23-Modern-green?logo=c-sharp)](https://learn.microsoft.com/en-us/dotnet/csharp/)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-Ready-red?logo=microsoftsqlserver)](https://www.microsoft.com/en-us/sql-server)
[![License](https://img.shields.io/badge/License-MIT-yellow)](LICENSE)

A modern, scalable Gym Management System built with **.NET 10**, following **Onion Architecture** principles. Designed for small to medium gyms to manage members, trainers, sessions, bookings, and memberships efficiently.  

---

## 🚀 Features (MVP)

### 👤 Member Management
* Add, edit, deactivate members  
* Track membership start & end dates  
* Membership status: Active / Expired  
* Basic profile info: Name, Phone, Email  

### 🏋️ Trainer Management
* Add trainers  
* Assign trainers to sessions  
* Manage availability  

### 📅 Sessions & Bookings
* Create sessions (date, time, capacity)  
* Assign trainers  
* Member booking & cancellation  
* Prevent overbooking  

### 💳 Membership Plans
* Monthly / Yearly plans  
* Max sessions per plan  
* Price management  

### 🔐 Authentication & Roles
* JWT authentication  
* Role-based access: Admin, Trainer, Member  

### 📊 Reports
* Active members count  
* Sessions per day  
* Bookings per session  

---

## 🛠 Tech Stack

* **Backend:** .NET 10, ASP.NET Core Web API  
* **Architecture:** Onion Architecture (Domain → Application → Infrastructure → API)  
* **Database:** SQL Server, EF Core (Code-First), Fluent API  
* **Mapping:** AutoMapper  
* **Validation:** FluentValidation  
* **Auth:** ASP.NET Core Identity, JWT  
* **Logging:** ILogger, centralized middleware  
* **Docs:** Swagger / OpenAPI  

---

## 📂 Project Structure
```
GymManagement/
├─ Gym.Domain/ # Entities, Enums, BaseEntity
├─ Gym.Application/ # Interfaces, DTOs, Services, AutoMapper profiles
├─ Gym.Infrastructure/ # DbContext, Configurations, Repositories, UnitOfWork
└─ Gym.API/ # Controllers, DI, JWT, Swagger, Program.cs
```
---

## ⚡ Quick Start

### 1️⃣ Clone & Restore
1. Clone the repository and navigate into it:
```bash
git clone https://github.com/MonaSaeedDev/GymManagementSystem.git
cd GymManagement
dotnet restore
dotnet build
```

### 2️⃣ Database Setup

Add SQL Server connection in Gym.API/appsettings.json

Register GymDbContext in Program.cs

Create initial migration:
```bash
dotnet ef migrations add InitialCreate -s Gym.API -p Gym.Infrastructure
```

### 3️⃣ Run API
```bash
dotnet run --project Gym.API
```

Access Swagger UI: `https://localhost:5001/swagger`  
> Replace the port if your terminal shows a different one.


## 🧩 Authentication & Roles

1. Secure endpoints using `[Authorize(Roles = "RoleName")]`
2. Default roles: `Admin`, `Trainer`, `Member`
3. JWT tokens for authentication

## ✨ Highlights

* Clean **Onion Architecture**, scalable & maintainable
* EF Core **Repository + Unit of Work** pattern
* Centralized logging & global error handling
* Ready for production MVP, easy to extend

## 🤝 Contributing

Fork → branch → commit → push → pull request

## 📜 License

MIT License


This version is now:  

* Properly **indented & readable**  
* **Sections clearly separated**  
* Markdown-friendly for **GitHub display**  
* **Concise yet informative** for recruiters  

If you want, I can **also add a “Project Snapshot” diagram with emojis** showing the workflow visually—it’ll make your README **stand out even more**.  

Do you want me to do that?