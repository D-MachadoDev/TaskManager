# TaskManager - Arquitectura y capas

Este proyecto usa una arquitectura por capas (no MVC con vistas).

## Capas actuales

- **TaskManager.Api**
  - Expone endpoints HTTP (controladores).
  - Es la puerta de entrada del sistema para clientes externos.
- **TaskManager.Application**
  - Contiene casos de uso/reglas de aplicación (`TaskService`).
  - Orquesta validaciones y flujo de negocio.
- **TaskManager.Domain**
  - Contiene entidades del negocio (`TaskEntity`, `UserEntity`).
- **TaskManager.Infrastructure**
  - Implementa acceso a datos (`TaskRepository`, `AppDbContext`).
  - Se comunica con SQL Server mediante EF Core.

## ¿Qué significa "exterior"?

"Exterior" significa cualquier sistema fuera del backend:

- Frontend web (por ejemplo `fetch`/Axios)
- App móvil
- Postman/Insomnia
- Otro backend

Todos estos clientes consumen la API por URL + verbo HTTP.

## Flujo de una operación

1. Cliente llama un endpoint (ej. `GET /api/tasks/{userId}/{taskId}`)
2. `TaskController` recibe la petición
3. `TaskService` valida y aplica reglas
4. `TaskRepository` consulta/actualiza la base de datos
5. La API responde con código HTTP + JSON

## Operaciones implementadas en `TaskController`

- `GET /api/tasks/{userId}/{taskId}`: obtener tarea por usuario y tarea
- `GET /api/tasks`: listar todas las tareas
- `GET /api/tasks/user/{userId}`: listar tareas de un usuario
- `POST /api/tasks`: crear tarea
- `PATCH /api/tasks/{taskId}/complete?userId={id}`: completar tarea
- `DELETE /api/tasks/{taskId}?userId={id}`: eliminar tarea

> En este proyecto no hay vistas Razor, así que **sí hay patrón MVC**, pero usando solo **Controller + Model** (sin View), típico de ASP.NET Core Web API.
