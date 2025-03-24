# Asmt API Project

A .NET 7.0 Web API solution following a layered architecture pattern.

## Project Structure

The solution consists of the following projects:

- **Asmt.Main** - The main web API project that handles HTTP requests and serves as the entry point
- **Asmt.BL** - Business Logic layer containing the core business logic and services
- **Asmt.DAL** - Data Access Layer handling database operations and data persistence
- **Asmt.Test** - Test project containing unit tests

## Technical Stack

- .NET 7.0
- ASP.NET Core Web API
- Entity Framework Core (based on the presence of EntityFrameworkCore.targets)

## Getting Started

### Prerequisites

- .NET 7.0 SDK
- Your preferred IDE (Visual Studio, VS Code, etc.)

### Building the Project

```bash
dotnet build
```

### Running the Project

```bash
cd Asmt.Main
dotnet run
```

### Adding Migrations

```bash
cd Asmt.DAL
dotnet ef migrations add STORY002_AddUserTables -s ..\Asmt.Main\Asmt.Main.csproj
dotnet ef database update -s ..\Asmt.Main\Asmt.Main.csproj
```

## Project Configuration

The application supports multiple environments (Debug/Release) and is configured to run on Windows, Linux, and macOS platforms.
