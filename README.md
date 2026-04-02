# LoanCalculator

Aplicacion web ASP.NET Core para calculo de prestamos con arquitectura por capas (Presentacion, Logica de negocio y Datos), incluyendo inicializacion automatica de base de datos mediante scripts SQL.

## Stack Tecnologico

- .NET 8 (ASP.NET Core MVC)
- SQL Server (Localhost)
- Tailwind CSS (via CDN)
- ADO.NET (Microsoft.Data.SqlClient)

## Arquitectura

La solucion esta organizada en las siguientes capas:

- `LoanCalculator.Web`: Presentacion (MVC, vistas, layout, controllers)
- `LoanCalculator.Application`: Logica de negocio (servicios, validaciones de reglas)
- `LoanCalculator.Domain`: Entidades de dominio
- `LoanCalculator.Infrastructure`: Acceso a datos (repositorios, conexion SQL, inicializacion de scripts)

## Prerrequisitos

- .NET SDK 8 instalado
- SQL Server disponible en `localhost`
- Permisos para `Trusted_Connection=True`

## Configuracion de Connection String

Para crear la base de datos por primera vez y ejecutar los scripts de inicializacion, usa esta conexion en `appsettings.Development.json` o `appsettings.json`:

```json
"DefaultConnection": "Server=localhost;Trusted_Connection=True;TrustServerCertificate=True;"
```

Luego, para el funcionamiento normal de la aplicacion, configura:

```json
"DefaultConnection": "Server=localhost;Database=LoanCalculator;Trusted_Connection=True;TrustServerCertificate=True;"
```

## Migracion / Inicializacion de Base de Datos

Este proyecto no usa EF Core migrations. La base de datos se inicializa automaticamente con scripts SQL al iniciar la aplicacion, desde `DatabaseInitializer`.

Scripts ejecutados en orden:

1. `LoanCalculator.Web/database/01_create_database.sql`
2. `LoanCalculator.Web/database/02_tables.sql`
3. `LoanCalculator.Web/database/03_seed_data.sql`
4. `LoanCalculator.Web/database/04_stored_procedures.sql`

## Setup y Ejecucion Local

1. Restaurar paquetes:

```bash
dotnet restore
```

2. Configurar `DefaultConnection` para inicializacion (sin `Database=LoanCalculator`):

```json
"DefaultConnection": "Server=localhost;Trusted_Connection=True;TrustServerCertificate=True;"
```

3. Ejecutar la aplicacion una primera vez para crear base de datos, tablas, data semilla y stored procedures:

```bash
dotnet run --project LoanCalculator.Web
```

4. Cambiar `DefaultConnection` a modo funcionamiento (con `Database=LoanCalculator`):

```json
"DefaultConnection": "Server=localhost;Database=LoanCalculator;Trusted_Connection=True;TrustServerCertificate=True;"
```

5. Ejecutar nuevamente la aplicacion:

```bash
dotnet run --project LoanCalculator.Web
```

## Notas Importantes

- Si usas perfil `Development`, prioriza `LoanCalculator.Web/appsettings.Development.json`.
- Si no usas `Development`, aplica en `LoanCalculator.Web/appsettings.json`.
- Si la carpeta `database` no se encuentra, la inicializacion fallara.

## Estructura Rapida

```text
LoanCalculator.Web/             -> UI MVC + scripts SQL
LoanCalculator.Application/     -> servicios de negocio
LoanCalculator.Domain/          -> entidades de dominio
LoanCalculator.Infrastructure/  -> repositorios y acceso SQL
```