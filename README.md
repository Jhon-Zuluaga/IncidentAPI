# IncidentAPI

Sistema de gestión de incidentes técnicos desarrollado con **ASP.NET Core 10** y **PostgreSQL**, especializado en el registro, seguimiento y resolución de incidentes técnicos con soporte para usuarios, categorías, comentarios, **archivos adjuntos**, **autenticación JWT** y **notificaciones por correo**.

---

## Requisitos

- .NET 10 SDK
- Docker y Docker Compose (para PostgreSQL)
- Git
- VS Code / Visual Studio
- Extensión C# Dev Kit (VS Code)
- Node.js 18+ *(para el frontend React)*

---

## Instalación

### Backend

```bash
# 1. Clonar el repositorio
git clone https://github.com/Jhon-Zuluaga/IncidentAPI.git
cd IncidentAPI

# 2. Levantar PostgreSQL con Docker
docker-compose up -d

# 3. Restaurar dependencias
dotnet restore

# 4. Instalar herramienta de Entity Framework Core
dotnet tool install --global dotnet-ef

# 5. Configurar la clave secreta JWT (fuera del repositorio)
cd IncidentAPI.Api
dotnet user-secrets init
dotnet user-secrets set "Jwt:Key" "TuClaveSecreta12345!"

# 6. Configurar credenciales de correo Gmail
dotnet user-secrets set "Email:Username" "tucorreo@gmail.com"
dotnet user-secrets set "Email:Password" "abcd efgh ijkl mnop"

# 7. Aplicar migraciones y crear la base de datos
dotnet ef database update

# 8. Correr el proyecto
dotnet run
```

> Las migraciones se aplican automáticamente al iniciar la aplicación.

### Docker (producción)

```bash
docker-compose up -d
```

Esto levanta PostgreSQL (puerto 5433) y la API.

### Acceder a Swagger

```
http://localhost:5230/swagger
```

### Frontend React

Repositorio separado: https://github.com/Jhon-Zuluaga/IncidentAPI-frontend.git

```bash
cd frontend
npm install
npm run dev
```

---

## Características

### Gestión de Incidentes
- CRUD completo de incidentes técnicos
- Control de estado: `abierto`, `en_progreso`, `cerrado`
- Asociación de incidentes a usuarios y categorías
- Notificación automática por correo al crear un incidente

### Archivos Adjuntos
- Carga de archivos (imágenes, documentos) asociados a incidentes
- Validación de tipo MIME y tamaño máximo (5MB)
- Almacenamiento en `wwwroot/uploads/`
- Eliminación de archivo al eliminar el incidente (cascade delete)

### Comentarios en Incidentes
- CRUD completo de comentarios por incidente
- Cada comentario registra autor y fecha

### Gestión de Usuarios
- CRUD completo de usuarios
- Validación de email
- Contraseña hasheada con BCrypt (nunca en texto plano)

### Gestión de Categorías
- CRUD completo de categorías
- Seed data inicial: Hardware, Software, Red

### Autenticación JWT con Cookies
- Login y logout seguros
- Token JWT almacenado en cookie `HttpOnly` (protección XSS)
- `SameSite=Strict` para mitigar CSRF
- Expiración configurable (por defecto 60 minutos)
- Endpoints protegidos con `[Authorize]`
- Registro de usuarios disponible sin autenticación (`[AllowAnonymous]`)

### Notificaciones por Correo SMTP
- Notificación automática al usuario al crear un incidente
- Recuperación de contraseña con token de 6 dígitos
- Token de recuperación con expiración de 15 minutos
- Implementado con `System.Net.Mail` (SMTP nativo de .NET) vía Gmail

### Middleware Global de Errores
- Captura de excepciones no controladas
- Respuesta JSON estandarizada con `status`, `message` y `detail`
- Registrado antes de los controllers para cubrir todo el pipeline

### Documentación con Swagger
- Documentación automática e interactiva de todos los endpoints
- Accesible en `/swagger`

### Logging con Serilog
- Logs estructurados en consola
- Logs guardados en archivos con rotación diaria en `Logs/`

### Pruebas Unitarias
- 5 pruebas con xUnit y Moq
- Camino feliz y casos de error cubiertos

---

## Arquitectura

El proyecto sigue un patrón **Layered Monolith** con separación por capas:

```
Controllers → Services → Repositories → EF Core → PostgreSQL
```

### Estructura del proyecto

```
IncidentAPI/
├── IncidentAPI.Api/
│   ├── Controllers/          → Endpoints HTTP (CRUD + Auth)
│   ├── Models/               → Entidades de la base de datos
│   ├── DTOs/                 → Objetos de transferencia de datos
│   │   ├── Attachment/
│   │   ├── Auth/
│   │   ├── Category/
│   │   ├── Comment/
│   │   ├── Incident/
│   │   └── User/
│   ├── Data/                 → AppDbContext (EF Core)
│   ├── Repositories/         → Acceso a la base de datos
│   │   ├── Interfaces/
│   │   └── Implementations/
│   ├── Services/             → Lógica de negocio
│   │   ├── Interfaces/
│   │   └── Implementations/
│   ├── Middleware/            → Manejo global de errores
│   ├── Migrations/           → Migraciones de EF Core
│   └── Program.cs            → Composition root (DI + config)
├── IncidentAPI.Tests/        → Pruebas unitarias
├── Dockerfile                → Build multi-stage
└── docker-compose.yml        → Orchestration PostgreSQL + API
```

### Patrones de diseño aplicados

| Patrón | Estado | Detalle |
|--------|--------|---------|
| **Repository** | Implementado | Interfaz + implementación por cada entidad |
| **Service Layer** | Implementado | Lógica de negocio, validaciones, mapeo de DTOs |
| **DTO Pattern** | Implementado | 3 variantes por entidad: `ReadDto`, `CreateDto`, `UpdateDto` |
| **Middleware** | Implementado | `ErrorHandlingMiddleware` - manejo global de errores |
| **Dependency Injection** | Implementado | Composition root centralizado en `Program.cs` |

### Modelo de entidades

```
User (1) ──── (N) Incident (N) ──── (1) Category
                     │
                     ├── (N) Comment
                     └── (N) Attachment (cascade delete)
```

### Flujo de petición

```
Cliente HTTP / React
        ↓
   LoginPage  →  POST /api/Auth/login  →  Cookie JWT (HttpOnly)
        ↓
   ErrorHandlingMiddleware (try/catch global)
        ↓
   JWT Authentication (cookie-based)
        ↓
   Controller [Authorize]  →  Service  →  Repository  →  EF Core  →  PostgreSQL
```

---

## Endpoints de la API

### Auth

| Método | Ruta | Auth | Descripción |
|--------|------|------|-------------|
| POST | `/api/Auth/login` | Público | Inicia sesión y setea la cookie JWT |
| POST | `/api/Auth/logout` | Requiere token | Cierra sesión y elimina la cookie |
| POST | `/api/Auth/forgot-password` | Público | Envía token de recuperación por correo |
| POST | `/api/Auth/reset-password` | Público | Cambia la contraseña con el token recibido |

### Incidents

| Método | Ruta | Descripción |
|--------|------|-------------|
| GET | `/api/Incident` | Obtiene todos los incidentes |
| GET | `/api/Incident/{id}` | Obtiene un incidente por Id |
| POST | `/api/Incident` | Crea un nuevo incidente |
| PUT | `/api/Incident/{id}` | Actualiza un incidente |
| DELETE | `/api/Incident/{id}` | Elimina un incidente |

### Comments

| Método | Ruta | Descripción |
|--------|------|-------------|
| GET | `/api/Comment/incident/{incidentId}` | Obtiene comentarios de un incidente |
| GET | `/api/Comment/{id}` | Obtiene un comentario por Id |
| POST | `/api/Comment` | Crea un nuevo comentario |
| PUT | `/api/Comment/{id}` | Actualiza un comentario |
| DELETE | `/api/Comment/{id}` | Elimina un comentario |

### Attachments

| Método | Ruta | Descripción |
|--------|------|-------------|
| GET | `/api/Attachment/incident/{incidentId}` | Obtiene adjuntos de un incidente |
| GET | `/api/Attachment/{id}` | Obtiene un adjunto por Id |
| POST | `/api/Attachment` | Sube un nuevo adjunto (multipart/form-data) |
| DELETE | `/api/Attachment/{id}` | Elimina un adjunto |

### Users

| Método | Ruta | Auth | Descripción |
|--------|------|------|-------------|
| GET | `/api/User` | Requiere token | Obtiene todos los usuarios |
| GET | `/api/User/{id}` | Requiere token | Obtiene un usuario por Id |
| POST | `/api/User` | Público | Crea un nuevo usuario (registro) |
| PUT | `/api/User/{id}` | Requiere token | Actualiza un usuario |
| DELETE | `/api/User/{id}` | Requiere token | Elimina un usuario |

### Categories

| Método | Ruta | Descripción |
|--------|------|-------------|
| GET | `/api/Category` | Obtiene todas las categorías |
| GET | `/api/Category/{id}` | Obtiene una categoría por Id |
| POST | `/api/Category` | Crea una nueva categoría |
| PUT | `/api/Category/{id}` | Actualiza una categoría |
| DELETE | `/api/Category/{id}` | Elimina una categoría |

> Todos los endpoints excepto `/api/Auth/login`, `/api/Auth/forgot-password`, `/api/Auth/reset-password` y `POST /api/User` requieren autenticación JWT.

---

## Autenticación JWT

El sistema utiliza **JWT almacenado en una cookie HttpOnly** para proteger contra ataques XSS.

### Flujo completo

```
1. Usuario hace POST /api/Auth/login con { email, password }
2. API valida credenciales con BCrypt
3. API genera JWT y lo setea en cookie HttpOnly
4. El navegador envía la cookie automáticamente en cada petición
5. API valida el JWT en cada request protegido
6. Al expirar (60 min) → API devuelve 401 → React redirige al login
7. POST /api/Auth/logout → cookie eliminada → sesión cerrada
```

### Configuración en `appsettings.json`

```json
"Jwt": {
  "Issuer": "IncidentAPI",
  "Audience": "IncidentAPIUsers",
  "ExpirationMinutes": 60
}
```

> La `Jwt:Key` **nunca** va en `appsettings.json`. Se configura con `dotnet user-secrets`.

### Seguridad aplicada

| Medida | Descripción |
|--------|-------------|
| `HttpOnly = true` | JavaScript no puede leer la cookie → protege de XSS |
| `SameSite = Strict` | Mitiga ataques CSRF |
| `Secure = true` | Solo HTTPS en producción |
| `ClockSkew = Zero` | El token expira exactamente en el tiempo configurado |
| BCrypt | Las contraseñas nunca se guardan en texto plano |
| CORS restringido | Solo `localhost:5173` con credenciales |

---

## Notificaciones por Correo SMTP

El sistema envía correos automáticamente usando `System.Net.Mail` (SMTP nativo de .NET) con Gmail como proveedor.

### Funcionalidades

- **Notificación de incidente:** al crear un incidente se envía un correo al usuario con el título del mismo.
- **Recuperación de contraseña:** el usuario solicita un token de 6 dígitos que se envía a su correo y expira en 15 minutos.

### Configuración en `appsettings.json`

```json
"Email": {
  "From": "tucorreo@gmail.com",
  "DisplayName": "IncidentAPI",
  "Host": "smtp.gmail.com",
  "Port": 587
}
```

> El `Email:Username` y `Email:Password` se configuran con `dotnet user-secrets`. No usar la contraseña normal de Gmail — generar una **contraseña de aplicación** de 16 caracteres en: *Cuenta Google → Seguridad → Verificación en dos pasos → Contraseñas de aplicación*.

### Flujo de recuperación de contraseña

```
1. POST /api/Auth/forgot-password  { email }
   → API genera token de 6 dígitos con expiración de 15 min
   → Envía correo con el token
   → Siempre responde Ok (no revela si el email existe)

2. POST /api/Auth/reset-password  { email, token, newPassword }
   → API valida token y expiración
   → Hashea la nueva contraseña con BCrypt
   → Limpia el token de la base de datos
```

---

## Frontend React

La aplicación React consume la API usando **cookies JWT** automáticamente.

Repositorio: https://github.com/Jhon-Zuluaga/IncidentAPI-frontend.git

### Puntos clave de la integración

- Todas las peticiones incluyen `credentials: 'include'` para enviar la cookie automáticamente.
- Si la API devuelve `401`, React detecta el token expirado y redirige al login.
- El login exitoso setea la cookie en el navegador — React no maneja el token directamente.

---

## Pruebas unitarias

```bash
cd IncidentAPI.Tests
dotnet test
```

Las pruebas cubren:

- `CreateAsync_ValidData` — crear un incidente con datos válidos *(camino feliz)*
- `UpdateAsync_IncidentNotFound` — actualizar un incidente que no existe *(caso de error)*
- `UpdateAsync_InvalidStatus` — actualizar con un estado inválido *(caso de error)*
- `GetByIdAsync_ExistingId` — obtener un incidente existente *(camino feliz)*
- `GetByIdAsync_NotExistingId` — obtener un incidente que no existe *(caso de error)*

---

## Construido con

- **ASP.NET Core 10** — Framework principal
- **Entity Framework Core** — ORM con Code-First migrations
- **PostgreSQL** — Base de datos (via Docker)
- **Swagger / OpenAPI** — Documentación
- **Serilog** — Logging estructurado
- **JWT (JwtBearer)** — Autenticación basada en cookies
- **BCrypt.Net** — Hash de contraseñas
- **System.Net.Mail** — Envío de correos SMTP
- **Docker** — Containerización multi-stage
- **React** — Frontend
- **xUnit + Moq** — Pruebas unitarias

---

## Paquetes utilizados

### API Project

| Paquete | Versión | Descripción |
|---------|---------|-------------|
| `Npgsql.EntityFrameworkCore.PostgreSQL` | 9.0.4 | Proveedor de EF Core para PostgreSQL |
| `Microsoft.EntityFrameworkCore.Design` | 9.0.4 | Herramientas de migración |
| `Microsoft.AspNetCore.Authentication.JwtBearer` | 10.0.5 | Middleware de autenticación JWT |
| `System.IdentityModel.Tokens.Jwt` | 8.17.0 | Generación y validación de tokens JWT |
| `BCrypt.Net-Next` | 4.1.0 | Hash seguro de contraseñas |
| `Swashbuckle.AspNetCore` | 10.1.4 | Generación automática de Swagger |
| `Serilog.AspNetCore` | 10.0.0 | Logging estructurado |
| `Serilog.Sinks.Console` | 6.1.1 | Logs en consola |
| `Serilog.Sinks.File` | 7.0.0 | Logs en archivo con rotación diaria |

### Test Project

| Paquete | Versión | Descripción |
|---------|---------|-------------|
| `xunit` | 2.9.3 | Framework de pruebas unitarias |
| `xunit.runner.visualstudio` | 3.1.4 | Runner de pruebas |
| `Microsoft.NET.Test.Sdk` | 17.14.1 | Host de pruebas |
| `Moq` | 4.20.72 | Mocks para pruebas unitarias |
| `Microsoft.EntityFrameworkCore.InMemory` | 10.0.3 | Base de datos en memoria para tests |
| `coverlet.collector` | 6.0.4 | Cobertura de código |

---

## Base de datos

### Entidades

| Entidad | Descripción |
|---------|-------------|
| `User` | Usuarios del sistema (email, nombre, password hash, token reset) |
| `Category` | Categorías de incidentes (Hardware, Software, Red) |
| `Incident` | Incidentes técnicos con estado, prioridad, asociados a usuario y categoría |
| `Comment` | Comentarios en incidentes |
| `Attachment` | Archivos adjuntos en incidentes (con validación de tipo y tamaño) |

### Relaciones

```
User (1) ──── (N) Incident (N) ──── (1) Category
                     │
                     ├── (N) Comment
                     └── (N) Attachment (cascade delete)
```

### Migraciones

El proyecto usa Code-First con 5 migraciones:

1. `InitialCreate` — esquema inicial
2. `AddPasswordHashToUser` — campo de hash de contraseña
3. `AddResetTokenToUser` — token de reset de contraseña
4. `AddAttachments` — funcionalidad de archivos adjuntos
5. `AddAttachmentsv2` — mejora de archivos adjuntos

### Datos semilla

- **Admin user:** admin@incidentapi.com / admin123
- **Categorías:** Hardware, Software, Red
- **Estado por defecto de incidente:** `abierto`
