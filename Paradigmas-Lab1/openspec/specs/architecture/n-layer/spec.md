# architecture/n-layer Specification

## Purpose

Defines the N-layer architecture constraints for the library API solution: layer dependency direction, persistence isolation, and dependency-injection lifetime rules.

## Requirements

### Requirement: Business logic accesses persistence through repositories
The BusinessLogic layer SHALL access library and book persistence only through repository interfaces. BusinessLogic services SHALL NOT depend directly on EF Core context types.

#### Scenario: Books service queries persistence
- **WHEN** the books business service retrieves books for a library
- **THEN** it uses a books repository abstraction rather than the EF Core context directly

#### Scenario: Libraries service delegates persistence
- **WHEN** the libraries business service reads, creates, updates, or deletes library data
- **THEN** it delegates persistence work to a libraries repository abstraction

### Requirement: Layer dependencies flow downward
The solution SHALL preserve a downward dependency model: Api composes and calls BusinessLogic, BusinessLogic uses DataAccess abstractions, DataAccess owns persistence implementation, and Entities remains dependency-free domain/model code.

#### Scenario: Solution builds without circular dependencies
- **WHEN** the solution is compiled after the refactor
- **THEN** projects compile without circular project references or upward layer dependencies

### Requirement: Data access implementation owns EF Core usage
The DataAccess layer SHALL contain the EF Core `LibraryContext`, database migrations, and repository implementations that execute EF Core queries and commands.

#### Scenario: Persistence implementation changes are isolated
- **WHEN** repository methods execute database operations
- **THEN** EF Core usage remains inside the DataAccess layer and is not duplicated in controllers or business services

### Requirement: Repository and service lifetimes align with the data context
Business services and repositories SHALL be registered with a scoped lifetime so each request scope can share the same scoped data context safely.

#### Scenario: Request resolves business services
- **WHEN** a request resolves a business service that depends on a repository
- **THEN** both the service and repository are resolved within the same request scope as the data context
