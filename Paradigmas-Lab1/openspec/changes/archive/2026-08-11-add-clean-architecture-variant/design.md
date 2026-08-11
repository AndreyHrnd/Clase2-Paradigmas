## Context

See `proposal.md` for motivation. The repository now needs architecture variants side by side. The `n-layer/` folder is the classic layered version, while `clean/` must represent Clean Architecture for the same library API domain.

The existing project names can remain for minimal churn, but their logical responsibilities change in `clean/`:

```text
clean/
  src/HackerRank1.Entities        -> Domain entities/settings currently shared by the app
  src/HackerRank1.BusinessLogic   -> Application layer: services and repository contracts
  src/HackerRank1.DataAccess      -> Infrastructure layer: EF Core, migrations, repository implementations
  src/HackerRank1.Api             -> Presentation + composition root
```

## Goals / Non-Goals

**Goals:**

- Keep `clean/` as an independent Clean Architecture variant.
- Make dependencies point inward: Api/Infrastructure depend on Application/Domain; Application does not depend on Infrastructure.
- Move repository contracts to the application/business layer.
- Keep EF Core usage isolated in DataAccess/Infrastructure.
- Preserve the same API behavior baseline and pending lab functionality state.
- Verify that the `clean/` solution builds.

**Non-Goals:**

- Rename projects or namespaces to `Domain`, `Application`, `Infrastructure`, or `WebApi`.
- Implement missing HackerRank endpoints (`POST books`, `DELETE library`, etc.).
- Move DTOs/settings out of `Entities` in this change.
- Modify the `n-layer/` folder.
- Remove the Supabase/Postgres configuration or rewrite auth.

## Decisions

### Decision: Use existing project names as Clean Architecture roles

The folders and `.csproj` names remain `HackerRank1.Entities`, `HackerRank1.BusinessLogic`, `HackerRank1.DataAccess`, and `HackerRank1.Api`. This keeps the course project recognizable while changing dependency direction to match Clean Architecture.

Alternative considered: rename projects to Domain/Application/Infrastructure/Web. That is clearer architecturally but adds solution and namespace churn not needed for this lab variant.

### Decision: Application owns repository contracts

Repository interfaces belong in `HackerRank1.BusinessLogic/Repositories`. Business services depend on those interfaces. DataAccess implements them.

Alternative considered: keep interfaces in DataAccess as in classic N-layer. That is acceptable for N-layer but not Clean Architecture, because the application layer would still depend on infrastructure.

### Decision: DataAccess references BusinessLogic

DataAccess will reference BusinessLogic to implement application-owned ports. BusinessLogic will remove its project reference to DataAccess.

Alternative considered: add a separate abstractions project. That is cleaner for larger systems but unnecessary for the assignment and introduces a fifth project.

### Decision: Api remains composition root

Api continues to reference both BusinessLogic and DataAccess so `Startup.cs` can bind application contracts to infrastructure implementations. Controllers keep depending on business services, not repositories.

Alternative considered: move DI extension methods into DataAccess. This is useful later, but keeping composition in Api is simpler and explicit.

### Decision: Preserve behavior baseline

This change is architectural. Pending functionality remains pending. Existing tests may still fail for known missing endpoints; those failures do not block the Clean Architecture refactor if the solution builds and the dependency direction is correct.

## Risks / Trade-offs

- Project names do not use Clean Architecture terminology -> mitigated by documenting logical roles in design and code structure.
- `Entities` still contains DTOs/settings -> accepted temporarily to avoid breaking tests and keep scope focused.
- Existing appsettings secrets remain a separate security concern -> not handled here because this change is architecture-only.
- Tests may fail after build -> expected if failures map to the already-known missing HackerRank endpoints.

## Migration Plan

1. Ensure `clean/` contains the project baseline without build artifacts.
2. Move repository interfaces into BusinessLogic/Application.
3. Remove BusinessLogic -> DataAccess project reference.
4. Add DataAccess -> BusinessLogic project reference.
5. Update repository implementations to implement application-owned interfaces.
6. Update services and Api DI imports/registrations.
7. Build and test from `clean/`, documenting expected out-of-scope failures.

Rollback: delete or restore the `clean/` folder from the previous baseline; `n-layer/` remains unaffected.
