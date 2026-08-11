## Context

See `proposal.md` for motivation. The repository now contains architecture variants for the same library API domain: `n-layer/` and `clean/`. The new `vertical-slice/` variant should demonstrate organizing behavior around features/use cases instead of technical layers.

The target structure keeps the same .NET API domain but changes code organization within `vertical-slice/`:

```text
vertical-slice/
  HackerRank1.sln
  src/HackerRank1.Api/
    Features/
      Libraries/
        GetLibraries.cs
        GetLibrary.cs
        CreateLibrary.cs
        UpdateLibrary.cs
      Books/
        GetBooks.cs
        AddBook.cs
    Infrastructure/
      LibraryContext.cs
      Migrations/
      Auth/
      Settings/
    Domain/
      Library.cs
      Book.cs
    Program.cs / Startup.cs
  tests/HackerRank1.Tests/
```

Exact filenames can vary during implementation, but the organizing principle is fixed: slices own use-case behavior.

## Goals / Non-Goals

**Goals:**

- Create `vertical-slice/` as an independent architecture variant.
- Keep `n-layer/` and `clean/` untouched.
- Organize API behavior by feature/use case rather than controller/service/repository layers.
- Keep shared infrastructure explicit and minimal.
- Preserve the functional baseline, including known pending endpoint state.
- Verify that the `vertical-slice/` solution builds.

**Non-Goals:**

- Implement missing HackerRank endpoints or make the tests fully green.
- Introduce MediatR or a new external library unless implementation proves it necessary.
- Rename the domain, DTO, or test namespaces if doing so would break compatibility.
- Change Supabase/Postgres configuration, JWT behavior, or migrations beyond what is needed to compile the variant.
- Modify `n-layer/` or `clean/`.

## Decisions

### Decision: Start from an existing working variant

Implementation should copy a baseline solution into `vertical-slice/` before reorganizing. The closest baseline is `n-layer/` because it already builds and preserves the original lab shape.

Alternative considered: start from `clean/`. That would bring dependency inversion structure that is less representative of Vertical Slice's goal of colocating use-case behavior.

### Decision: Single API project can host slices

The Vertical Slice variant can use a simpler project layout than N-layer/Clean by placing features and shared infrastructure inside one API project. This emphasizes feature locality and avoids artificial cross-project layering.

Alternative considered: keep four projects and add `Features/` inside BusinessLogic. That is less clear as a Vertical Slice example because technical layers still dominate the solution shape.

### Decision: Slice files own endpoint/use-case logic

Each feature slice should define its endpoint mapping or controller action, request/response contract, validation if any, use-case logic, and slice-specific persistence operations close together.

Alternative considered: retain controllers, services, and repositories while grouping by feature. This is a partial slice approach, but it weakens the contrast with N-layer.

### Decision: Avoid adding MediatR by default

Handlers can be plain classes or methods. MediatR is common in Vertical Slice examples, but adding a dependency is unnecessary for a small lab variant unless explicitly required later.

Alternative considered: introduce MediatR handlers. This increases ceremony and package surface for little value in this project.

### Decision: Preserve behavior baseline

This is an architecture variant. Pending functionality remains pending unless a future change asks to complete the HackerRank endpoints.

## Risks / Trade-offs

- One-project Vertical Slice may look less layered than the previous variants -> this is intentional and highlights feature locality.
- Existing DTOs/entities may remain close to the original names -> acceptable to preserve tests and reduce churn.
- Tests may still fail -> expected if failures match known missing endpoint behavior.
- Copying baseline files can accidentally include `bin/`, `obj/`, or `.vs/` -> exclude build artifacts during implementation.

## Migration Plan

1. Confirm `vertical-slice/` exists and is empty or safe to populate.
2. Copy the chosen baseline into `vertical-slice/` without build artifacts.
3. Restructure source around `Features/<Feature>/<UseCase>.cs` slices.
4. Move shared persistence/auth/settings/domain code into explicit shared areas.
5. Update project references and namespaces to match the simplified vertical-slice layout.
6. Build and run tests from `vertical-slice/`, documenting expected out-of-scope failures.

Rollback: remove or restore `vertical-slice/`; other architecture variants remain unaffected.
