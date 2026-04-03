# MyShop API

MyShop is an ASP.NET Core Web API project that implements a simple online store backend. It uses Entity Framework Core with SQL Server, a layered architecture with repositories and services, and seeding support for initial data.

## Key Features

- RESTful API endpoints for products, orders, order items, and items
- Clean separation of concerns using:
  - Entities
  - DTOs
  - Repositories
  - Services
  - Controllers
- Database seeding for sample data on startup
- Swagger/OpenAPI support for API exploration in development
- EF Core migrations and automatic database migration on run

## Project Structure

- `Controllers/` - API controllers
- `Data/` - EF Core `AppDbContext`
- `Entities/` - domain models
- `DTO/` - request/response models
- `Repositories/` - data access layer and interfaces
- `Services/` - business logic layer and interfaces
- `Seeders/` - initial data population
- `Configurations/` - entity configuration classes

## Getting Started

1. Set the SQL Server connection string in `appsettings.json` under `ConnectionStrings:DefaultConnection`.
2. Build and run the application with:

```bash
cd MyShop
dotnet run
```

3. In development, Swagger is available at:

```
https://localhost:<port>/swagger
```

## Notes

- The application automatically applies pending migrations and seeds the database when it starts.
- The `ProductsController` supports basic CRUD operations and search by name or category.

## Requirements

- .NET 10 SDK
- SQL Server accessible from the application

## License

This project is provided as a sample application and does not include a license file.
