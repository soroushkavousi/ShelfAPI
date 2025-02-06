# Shelf Api (E-Commerce sample of ASP.NET Core Web API)

[![.NET Version](https://img.shields.io/badge/.net_version-v8.0-4CAF50?logo=.net)](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-8/overview)

## Features

- **Clean Architecture**
- **Domain-Driven Design (DDD)**
- **CQRS with MediatR**
- **ASP.NET Core Identity**
- **JWT Authentication**
- **Role-Based Authorization** (Includes admin and user routes)
- **Serilog Logging with Seq Sink**
- **Entity Framework Core with PostgreSQL**
- **Value Converters and Predefined Conversions**
- **FusionCache (Hybrid Caching)**
  - Retrieves project settings using a hybrid cache mechanism.
  - Stores cached items in both distributed cache and memory cache.
- **Startup Data Support**
  - The project only requires a database connection string to retrieve startup data from the database.
- **OwnsOne in EF Core**
  - Used for owning value objects, such as `Price` in `Product`.
- **Error Handling with the Result Pattern**
  - Supports implicit operators and deconstruction.
  - Utilized in the Try-Create pattern for value objects.
- **Includes Product, Cart, and Payment Models**
