## 1. Baseline Variant Setup

- [x] 1.1 Confirm `vertical-slice/` exists and is empty or safe to populate.
- [x] 1.2 Copy the project baseline from `n-layer/` into `vertical-slice/` without `bin/`, `obj/`, or `.vs/` artifacts.
- [x] 1.3 Confirm `n-layer/` and `clean/` remain unchanged by the Vertical Slice work.

## 2. Project Shape

- [x] 2.1 Simplify the variant toward a Vertical Slice layout centered on `src/HackerRank1.Api`.
- [x] 2.2 Create `Features/Libraries` for library use cases.
- [x] 2.3 Create `Features/Books` for book use cases.
- [x] 2.4 Create shared areas for `Domain`, `Infrastructure`, `Settings`, and auth/token concerns where needed.
- [x] 2.5 Update the solution/project references after removing layer-oriented projects if they are no longer needed.

## 3. Slice Organization

- [x] 3.1 Move library endpoint/use-case behavior into library feature slices.
- [x] 3.2 Move books endpoint/use-case behavior into books feature slices.
- [x] 3.3 Keep slice-specific query/command logic close to the slice rather than in generic services/repositories.
- [x] 3.4 Preserve current route paths and response behavior unless already pending/out-of-scope.

## 4. Shared Infrastructure

- [x] 4.1 Move or keep `LibraryContext` as shared infrastructure used by slices.
- [x] 4.2 Keep migrations and database provider configuration in infrastructure.
- [x] 4.3 Keep JWT/login behavior available without spreading auth logic across feature slices.
- [x] 4.4 Preserve DTO/entity compatibility expected by tests.

## 5. Verification

- [x] 5.1 Run `dotnet build` from `vertical-slice/` and confirm the solution compiles.
- [x] 5.2 Run `dotnet test` from `vertical-slice/` and record remaining failures as out-of-scope if they match known pending endpoint implementations.
- [x] 5.3 Verify feature use cases live under `Features/*` rather than technical service/repository layers.
- [x] 5.4 Verify `n-layer/` and `clean/` were not modified.
