# RaceDay API

## 1. Project Overview

RaceDay API is a RESTful web API developed for the PROG6212 Formative Assessment 02 Part 02. The system is designed to support the management of road-running, walking and cycling events in South Africa.

The API provides functionality for user authentication, profile management, event management, event categories, participant enrolments and race results.

The system uses ASP.NET Core Web API with .NET 8 and SQL Server for data storage.

## 2. Technologies Used

- C#
- ASP.NET Core Web API (.NET 8)
- Entity Framework Core
- Microsoft SQL Server
- Swagger / OpenAPI
- xUnit for unit testing
- Git and GitHub

## 3. Project Structure

The project is organised into the following main areas:

- `Controllers` – handles incoming HTTP requests and API operations.
- `Models` – contains the application's data models.
- `DTOs` – contains data transfer objects used for requests and responses.
- `Data` – contains the database context and database configuration.
- `Tests` – contains automated tests for the API.

The exact folders and test project names depend on the final solution structure.

## 4. Main API Features

The planned API functionality includes:

- User registration and login.
- Authentication and logout.
- Viewing and updating user profiles.
- Creating, viewing, updating and deleting events, according to the user's role.
- Managing event categories.
- Enrolling participants in events.
- Recording and viewing race results.
- Automated testing of selected API functionality.

## 5. Prerequisites

Before running the project, ensure that you have installed:

- Visual Studio 2022 or another compatible .NET development environment.
- The .NET 8 SDK.
- Microsoft SQL Server or SQL Server LocalDB.
- SQL Server Management Studio (SSMS), if required for database management.

## 6. Database Configuration

The application uses SQL Server to store RaceDay data.

1. Open the project in Visual Studio.
2. Locate the application's database connection configuration.
3. Configure the connection string to point to your SQL Server instance and the `RaceDayDB` database.
4. Ensure that the required database and tables exist.
5. Apply Entity Framework Core migrations if the project uses migrations.

Do not commit passwords or other sensitive database credentials to GitHub. Use user secrets or environment variables where appropriate.

## 7. Running the API

1. Open the solution in Visual Studio.
2. Restore the NuGet packages.
3. Confirm that the database connection is configured correctly.
4. Set `API_RaceDay` as the startup project.
5. Build the solution.
6. Run the project using HTTPS or HTTP as configured.
7. Open the Swagger URL displayed by the application, if Swagger is enabled.

Swagger can be used to inspect available endpoints and send test requests to the API.

## 8. API Endpoints

The API uses RESTful HTTP methods. The actual routes should be confirmed against the implemented controllers.

The main endpoint groups are expected to include:

| Feature | Purpose |
|---|---|
| Authentication | Registration, login and logout |
| Profiles | View and update user profile information |
| Events | Manage and view road events |
| Categories | Manage event categories |
| Enrolments | Register participants for events |
| Results | Record and retrieve race results |

Refer to the implemented controllers and Swagger documentation for the exact routes, request formats and responses.

## 9. Authentication and Authorisation

The API includes authentication functionality for users of the system. Access to protected operations should be restricted according to the authentication and role-authorisation mechanisms implemented in the application.

For assessment, test registration, login and protected endpoints using the configured authentication method.

## 10. Unit Testing

The solution includes a test project for automated testing of selected API functionality.

To run the tests in Visual Studio:

1. Open **Test → Test Explorer**.
2. Build the solution.
3. Select **Run All Tests**.

Alternatively, run the tests from a terminal in the solution directory:

```bash
dotnet test
```

Review the test results and resolve any failing tests before submission.

## 11. Running from the Command Line

From the directory containing the API project file, run:

```bash
dotnet restore
dotnet build
dotnet run
```

Run these commands from the API project directory. Run `dotnet test` from the solution directory when the test project is included in the solution.

## 12. Repository Contents

The repository should contain the source code, project and solution files, required configuration templates, and automated tests needed to build and assess the application.

Generated build folders such as `bin` and `obj` should normally be excluded from version control.

## 13. Author and Assessment

**Module:** PROG6212  
**Assessment:** Formative Assessment 02 Part 02  
**Project:** RaceDay API  
**Framework:** .NET 8

## 14. Notes

This README describes the intended project structure and functionality. Confirm the final endpoint routes, database setup, authentication method and test coverage against the implemented application before submission.
