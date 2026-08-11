## Why

The course project now has multiple architecture variants of the same library API (`n-layer/` and `clean/`). A third variant is needed to demonstrate Vertical Slice Architecture, where code is organized by feature/use case rather than by technical layer.

## What Changes

- Build a new Vertical Slice Architecture variant inside `vertical-slice/`, using the same library API domain and functional baseline as the existing variants.
- Preserve route behavior, entity shapes, DTO compatibility, tests, and the known pending HackerRank endpoint state unless a future change explicitly changes behavior.
- Organize code by vertical features/slices, such as `Libraries` and `Books`, where each slice owns its request handlers, service logic, data access needed for that feature, and endpoint wiring.
- Keep shared cross-cutting infrastructure minimal and explicit, such as database context, JWT settings/token generation, dependency injection setup, and common models.
- Ensure `vertical-slice/` is independent from `n-layer/` and `clean/`; this change must not modify the other architecture variants.
- Verify the `vertical-slice/` solution builds, and run tests while documenting expected failures caused by still-pending endpoint implementations.

## Capabilities

### New Capabilities

- `architecture/vertical-slice`: Defines the Vertical Slice Architecture rules and responsibilities for the `vertical-slice/` variant.

### Modified Capabilities

- None. Existing main specs cover `architecture/n-layer`, `architecture/clean`, and `api/books`; this change introduces a new architecture variant.

## Impact

- `vertical-slice/`: contains the new Vertical Slice Architecture variant of the library API solution.
- Feature-oriented folders/slices will replace layer-oriented project organization inside the variant.
- Shared infrastructure remains available only where needed by slices.
- `n-layer/` and `clean/` are not modified.
- Existing OpenSpec main specs are extended with a new `architecture/vertical-slice` capability after archive sync.
