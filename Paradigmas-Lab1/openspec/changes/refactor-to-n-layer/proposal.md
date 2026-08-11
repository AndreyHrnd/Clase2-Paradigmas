## Why

The `HackerRank1.sln` solution is split into Entities, DataAccess, BusinessLogic, and Api projects, but the BusinessLogic services still use `LibraryContext` directly. That keeps EF Core concerns inside the business layer and makes the current structure a project split rather than a real N-layer architecture.

This change defines and prepares a refactor to classic N-layer structure: Api -> BusinessLogic -> DataAccess -> Entities, with repository abstractions isolating persistence.

## What Changes

- Add repository abstractions and implementations in `HackerRank1.DataAccess` for libraries and books.
- Refactor `LibrariesService` and `BooksService` so BusinessLogic depends on repository interfaces instead of `LibraryContext` or EF Core types.
- Register services and repositories with scoped lifetimes so they align with the scoped EF Core context.
- Remove `[Authorize]` from `BooksController.GetAll`, keeping the books list endpoint aligned with the HackerRank API contract that does not require a token.
- Keep pending endpoint implementation out of scope: `LibrariesService.Delete`, `BooksService.Add/Update/Delete`, missing controller actions, and test completion are not implemented by this change.
- Preserve existing project boundaries, DTO namespaces, JSON shapes, and public method signatures unless required for dependency injection.

## Capabilities

### New Capabilities

- `architecture/n-layer`: Defines the enforced layered structure, persistence isolation through repositories, and dependency-injection lifetime expectations.
- `api/books`: Defines the authentication behavior of the books list API endpoint.

### Modified Capabilities

- None. There are no existing specs under `openspec/specs/`.

## Impact

- `src/HackerRank1.DataAccess`: new repository interfaces and EF Core repository implementations.
- `src/HackerRank1.BusinessLogic`: services change from direct `LibraryContext` usage to repository injection.
- `src/HackerRank1.Api`: DI registration updates and removal of `[Authorize]` from the books GET action.
- Tests remain read-only and are not modified.
- No new external dependencies are required.
