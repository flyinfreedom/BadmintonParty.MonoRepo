# AI Rules for BadmintonParty.Api and BadmintonParty.CourtManagement.Api

The BadmintonParty.Api and BadmintonParty.CourtManagement.Api projects are the web APIs for the badminton party management system. They provide API services for the frontend web application and follow the Domain-Driven Design (DDD) architecture.

## BACKEND

### Guidelines for DOTNET

#### ASP_NET

- Use minimal APIs for simple endpoints in .NET 10+ applications to reduce boilerplate code
- Implement the mediator pattern with MediatR for decoupling request handling and simplifying cross-cutting concerns
- Use API controllers with model binding and validation attributes for multi-step form data
- Implement proper exception handling with ExceptionFilter or middleware to provide consistent error responses
- Use dependency injection with scoped lifetime for request-specific services and singleton for stateless services

#### ENTITY_FRAMEWORK

- Use the repository and unit of work patterns to abstract data access logic and simplify testing
- Use migrations for database schema changes and version control with proper naming conventions
- Apply appropriate tracking behavior (AsNoTracking() for read-only queries) to optimize performance
- Implement query optimization techniques like compiled queries for frequently executed database operations