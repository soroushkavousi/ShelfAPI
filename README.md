# Shelf Api (E-Commerce sample of ASP.NET Core Web API)

[![.NET Version](https://img.shields.io/badge/.net_version-v8.0-4CAF50?logo=.net)](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-8/overview)

### Features

- **Clean Architecture**
- **Domain-Driven Design (DDD)**
- **CQRS** with **MediatR**
- **ASP.NET Core Identity**
- **JWT**
- **Role Based Authorization**, has admin and user routes
- **Serilog** for logging with **Seq** sink
- **Mapster** for mapping Models to DTOs
- Using **environment variables** for loading connection string
- Storing **project configurations in a database table** with JSON format
- Entity Framework Core for **SQL Server**
- **OwnsOne** in EF Core for owning **value objects** such as Price in Product
- **IdGen** to generate Snowflake-based globally unique ulong IDs
- Has **Product, Order, OrderLine**