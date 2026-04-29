#  IncidentAPI

Sistema de gestión de incidentes técnicos desarrollado con **ASP.NET Core 8** y **SQLite**, especializado en el registro, seguimiento y resolución de incidentes técnicos con soporte para usuarios, categorías, comentarios, **autenticación JWT** y **notificaciones por correo**.

Estas instrucciones te permitirán obtener una copia del proyecto en funcionamiento en tu máquina local con el propósito de desarrollo y testing.

---

## 🛠️ Requisitos

- .NET 8 SDK
- Git
- VS Code
- Extensión C# Dev Kit (VS Code)
- Node.js 18+ *(para el frontend React)*

---

## ⚙️ Instalación

### Backend

```bash
# 1. Clonar el repositorio
git clone https://github.com/Jhon-Zuluaga/IncidentAPI.git
cd IncidentAPI

# 2. Restaurar dependencias
dotnet restore

# 3. Instalar herramienta de Entity Framework Core
dotnet tool install --global dotnet-ef

# 4. Configurar la clave secreta JWT (fuera del repositorio)
cd IncidentAPI.Api
dotnet user-secrets init
dotnet user-secrets set "Jwt:Key" "TuClaveSecret1234!"

# 5. Configurar credenciales de correo Gmail
dotnet user-secrets set "Email:Username" "tucorreo@gmail.com"
dotnet user-secrets set "Email:Password" "abcd efgh ijkl mnop"

# 6. Aplicar migraciones y crear la base de datos
dotnet ef database update

# 7. Correr el proyecto
dotnet run
```


### Acceder a Swagger

```
http://localhost:5230/swagger
```

### Frontend React -> https://github.com/Jhon-Zuluaga/IncidentAPI-frontend.git

```bash
cd frontend
npm install
npm run dev
```

---

##  Características

###  Gestión de Incidentes
- CRUD completo de incidentes técnicos
- Control de estado: `abierto`, `en_progreso`, `cerrado`
- Asociación de incidentes a usuarios y categorías
- Notificación automática por correo al crear un incidente

###  Comentarios en Incidentes
- CRUD completo de comentarios por incidente
- Cada comentario registra autor y fecha

###  Gestión de Usuarios
- CRUD completo de usuarios
- Validación de email
- Contraseña hasheada con BCrypt (nunca en texto plano)

###  Gestión de Categorías
- CRUD completo de categorías
- Agrupación de incidentes por categoría

###  Autenticación JWT con Cookies
- Login y logout seguros
- Token JWT almacenado en cookie `HttpOnly` (protección XSS)
- `SameSite=Strict` para mitigar CSRF
- Expiración configurable (por defecto 60 minutos)
- Endpoints protegidos con `[Authorize]`
- Registro de usuarios disponible sin autenticación (`[AllowAnonymous]`)

###  Notificaciones por Correo SMTP
- Notificación automática al usuario al crear un incidente
- Recuperación de contraseña con token de 6 dígitos
- Token de recuperación con expiración de 15 minutos
- Implementado con `System.Net.Mail` (SMTP nativo de .NET) vía Gmail

###  Middleware Global de Errores
- Captura de excepciones no controladas
- Respuesta JSON estandarizada con `status`, `message` y `detail`
- Registrado antes de los controllers para cubrir todo el pipeline

###  Documentación con Swagger
- Documentación automática e interactiva de todos los endpoints
- Accesible en `/swagger`

###  Logging con Serilog
- Logs estructurados en consola
- Logs guardados en archivos con rotación diaria en `Logs/`

###  Pruebas Unitarias
- 5 pruebas con xUnit y Moq
- Camino feliz y casos de error cubiertos

---

## 🏗️ Arquitectura

El proyecto está organizado en capas siguiendo principios de **DDD (Domain Driven Design)** y el **Repository Pattern**:

```
IncidentAPI/
├── IncidentAPI.Api/
│   ├── Controllers/       → Endpoints HTTP (CRUD + Auth)
│   ├── Models/            → Entidades de la base de datos
│   ├── DTOs/              → Objetos de transferencia de datos
│   │   ├── Auth/
│   │   ├── Category/
│   │   ├── Comment/
│   │   ├── Incident/
│   │   └── User/
│   ├── Data/              → AppDbContext (EF Core)
│   ├── Repositories/      → Acceso a la base de datos
│   │   ├── Interfaces/
│   │   └── Implementations/
│   ├── Services/          → Lógica de negocio
│   │   ├── Interfaces/
│   │   └── Implementations/
│   ├── Middleware/        → Manejo global de errores
│   └── Program.cs         → Configuración general
├── IncidentAPI.Tests/     → Pruebas unitarias
└── frontend/              → Aplicación React
    ├── Api/               → Peticiones al backend
    ├── Components/        → Piezas visuales reutilizables
    ├── Constants/         → Valores fijos (estados de incidente)
    ├── Pages/             → Páginas completas (LoginPage)
    └── Sections/          → Secciones del panel principal
```

**Flujo de petición:**

```
Cliente HTTP / React
        ↓
   LoginPage  →  POST /api/Auth/login  →  Cookie JWT (HttpOnly)
        ↓
   Controller  →  [Authorize]  →  Service  →  Repository  →  EF Core  →  SQLite
        ↑
   Middleware (captura errores en todo el pipeline)
```

---

##  Endpoints de la API

###  Auth

| Método | Ruta | Auth | Descripción |
|--------|------|------|-------------|
| POST | `/api/Auth/login` | ❌ Público | Inicia sesión y setea la cookie JWT |
| POST | `/api/Auth/logout` | ✅ Requiere token | Cierra sesión y elimina la cookie |
| POST | `/api/Auth/forgot-password` | ❌ Público | Envía token de recuperación por correo |
| POST | `/api/Auth/reset-password` | ❌ Público | Cambia la contraseña con el token recibido |

###  Incidents

| Método | Ruta | Descripción |
|--------|------|-------------|
| GET | `/api/Incident` | Obtiene todos los incidentes |
| GET | `/api/Incident/{id}` | Obtiene un incidente por Id |
| POST | `/api/Incident` | Crea un nuevo incidente |
| PUT | `/api/Incident/{id}` | Actualiza un incidente |
| DELETE | `/api/Incident/{id}` | Elimina un incidente |

###  Comments

| Método | Ruta | Descripción |
|--------|------|-------------|
| GET | `/api/Comment/incident/{incidentId}` | Obtiene comentarios de un incidente |
| GET | `/api/Comment/{id}` | Obtiene un comentario por Id |
| POST | `/api/Comment` | Crea un nuevo comentario |
| PUT | `/api/Comment/{id}` | Actualiza un comentario |
| DELETE | `/api/Comment/{id}` | Elimina un comentario |

###  Users

| Método | Ruta | Auth | Descripción |
|--------|------|------|-------------|
| GET | `/api/User` | ✅ | Obtiene todos los usuarios |
| GET | `/api/User/{id}` | ✅ | Obtiene un usuario por Id |
| POST | `/api/User` | ❌ Público | Crea un nuevo usuario (registro) |
| PUT | `/api/User/{id}` | ✅ | Actualiza un usuario |
| DELETE | `/api/User/{id}` | ✅ | Elimina un usuario |

###  Categories

| Método | Ruta | Descripción |
|--------|------|-------------|
| GET | `/api/Category` | Obtiene todas las categorías |
| GET | `/api/Category/{id}` | Obtiene una categoría por Id |
| POST | `/api/Category` | Crea una nueva categoría |
| PUT | `/api/Category/{id}` | Actualiza una categoría |
| DELETE | `/api/Category/{id}` | Elimina una categoría |

> ✅ Todos los endpoints excepto `/api/Auth/login`, `/api/Auth/forgot-password`, `/api/Auth/reset-password` y `POST /api/User` requieren autenticación JWT.

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

> ⚠️ La `Jwt:Key` **nunca** va en `appsettings.json`. Se configura con `dotnet user-secrets`.

### Seguridad aplicada

| Medida | Descripción |
|--------|-------------|
| `HttpOnly = true` | JavaScript no puede leer la cookie → protege de XSS |
| `SameSite = Strict` | Mitiga ataques CSRF |
| `Secure = true` | Solo HTTPS en producción |
| `ClockSkew = Zero` | El token expira exactamente en el tiempo configurado |
| BCrypt | Las contraseñas nunca se guardan en texto plano |

---

## 📧 Notificaciones por Correo SMTP

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

> ⚠️ El `Email:Username` y `Email:Password` se configuran con `dotnet user-secrets`. No usar la contraseña normal de Gmail — generar una **contraseña de aplicación** de 16 caracteres en: *Cuenta Google → Seguridad → Verificación en dos pasos → Contraseñas de aplicación*.

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

## ⚛️ Frontend React

La aplicación React consume la API usando **cookies JWT** automáticamente.

### Estructura de carpetas

```
frontend/
├── Api/          → Todas las peticiones al backend (fetch con credentials: 'include')
├── Components/   → Piezas visuales reutilizables (Input, Btn, Modal)
├── Constants/    → Valores fijos (estados de incidente: abierto, en_progreso, cerrado)
├── Pages/        → Página completa de login (LoginPage)
└── Sections/     → Secciones del panel (IncidentsSection, UsersSection...)
```

### Puntos clave de la integración

- Todas las peticiones incluyen `credentials: 'include'` para enviar la cookie automáticamente.
- Si la API devuelve `401`, React detecta el token expirado y redirige al login.
- El login exitoso setea la cookie en el navegador — React no maneja el token directamente.

---

## 🧪 Pruebas unitarias

```bash
cd IncidentAPI.Tests
dotnet test
```

Las pruebas cubren:

- ✅ `CreateAsync_ValidData` — crear un incidente con datos válidos *(camino feliz)*
- ✅ `UpdateAsync_IncidentNotFound` — actualizar un incidente que no existe *(caso de error)*
- ✅ `UpdateAsync_InvalidStatus` — actualizar con un estado inválido *(caso de error)*
- ✅ `GetByIdAsync_ExistingId` — obtener un incidente existente *(camino feliz)*
- ✅ `GetByIdAsync_NotExistingId` — obtener un incidente que no existe *(caso de error)*

---

## 🏗️ Construido con

- **ASP.NET Core 8** — Framework principal
- **Entity Framework Core** — ORM
- **SQLite** — Base de datos
- **Swagger / OpenAPI** — Documentación
- **Serilog** — Logging
- **JWT (JwtBearer)** — Autenticación
- **BCrypt.Net** — Hash de contraseñas
- **System.Net.Mail** — Envío de correos SMTP
- **React** — Frontend
- **xUnit + Moq** — Pruebas unitarias

---

## 📚 Paquetes utilizados

| Paquete | Versión | Descripción |
|---------|---------|-------------|
| `Npgsql.EntityFrameworkCore.PostgreSQL` | 8.x | Proveedor de Entity Framework Core para PostgreSQL |
| `Microsoft.EntityFrameworkCore.Design` | 8.x | Herramientas de migración |
| `Swashbuckle.AspNetCore` | 6.x | Generación automática de Swagger |
| `Serilog.AspNetCore` | 8.x | Logging estructurado |
| `Serilog.Sinks.Console` | 5.x | Logs en consola |
| `Serilog.Sinks.File` | 5.x | Logs en archivo |
| `Microsoft.AspNetCore.Authentication.JwtBearer` | 8.x | Middleware de autenticación JWT |
| `System.IdentityModel.Tokens.Jwt` | 7.x | Generación y validación de tokens JWT |
| `BCrypt.Net-Next` | 4.x | Hash seguro de contraseñas |
| `Moq` | 4.x | Mocks para pruebas unitarias |
| `xUnit` | 2.x | Framework de pruebas unitarias |
