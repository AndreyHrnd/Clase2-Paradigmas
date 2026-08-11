## Why

The course project now needs multiple architecture variants of the same library API. The `clean/` folder must become the Clean Architecture version, separate from the existing `n-layer/` variant, so the same domain can be compared across architectural styles.

## What Changes

- Build the Clean Architecture variant inside `clean/`, using the current library API behavior as the functional baseline.
- Preserve the same solution purpose, API routes, entities, tests, and pending HackerRank functionality scope as the other variants.
- Reorganize project dependencies so application/business logic defines persistence contracts and infrastructure/data access implements them.
- Ensure the core/application layer does not depend on EF Core, `LibraryContext`, or infrastructure implementation details.
- Keep infrastructure-specific code in DataAccess: EF Core context, migrations, repository implementations, and database provider packages.
- Keep Api as the composition root: controllers call application services, while `Startup.cs` wires infrastructure implementations to application contracts.
- Verify the `clean/` solution builds, and run tests while documenting any expected failures caused by still-pending endpoint implementations.

## Capabilities

### New Capabilities

- `architecture/clean`: Defines the Clean Architecture dependency rules and folder/project responsibilities for the `clean/` variant.

### Modified Capabilities

- None. Existing specs are not yet archived into `openspec/specs/`, so this change introduces a new architectural capability.

## Impact

- `clean/`: contains the Clean Architecture variant of the library API solution.
- `clean/src/HackerRank1.BusinessLogic`: becomes the application layer that owns service interfaces and repository contracts.
- `clean/src/HackerRank1.DataAccess`: becomes the infrastructure layer that implements repository contracts using EF Core.
- `clean/src/HackerRank1.Api`: remains the outer web/API layer and dependency injection composition root.
- `clean/src/HackerRank1.Entities`: remains the domain/entities layer with no project dependencies.
- `n-layer/` is not modified by this change.
