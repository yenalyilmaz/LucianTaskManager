LucianTaskManager API

Project Purpose
LucianTaskManager is a simple yet scalable REST API built with .NET 8, designed to manage personal tasks with secure authentication.
It features user registration, login with JWT authentication, task CRUD operations, and admin-level management endpoints.

Perfect as a freelance portfolio project to showcase backend development and API architecture skills.

Technologies Used
- .NET 8 Web API
- Entity Framework Core
- PostgreSQL
- JWT Authentication (with Role-based Access Control)
- Swagger / OpenAPI
- Docker (optional for database)

Getting Started

Prerequisites
- .NET 8 SDK
- PostgreSQL database

Setup
Clone the repository:
git clone https://github.com/your-username/LucianTaskManager.git
cd LucianTaskManager


Configure appsettings.json:
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=LucianTaskDb;Username=postgres;Password=yourpassword"
},
"Jwt": {
  "Key": "YourSuperSecretKeyHereAtLeast32Chars",
  "Issuer": "LucianTaskManager",
  "Audience": "LucianTaskManagerUsers"
}


Apply database migrations:
dotnet ef database update


Run the application:
dotnet run

Access Swagger UI:
https://localhost:5001/swagger/index.html


API Endpoints

Authentication
POST /auth/register → Register a new user
POST /auth/login → Login and receive a JWT token


User Task Management
GET /tasks → List user's own tasks
POST /tasks → Create a new task
PUT /tasks/{id} → Update a user's own task
DELETE /tasks/{id} → Delete a user's own task

Admin Management
GET /admin/users → List all users
GET /admin/tasks → List all tasks across all users


📦 Deployment Notes
The application can be containerized using Docker for production.

Environment variables should be used for sensitive settings (connection strings, JWT keys).
