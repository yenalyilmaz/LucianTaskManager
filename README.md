
# LucianTaskManager API

---

## Project Purpose

LucianTaskManager is a simple yet scalable REST API built with .NET 8, designed to manage personal tasks with secure authentication.  
It features:

- User registration and login with JWT authentication
- Role-based access control (User and Admin)
- Task CRUD operations
- Admin-level management endpoints

Perfect for showcasing backend development and API architecture skills in freelance portfolios.

---

## Technologies Used

- .NET 8 Web API
- Entity Framework Core
- PostgreSQL
- JWT Authentication (Role-Based)
- Swagger / OpenAPI
- Docker (optional, for database)

---

## Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [PostgreSQL](https://www.postgresql.org/)

---

### Setup

1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/LucianTaskManager.git
   cd LucianTaskManager
   ```

2. Configure `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Port=5432;Database=LucianTaskDb;Username=postgres;Password=yourpassword"
     },
     "Jwt": {
       "Key": "YourSuperSecretKeyHereAtLeast32Chars",
       "Issuer": "LucianTaskManager",
       "Audience": "LucianTaskManagerUsers"
     }
   }
   ```

3. Apply database migrations:
   ```bash
   dotnet ef database update
   ```

4. Run the application:
   ```bash
   dotnet run
   ```

5. Access Swagger UI:
   ```
   https://localhost:5001/swagger/index.html
   ```

---

## API Endpoints

### Authentication
- POST `/auth/register` → Register a new user
- POST `/auth/login` → Login and receive a JWT token

### User Task Management
- GET `/tasks` → List user's own tasks
- POST `/tasks` → Create a new task
- PUT `/tasks/{id}` → Update a user's own task
- DELETE `/tasks/{id}` → Delete a user's own task

### Admin Management
- GET `/admin/users` → List all users
- GET `/admin/tasks` → List all tasks across all users

---

## Deployment Notes

- Docker can be used to containerize the database and application for production environments.
- Important: Use environment variables for sensitive data like database credentials and JWT secret keys in production.

---

## Status

Ready to deploy and showcase as a freelance portfolio project.

---
