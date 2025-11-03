# NewsProject Backend API

REST API для новостного сайта на ASP.NET Core 8.0.

##  Архитектура

Проект следует **Layered Architecture (N-Tier Architecture)** с разделением на:

- **Controllers** - HTTP endpoints
- **Services** - бизнес-логика
- **Repositories** - доступ к данным


##  Быстрый старт

1. Установите .NET 8.0 SDK и PostgreSQL
2. Настройте строку подключения в `appsettings.json`
3. Запустите:
```bash
dotnet restore
dotnet run
```
База данных создается автоматически!

##  Аутентификация

Cookie-based через ASP.NET Core Identity.

##  Документация

Swagger UI: `https://localhost:7043/swagger`

##  Технологии

- .NET 8.0
- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL
- ASP.NET Core Identity
- Swagger/OpenAPI
- Dependency Injection
