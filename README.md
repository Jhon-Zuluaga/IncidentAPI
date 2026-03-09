# 🚀 IncidentAPI

Sistema de gestión de incidentes técnicos desarrollado con ASP.NET Core SQLite, especializado en el registro, seguimiento y resolución de incidentes técnicos con soporte para usuarios, categorias y comentarios.

---

## 📚 Tabla de Contenido
- [🚀 Inicio rápido](#-inicio-rápido)
- [🛠️ Requisitos](#️-prerequisitos)
- [⚙️ Instalación](#️-instalación)
- [✨ Características](#-características)
- [🏗️ Arquitectura](#️-arquitectura)
- [📋 Endpoints de la API](#-endpoints-de-la-api)
- [🧪 Pruebas unitarias](#-pruebas-unitarias)
- [🏗️ Construido con](#️-construido-con)
- [📚 Paquetes utilizados](#-paquetes-utilizados)

---

## 🚀 Inicio rápido

Estas instrucciones te permitirán obtener una copia del proyecto en funcionamiento en tu máquina local con el propósito de desarrollo y testing.

---

# 🛠️ Requisitos

Para poder utilizar este software necesitaras: 

- .NET 8 SDK
- Git
- VSC
- Extensión C# Dev Kit (VSC)

---

## ⚙️ Instalación 

Paso a paso para obtener un entorno de desarrollo ejecutable: 

**1. Clonar el repositorio**
```bash
git clone https://github.com/Jhon-Zuluaga/IncidentAPI.git
cd IncidentAPI
```

**2. Restaurar dependencias**
```bash
dotnet restore
```

**3. Instalar herramienta de Entity Framework Core**
```bash
dotnet tool install --global dotnet-ef
```


**4. Aplicar migraciones y crear la base de datos**
```bash
cd IncidentAPI.Api
dotnet ef database update
```

**5. Correr el proyecto**
```bash
dotnet run
```

**6. Abrir Swagger en el navegador**
```
http://localhost:5230/swagger
```

---

## Características

### 🚨 Gestión de Incidentes
- CRUD Completo de incidentes técnicos
- Control de estado: "abierto", "en_progreso", "cerrado"
- Asociación de incidentes a usuarios y categorias

### 💬 Comentarios en Incidentes
- CRUD Completo de comentarios
- Registro de comentarios por incidente
- Cada comentario tiene autor y fecha

### 👥 Gestión de Usuarios
- CRUD Completo de usuarios
- Validación de email

### 📂 Gestión de Categorías
- CRUD completo de categorías
- Agrupación de incidentes por categoría

### 🛡️ Middleware Global de Errores
- Captura de excepciones no controladas
- Respuesta JSON con status, mensaje y detalle
- Implementado antes de los controllers para cubrir toda la aplicación

### 📋 Documentación con Swagger
- Documentación automática e interactiva de todos los endpoints
- Accesible con /swagger

### 📊 Logging con Serilog
- Logs estructurados en consola
- Logs guardados en archivos con rotación diaria en la carpeta Logs/

### 🧪 Pruebas Unitarias
- 5 pruebas con xUnit y Moq
- Camino feliz y casos de error cubiertos

---

### 🏗️ Arquitectura

El proyecto está organizado en capas siguiendo principios de **DD (Domain Driven Desing)** y el **Repository Pattern**:

```
IncidentAPI/
├── IncidentAPI.Api/
│   ├── Controllers/       → Endpoints HTTP (CRUD)
│   ├── Models/            → Entidades de la base de datos
│   ├── DTOs/              → Objetos de transferencia de datos
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
└── IncidentAPI.Tests/     → Pruebas unitarias
```


### Flujo de petición: 
```
Cliente HTTP
     ↓
Controller  →  Service  →  Repository  →  EF Core  →  SQLite
     ↑                                                    
Middleware (captura errores en todo el pipeline)
```

## 📋 Endpoints de la API

### 🚨 Incidents
| Método | Ruta | Descripción |
|--------|------|-------------|
| GET | `/api/Incident` | Obtiene todos los incidentes |
| GET | `/api/Incident/{id}` | Obtiene un incidente por Id |
| POST | `/api/Incident` | Crea un nuevo incidente |
| PUT | `/api/Incident/{id}` | Actualiza un incidente |
| DELETE | `/api/Incident/{id}` | Elimina un incidente |

### 💬 Comments
| Método | Ruta | Descripción |
|--------|------|-------------|
| GET | `/api/Comment/incident/{incidentId}` | Obtiene comentarios de un incidente |
| GET | `/api/Comment/{id}` | Obtiene un comentario por Id |
| POST | `/api/Comment` | Crea un nuevo comentario |
| PUT | `/api/Comment/{id}` | Actualiza un comentario |
| DELETE | `/api/Comment/{id}` | Elimina un comentario |

### 👥 Users
| Método | Ruta | Descripción |
|--------|------|-------------|
| GET | `/api/User` | Obtiene todos los usuarios |
| GET | `/api/User/{id}` | Obtiene un usuario por Id |
| POST | `/api/User` | Crea un nuevo usuario |
| PUT | `/api/User/{id}` | Actualiza un usuario |
| DELETE | `/api/User/{id}` | Elimina un usuario |

### 📂 Categories
| Método | Ruta | Descripción |
|--------|------|-------------|
| GET | `/api/Category` | Obtiene todas las categorías |
| GET | `/api/Category/{id}` | Obtiene una categoría por Id |
| POST | `/api/Category` | Crea una nueva categoría |
| PUT | `/api/Category/{id}` | Actualiza una categoría |
| DELETE | `/api/Category/{id}` | Elimina una categoría |

---

## 🧪 Pruebas unitarias

Para correr las pruebas:
```bash
cd IncidentAPI.Tests
dotnet test
```
Las pruebas cubren:
- ✅ `CreateAsync_ValidData` — crear un incidente con datos válidos (camino feliz)
- ✅ `UpdateAsync_IncidentNotFound` — actualizar un incidente que no existe (caso de error)
- ✅ `UpdateAsync_InvalidStatus` — actualizar con un estado inválido (caso de error)
- ✅ `GetByIdAsync_ExistingId` — obtener un incidente existente (camino feliz)
- ✅ `GetByIdAsync_NotExistingId` — obtener un incidente que no existe (caso de error)

---

## 🏗️ Construido con

- [ASP.NET Core 8](https://dotnet.microsoft.com/) - Framework principal
- [Entity Framework Core](https://learn.microsoft.com/ef/core/) - ORM
- [SQLite](https://www.sqlite.org/) - Base de datos
- [Swagger / OpenAPI](https://swagger.io/) - Documentación
- [Serilog](https://serilog.net/) - Logging
- [xUnit](https://xunit.net/) - Pruebas unitarias
- [Moq](https://github.com/moq/moq4) - Mocks para pruebas

---

## 📚 Paquetes utilizados

| Paquete | Versión | Descripción |
|---------|---------|-------------|
| Microsoft.EntityFrameworkCore.Sqlite | 8.x | ORM con soporte para SQLite |
| Microsoft.EntityFrameworkCore.Design | 8.x | Herramientas de migración |
| Swashbuckle.AspNetCore | 6.x | Generación automática de Swagger |
| Serilog.AspNetCore | 8.x | Logging estructurado |
| Serilog.Sinks.Console | 5.x | Logs en consola |
| Serilog.Sinks.File | 5.x | Logs en archivo |
| Moq | 4.x | Mocks para pruebas unitarias |
| xUnit | 2.x | Framework de pruebas unitarias |

---

> API RESTful con .NET Core 8 



