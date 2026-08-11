# architecture/vertical-slice Specification

## Purpose

Defines the Vertical Slice Architecture variant of the library API project under `vertical-slice/`, organizing code by feature/use case while preserving the same API behavior baseline as the other architecture variants.

## Requirements

### Requirement: Vertical Slice variant lives in its own folder
The system SHALL provide the Vertical Slice Architecture implementation as a separate project variant under `vertical-slice/`, without modifying `n-layer/` or `clean/`.

#### Scenario: Variants coexist
- **WHEN** a developer opens the repository root
- **THEN** `n-layer/`, `clean/`, and `vertical-slice/` are separate variants of the same library API project

#### Scenario: Other variants remain isolated
- **WHEN** the Vertical Slice variant is created or changed
- **THEN** files under `n-layer/` and `clean/` are not required to change

### Requirement: Features are organized by use case
The Vertical Slice variant SHALL organize application code by feature/use case instead of by technical layer. Each slice SHALL contain the endpoint, request/response contract, use-case logic, and data access needed for that feature.

#### Scenario: Books feature is changed
- **WHEN** a developer works on a books-related use case
- **THEN** the primary code for that use case is located in the books feature slice rather than spread across separate controller, service, and repository layers

#### Scenario: Libraries feature is changed
- **WHEN** a developer works on a libraries-related use case
- **THEN** the primary code for that use case is located in the libraries feature slice

### Requirement: Shared infrastructure is minimal
The Vertical Slice variant SHALL keep cross-cutting infrastructure shared only when it is genuinely common to multiple slices, such as database context, authentication setup, application startup, and reusable domain models.

#### Scenario: Slice needs persistence
- **WHEN** a feature slice needs database access
- **THEN** it may use the shared database context while keeping slice-specific query/command logic inside the slice

### Requirement: Functional baseline remains unchanged
The Vertical Slice variant SHALL preserve current API routes, DTO compatibility, entities, authentication behavior, and known pending endpoint state unless a future change explicitly modifies behavior.

#### Scenario: Existing tests execute against vertical slice variant
- **WHEN** the test suite is run for `vertical-slice/`
- **THEN** failures caused by still-pending endpoint implementations are treated as out of scope for this architecture change
