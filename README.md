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
- Entity Framework Core for **PostgreSQL**
	- Used ValueConverter and Pre-defined conversions
- Using **Startup Data**
	- Load **environment variables**, such as connection string
	- Load **service registeration settings** from **database**, such as JWT settings
- Utilizing **Base Data Service**
	- Retrieve **project settings** stored as JSON values from a dedicated database table
	- Fetch **caching data from various tables**
- **OwnsOne** in EF Core for owning **value objects** such as Price in Product
- **IdGen** to generate Snowflake-based globally unique ulong IDs
- Has **Product, Order, OrderLine**