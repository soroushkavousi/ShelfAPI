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
- Entity Framework Core for **PostgreSQL**
    - Used ValueConverter and Pre-defined conversions
- Using **Startup Data**
    - Project only needs database connection string to get startup data from database
- Utilizing **FusionCache** (**Hybrid Cache**)
    - Retrieve **project settings** from hybrid cache
    - Cache items in both distributed cache and memory cache
- **OwnsOne** in EF Core for owning **value objects** such as Price in Product
- **Error Handling** with **Result Pattern**
    - has implicit operator, and deconstruct
    - used in try-create pattern value objects
- Has **Product, Cart, and Payment** models