\# Firebird API Demo



A Web API demonstrating CRUD operations with Firebird SQL database using Docker and .NET 10.



---



\## 🚀 Motivation



Learning Firebird SQL integration with modern .NET applications, focusing on:



\- \*\*Firebird SQL\*\* - Lightweight, powerful open-source relational database

\- \*\*Entity Framework Core 10\*\* - ORM patterns with Firebird provider

\- \*\*Vertical Slice Architecture\*\* - Organizing code by feature rather than technical layers

\- \*\*Result Pattern\*\* - Functional error handling without exceptions



---



\## 🗺️ Project Structure



```

FirebirdDemoApp

├── Infrastructure

│   ├── Data                    # EF Core DbContext

│   ├── Repositories            # Data access implementations

│   └── Services                # Business logic services

├── Interfaces

│   ├── Repositories            # Repository contracts

│   └── Services                # Service contracts

├── Vehicles                    # Feature: Vehicle management

│   └── Domains

│       ├── DTOs                # Request/Response objects

│       ├── Entities            # Domain entities

│       └── Mappings            # Entity-DTO mappings

├── Middlewares                 # Custom middlewares

├── Migrations                  # EF Core migrations

└── Shared                      # Shared utilities (Result pattern, etc.)

```



---



\## 🧰 Tech Stack



\- \*\*.NET 10\*\* - Backend framework

\- \*\*Entity Framework Core 10\*\* - ORM

\- \*\*Firebird SQL 4.0\*\* - Database

\- \*\*FirebirdSql.EntityFrameworkCore.Firebird 13.x\*\* - EF Core provider

\- \*\*Swagger\*\* - API documentation

\- \*\*Docker\*\* - Database containerization



---



\## ⚙️ Setup



\### Prerequisites



\- .NET 10 SDK

\- Docker



\### 1. Start Firebird Database



```bash

docker run `

&nbsp;   --name Firebird `

&nbsp;   -e FIREBIRD\_ROOT\_PASSWORD=\*\*\*\*\* `

&nbsp;   -e FIREBIRD\_USER=yourName `

&nbsp;   -e FIREBIRD\_PASSWORD=\*\*\*\*\* `

&nbsp;   -e FIREBIRD\_DATABASE=app.fdb `

&nbsp;   -e FIREBIRD\_DATABASE\_DEFAULT\_CHARSET=UTF8 `

&nbsp;   -p 3050:3050 `

&nbsp;   -v ./data:/var/lib/firebird/data `

&nbsp;   --detach firebirdsql/firebird

```



\### 2. Configure Connection String



`appsettings.json`:



```json

{

&nbsp; "ConnectionStrings": {

&nbsp;   "DefaultConnection": "User=yourName;Password=\*\*\*\*\*;Database=app.fdb;DataSource=localhost;Port=3050;Dialect=3;Charset=UTF8;"

&nbsp; }

}

```



\### 3. Create and Apply Migrations



```bash

dotnet ef migrations add InitialCreate

dotnet ef database update

```



\### 4. Run the API



```bash

dotnet run

```



Access Swagger: `http://localhost:5258/swagger`



---



\## 📝 API Endpoints



\### Vehicles



\- `GET /api/vehicles` - List all vehicles

\- `GET /api/vehicles/{id}` - Get vehicle by ID

\- `POST /api/vehicles` - Create vehicle

\- `PUT /api/vehicles/{id}` - Update vehicle

\- `DELETE /api/vehicles/{id}` - Delete vehicle



---



\## 🏗️ Architecture



The project uses \*\*By Technical Architecture\*\* pattern, which organizes code by technical concerns within a single project rather than separating into multiple projects like Clean Architecture.



\*\*Key characteristics:\*\*

\- All code in one project for simplicity

\- Features organized by domain (Vehicles)

\- Technical layers (Infrastructure, Interfaces, Domains) grouped together

\- Similar to Vertical Slice but focused on technical organization

\- Faster development without the overhead of multiple projects



This approach is ideal for smaller applications and rapid prototyping while maintaining good separation of concerns.

