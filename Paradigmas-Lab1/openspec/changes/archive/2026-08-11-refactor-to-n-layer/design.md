## Context

See `proposal.md` for motivation. The codebase already has separate projects for Api, BusinessLogic, DataAccess, and Entities, but `LibrariesService` and `BooksService` currently depend on `LibraryContext` directly. This places EF Core access in the business layer and weakens the N-layer separation expected for the lab.

The integration tests are treated as read-only. They import DTOs from `HackerRank1.Entities.DTO`, so this refactor avoids moving DTO namespaces even though DTO placement is not ideal.

Current intended dependency shape:

```text
Api
 |-- BusinessLogic
 |-- DataAccess        (composition root registers DbContext/repositories)
 `-- Entities

BusinessLogic
 |-- DataAccess        (repository abstractions)
 `-- Entities

DataAccess
 `-- Entities

Entities
 `-- no project dependencies
```

## Goals / Non-Goals

**Goals:**

- Make the current 4-project split behave as a real N-layer architecture.
- Move EF Core query and command code out of BusinessLogic and into DataAccess repositories.
- Keep service public contracts stable while changing their dependencies.
- Align DI lifetimes for services and repositories with the scoped EF Core context.
- Remove authentication requirement from the books list endpoint.

**Non-Goals:**

- Implement missing HackerRank functionality such as book POST, library DELETE, or service `NotImplementedException` methods.
- Move DTOs out of `Entities.DTO`; that would break read-only tests that import that namespace.
- Replace EF Core, change migrations, or change the Supabase/Postgres connection behavior.
- Refactor the auth implementation, hardcoded demo user, or JWT token generation.
- Convert the .NET hosting style from `Startup` to minimal hosting.

## Decisions

### Decision: Keep repository abstractions in DataAccess

Repository interfaces and implementations will live under `HackerRank1.DataAccess.Repositories`. BusinessLogic will depend on the repository interfaces and will no longer receive `LibraryContext`.

Alternative considered: create a fifth abstractions project or place repository interfaces in BusinessLogic so DataAccess implements upward-facing contracts. That is closer to Clean Architecture, but it is more disruptive than needed for this lab. Classic N-layer keeps the dependency flowing downward.

### Decision: Preserve existing DTO namespace

DTOs remain in `HackerRank1.Entities.DTO` for this change. The tests import this namespace directly, and the requested scope is N-layer restructuring rather than API contract package cleanup.

Alternative considered: move request/response DTOs into the Api layer. This is cleaner in a larger system, but it creates churn and risks breaking the read-only test project.

### Decision: Use scoped lifetimes for services and repositories

Business services and repositories will be registered as scoped. This matches the lifetime of EF Core's request-scoped context and avoids mixing transient business services with scoped persistence dependencies.

Alternative considered: keep current transient service registrations. This works in many simple cases, but it is weaker architecture and can obscure request-scope behavior.

### Decision: Remove authorization from books GET only

`BooksController.GetAll` will lose `[Authorize]`. JWT configuration, `/login`, `AuthenticationService`, and `TokenGenerator` remain unchanged.

Alternative considered: keep `[Authorize]` and update tests to send JWTs. Tests are read-only and the HackerRank spec expects unauthenticated access, so this was rejected.

## Risks / Trade-offs

- Repository interfaces in DataAccess still mean BusinessLogic references the DataAccess assembly -> acceptable for classic N-layer, but less strict than Clean Architecture.
- DTOs remain in Entities -> preserves compatibility, but keeps a known layering compromise for a later cleanup.
- Tests will not become fully green from this change alone -> expected because endpoint implementation is explicitly out of scope.
- Removing `[Authorize]` changes observable behavior of one endpoint -> intentional and captured in `api/books`.

## Migration Plan

1. Add repository files under DataAccess.
2. Refactor services to receive repositories instead of `LibraryContext`.
3. Update DI registration in `Startup.cs`.
4. Remove `[Authorize]` from `BooksController.GetAll`.
5. Build and run tests to confirm compile stability and no unintended regressions.

Rollback: revert service constructor changes, repository registrations, and the `[Authorize]` removal; no database migration rollback is needed.
