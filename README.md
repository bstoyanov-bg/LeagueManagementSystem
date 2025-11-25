# ⚽ League Management System – .NET Framework 4.8 Web API

**A football league management system that provides:**
- Team rankings
- Played match results
- CRUD for Teams
- CRUD for Matches (completed matches only)
- Automatic ranking updates after each match
- Scoring system: Win = 3 pts, Draw = 1 pt, Loss = 0 pts

**The project follows a clean layered architecture:**
- Domain Layer – Entities, domain logic
- Application Layer – Services, DTOs, interfaces
- Infrastructure Layer – EF6, repositories, Unit of Work, scoring strategies
- API Layer – Controllers, Swagger, filters, DI
- Patterns used: Repository, Unit of Work, Strategy, SOLID

## 📁 Project Structure

```
LeagueApi/
│
├── Api/                           
│   ├── Controllers/
│   │   ├─ MatchesController.cs
│   │   ├─ RankingsController.cs
│   │   ├─ SwaggerRedirectController.cs
│   │   └─ TeamsController.cs
│   │
│   ├── DTOs/
│   │   ├─ MatchCreateDto.cs
│   │   ├─ MatchUpdateDto.cs
│   │   ├─ TeamCreateDto.cs
│   │   ├─ TeamRankingDto.cs
│   │   └─ TeamUpdateDto.cs
│   │
│   └── Filters/
│       └─ GlobalExceptionFilter.cs
│
├── App_Start/
│   ├─ AutofacConfig.cs
│   ├─ FilterConfig.cs
│   ├─ RouteConfig.cs
│   ├─ SwaggerConfig.cs
│   └─ WebApiConfig.cs
│
├── Application/
│   ├── Interfaces/
│   │   └─ IRankingService.cs
│   └── Services/
│       └─ RankingService.cs
│
├── Domain/
│   ├── Entities/
│   │   ├─ Match.cs
│   │   └─ Team.cs
│   │
│   └── Scoring/
│       ├─ IScoringStrategy.cs
│       └─ StandardScoringStrategy.cs
│
├── Infrastructure/
│   ├── Data/
│   │   ├─ Migrations/
│   │   ├─ AppDbContext.cs
│   │   └─ SeedData.cs
│   │
│   └── Repositories/
│       ├─ IRepository.cs
│       ├─ Repository.cs
│       ├─ IUnitOfWork.cs
│       └─ UnitOfWork.cs
│
├── Global.asax
├── Web.config
└── README.md
```

## 🚀 Features
**Team Management API**
- GET /api/teams – list all teams
- GET /api/teams/{id} – get a team
- POST /api/teams – create team
- PUT /api/teams/{id} – update team
- DELETE /api/teams/{id} – delete team

**Match Management API**
- GET /api/matches – list played matches
- GET /api/matches/{id} – get match
- POST /api/matches – add played match (requires scores)
- PUT /api/matches/{id} – update result
- DELETE /api/matches/{id} – delete match

**Rankings API**
- GET /api/rankings – calculates standings live

## 🧩 Design Patterns Used
- Repository Pattern – abstracts EF data access
- Unit of Work Pattern – manages EF transactions
- Strategy Pattern – scoring logic (extendable for different leagues)
- Dependency Injection (Autofac)
- Global Exception Handling

## ⚙️ Setup Instructions
1. Clone the repository
- git clone https://github.com/bstoyanov-bg/LeagueManagementSystem.git

2. Open the solution
- Open LeagueApi.sln in Visual Studio 2019/2022.

3. Restore NuGet packages
- Open Package Manager Console -> Update-Package -reinstall

4. Configure the database
- Open Web.config and update the connection string:

<connectionStrings>
  <add name="DefaultConnection"
       connectionString="Server=YOUR_SERVER;Database=LeagueDb;User Id=USER;Password=PASS;TrustServerCertificate=True;"
       providerName="System.Data.SqlClient" />
</connectionStrings>

5. Run the application

✔️ Database is created automatically
✔️ Migrations are applied
✔️ Seed data is inserted if missing

The app opens Swagger UI automatically:

http://localhost:{PORT}/swagger

6. Verify database creation and seeded rows via SQL Server Management Studio (SSMS)

7. Test API Endpoints via Swagger UI:
- Teams → /api/teams
- Matches → /api/matches
- Rankings → /api/rankings