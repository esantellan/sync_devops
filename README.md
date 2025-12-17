# Proyecto Base .NET 8 - CQRS + Clean Architecture

Este proyecto es una plantilla inicial para desarrollar APIs en .NET 8 siguiendo la arquitectura **CQRS (Command Query Responsibility Segregation)** combinada con los principios de **Clean Architecture** y el patrón **Mediator (MediatR)**.

---

## Beneficios

Con respecto a la clásica arquitectura en capas (Controllers → Services → Repositories), que funciona pero tiende a crecer desordenadamente, mezclar responsabilidades y ser difícil de mantener a largo plazo. Una arquitectura CQRS puede traer grandes beneficios:

1. **Escalabilidad:** Cada caso de uso vive en su propia carpeta, independiente del resto.
2. **Separación clara:** Lecturas `Queries` y escrituras `Command` no se mezclan.
3. **Mantenibilidad:** Fácil agregar, modificar o eliminar funcionalidades.
4. **Testabilidad:** Puedes probar cada handler sin necesidad de levantar todo el stack.
5. **Extensible:** Permite agregar Event Sourcing, colas (RabbitMQ, Kafka) o microservicios en el futuro.

---

## Tecnologías usadas
- **.NET 8 Web API**
- **Entity Framework Core** (con soporte para PostgreSQL)
- **MediatR** para la mediación de comandos y consultas.
- **Clean Architecture** para separación de capas.

---

## Estructura del proyecto
```
src
 ├── CQRS.Api               → Capa de presentación (Controllers, Middlewares)
 │    ├── Controllers
 │    └── Middlewares
 ├── CQRS.Application       → Casos de uso (Commands, Queries, Handlers)
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
 ├── CQRS.Domain            → Entidades y lógica de negocio
 │    ├── Entities
 │    ├── Enums
 │    ├── ValueObjects
 │    └── Events
 ├── CQRS.Infrastructure    → Persistencia, Repositorios, EF Core
 │    ├── Persistence
 │    │   ├── Configurations
 │    │   ├── Interfaces
 │    │   ├── Repositories
 │    │   ├── UnitOfWork
 │    │   └── DbContext.cs
 │    ├── Services
 │    ├── Files
 │    └── ExternalApis
 └── CQRS.UnitTest             → Tests unitarios y de integración
      ├── Unit
      └── Integration
```

---

## Flujo de la arquitectura
1. **Controller** recibe la solicitud HTTP y envía un `Command` o `Query` al Mediator.
2. **MediatR** localiza el `Handler` correspondiente y ejecuta la lógica de negocio.
3. **Repositorio / DbContext** interactúa con la base de datos u otros servicios.
4. Se retorna una respuesta DTO a la API.

---

## Ejemplos incluidos
- **CreateUserCommand**: Crea un nuevo usuario y lo guarda en base de datos.
- **GetUserByIdQuery**: Obtiene un usuario por ID.

Ambos casos están implementados usando MediatR y EF Core InMemory.

---

## Ejecución del proyecto
### 1. Restaurar paquetes NuGet
```
dotnet restore
```

### 2. Compilar solución
```
dotnet build
```

### 3. Ejecutar API
```
dotnet run --project CQRS.Api
```

La API estará disponible en `https://localhost:5001` y `http://localhost:5000` con Swagger habilitado.

---

## Extender con nuevos casos de uso
Para agregar una nueva funcionalidad:
1. Crear un **Command** (si modifica datos) o **Query** (si solo consulta datos) en la capa `Application`.
2. Implementar su **Handler** con la lógica necesaria.
3. Crear un endpoint en `Controllers` que envíe la solicitud a MediatR.
4. Crear validaciones y tests unitarios según corresponda.

---

Con esta plantilla tendrás una base sólida para desarrollar APIs escalables, mantenibles y fáciles de probar bajo .NET 8.
