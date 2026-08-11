## Purpose

Defines the Clean Architecture variant of the library API project under `clean/`, separating domain/application concerns from infrastructure and presentation while preserving the same API behavior baseline.

## ADDED Requirements

### Requirement: Clean variant lives in its own folder
The system SHALL provide the Clean Architecture implementation as a separate project variant under `clean/`, without modifying the `n-layer/` variant.

#### Scenario: Variants coexist
- **WHEN** a developer opens the repository root
- **THEN** the `clean/` and `n-layer/` folders are separate variants of the same library API project

#### Scenario: N-layer variant remains isolated
- **WHEN** the Clean Architecture variant is changed
- **THEN** files under `n-layer/` are not required to change

### Requirement: Application layer owns persistence contracts
The application/business layer SHALL define the repository contracts used by business services. Infrastructure SHALL implement those contracts, and the application/business layer SHALL NOT depend on DataAccess or EF Core.

#### Scenario: Business service requests library persistence
- **WHEN** a business service needs library data
- **THEN** it depends on an application-owned repository contract rather than an infrastructure implementation

#### Scenario: Application layer builds without infrastructure reference
- **WHEN** the Clean Architecture solution is compiled
- **THEN** the application/business project builds without a project reference to the data access project

### Requirement: Infrastructure depends inward
The infrastructure/data access layer SHALL contain EF Core context, migrations, database provider packages, and repository implementations. It SHALL depend inward on application contracts and domain entities.

#### Scenario: Repository implementation uses EF Core
- **WHEN** a repository implementation executes a database operation
- **THEN** EF Core usage remains inside the infrastructure/data access layer

### Requirement: Api acts as the composition root
The API layer SHALL wire infrastructure implementations to application contracts through dependency injection while controllers continue to call application/business services.

#### Scenario: Request resolves a business service
- **WHEN** an HTTP request resolves a business service
- **THEN** dependency injection supplies the infrastructure repository implementation behind the application-owned contract

### Requirement: Functional baseline remains unchanged
The Clean Architecture variant SHALL preserve the current route surface, DTO namespaces, entities, and known pending endpoint state unless a future change explicitly modifies behavior.

#### Scenario: Existing tests execute against clean variant
- **WHEN** the test suite is run for `clean/`
- **THEN** failures caused by still-pending endpoint implementations are treated as out of scope for this architecture change
