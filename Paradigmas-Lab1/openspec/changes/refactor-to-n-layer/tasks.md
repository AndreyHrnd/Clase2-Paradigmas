## 1. DataAccess Repositories

- [x] 1.1 Create `HackerRank1.DataAccess/Repositories/ILibrariesRepository.cs` with the existing library persistence contract.
- [x] 1.2 Create `HackerRank1.DataAccess/Repositories/LibrariesRepository.cs` and move EF Core library operations from `LibrariesService` into it.
- [x] 1.3 Create `HackerRank1.DataAccess/Repositories/IBooksRepository.cs` with the existing book persistence contract.
- [x] 1.4 Create `HackerRank1.DataAccess/Repositories/BooksRepository.cs` and move EF Core book query behavior from `BooksService` into it.

## 2. BusinessLogic Refactor

- [x] 2.1 Refactor `LibrariesService` to inject `ILibrariesRepository` and remove direct `LibraryContext` usage.
- [x] 2.2 Refactor `BooksService` to inject `IBooksRepository` and remove direct `LibraryContext` usage.
- [x] 2.3 Preserve existing service method signatures and keep currently unimplemented methods unimplemented.

## 3. Api Composition

- [x] 3.1 Register repository interfaces and implementations in `Startup.cs` with scoped lifetime.
- [x] 3.2 Change `ILibrariesService` and `IBooksService` registrations from transient to scoped.
- [x] 3.3 Remove `[Authorize]` from `BooksController.GetAll` only.

## 4. Verification

- [x] 4.1 Run `dotnet build` from `n-layer` and confirm the solution compiles.
- [x] 4.2 Run `dotnet test` and record remaining failures as out-of-scope if they are caused by the known pending endpoint implementations.
- [x] 4.3 Confirm the books GET endpoint is no longer protected by `[Authorize]`.
