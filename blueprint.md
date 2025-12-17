
# Blueprint del Proyecto: API .NET 8 con CQRS y Clean Architecture

## 1. Visión General

Este proyecto es una plantilla inicial para desarrollar APIs en .NET 8 siguiendo la arquitectura **CQRS (Command Query Responsibility Segregation)** combinada con los principios de **Clean Architecture** y el patrón **Mediator (MediatR)**.

### Beneficios

*   **Escalabilidad:** Cada caso de uso vive en su propia carpeta, independiente del resto.
*   **Separación clara:** Lecturas (`Queries`) y escrituras (`Command`) no se mezclan.
*   **Mantenibilidad:** Fácil agregar, modificar o eliminar funcionalidades.
*   **Testabilidad:** Puedes probar cada handler sin necesidad de levantar todo el stack.
*   **Extensible:** Permite agregar Event Sourcing, colas (RabbitMQ, Kafka) o microservicios en el futuro.

---

## 2. Tecnologías Utilizadas

*   **.NET 8 Web API**
*   **Entity Framework Core** (con soporte para PostgreSQL)
*   **MediatR** para la mediación de comandos y consultas.
*   **Clean Architecture** para separación de capas.

---

## 3. Estructura del Proyecto

```
src
 ├── Metrics.Api               → Capa de presentación (Controllers, Middlewares)
 │    ├── Controllers
 │    └── Middlewares
 ├── Metrics.Application       → Casos de uso (Commands, Queries, Handlers)
 │    ├── Users
 │    |    ├── Commands
 │    │    |    ├── CreateUserCommand.cs
 │    │    |    ├── UpdateUserCommand.cs
 │    │    |    └── DeleteUserCommand.cs
 │    |    ├── Queries
 │    │    |    ├── GetUserByIdQuery.cs
 │    │    |    └── GetAllUsersQuery.cs
 │    |    ├── Validators
 │    │    |    └── CreateUserCommandValidator.cs
 │    |    ├── DTOs
 │    │    |    └── UserDto.cs
 │    |    └── Mapping
 │    |     └── UserProfile.cs
 |    ├──Projects
 │    |   ├── Commands
 │    |   ├── Queries
 │    |   ├── Validators
 │    |   └── DTOs
 │    └── Behaviors         --> (MediatR: Logging, Validation, etc.)
 ├── Metrics.Domain            → Entidades y lógica de negocio
 │    ├── Entities
 │    ├── Enums
 │    ├── ValueObjects
 │    └── Events
 ├── Metrics.Infrastructure    → Persistencia, Repositorios, EF Core
 │    ├── Persistence
 │    │   ├── Configurations
 │    │   ├── Interfaces
 │    │   ├── Repositories
 │    │   ├── UnitOfWork
 │    │   └── DbContext.cs
 │    ├── Services
 │    ├── Files
 │    └── ExternalApis
 └── Metrics.UnitTest             → Tests unitarios y de integración
      ├── Unit
      └── Integration
```

---

## 4. Flujo de la Arquitectura

1.  **Controller** recibe la solicitud HTTP y envía un `Command` o `Query` al Mediator.
2.  **MediatR** localiza el `Handler` correspondiente y ejecuta la lógica de negocio.
3.  **Repositorio / DbContext** interactúa con la base de datos u otros servicios.
4.  Se retorna una respuesta DTO a la API.

---

## 5. Características Existentes

*   **CreateUserCommand**: Crea un nuevo usuario y lo guarda en base de datos.
*   **GetUserByIdQuery**: Obtiene un usuario por ID.

Ambos casos están implementados usando MediatR y EF Core InMemory.

---

## 6. Plan Actual

_(Esta sección se rellenará con el plan para la próxima tarea)_
