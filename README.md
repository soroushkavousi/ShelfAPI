# Shelf Api (E-Commerce sample of ASP.NET Core Web API)

[![.NET Version](https://img.shields.io/badge/.net_version-v8.0-4CAF50?logo=.net)](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-8/overview)

## Features

- Has **User, Role, Product, Cart, Payment, ProjectSetting, and Error** Models
- **Clean Architecture**
- **Domain-Driven Design (DDD)**
- **CQRS with MediatR**
- **ASP.NET Core Identity**
- **JWT Authentication**
- **Role-Based Authorization** (Includes admin and user routes)
- **Serilog Logging with Elasticsearch Sink**
- **Entity Framework Core with PostgreSQL**
- Leverages **Elasticsearch** as a **read database for complex search queries**
- **Supports Pagination and Sorting for both Elasticsearch and PostgreSQL queries**
- **Domain Events with Outbox Pattern**
    - Reliable event processing with transactional consistency
    - Publish with MediatR in a background service
    - Automatic retries for failed events
- Uses **RabbitMQ** with **MassTransit** to publish **integration events**
- **FusionCache (Hybrid Caching)**
    - Stores cached items in both distributed cache (Redis) and memory cache.
    - Retrieves project settings using a hybrid cache mechanism.
- **Uses EF Core Value Converters and Predefined Conversions to support value-objects**
- Uses lightweight **expressions** and **extension methods** for **mapping between domain models, DTOs, and views**
    - Avoids external mapping libraries such as AutoMapper for better control and performance
- **Application-Side ID Generation**
    - Implements IdGen library for generating IDs at the application side
    - Database-independent ID generation
    - Combines timestamp (42 bits), generator ID (10 bits), and sequence (11 bits)
    - Enables ID generation before database persistence
    - Reduces database round trips and improves performance
- **Error Handling with the Result Pattern**
    - Supports implicit operators and deconstruction for better error handling
    - Utilized in the Try-Create pattern for value objects
- **Startup Data Support**
    - Requires only a database connection string to initialize startup data
- Uses **Docker** for infrastructure
    - docker-compose files exist in the docker directory