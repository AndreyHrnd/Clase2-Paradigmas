## 1. Baseline Variant Setup

- [x] 1.1 Confirm `clean/` contains an independent copy of the library API solution.
- [x] 1.2 Ensure copied build artifacts (`bin/`, `obj/`, `.vs/`) are not part of the variant source.
- [x] 1.3 Confirm `n-layer/` remains unchanged by the Clean Architecture work.

## 2. Application Contracts

- [x] 2.1 Create application-owned repository contracts under `clean/src/HackerRank1.BusinessLogic/Repositories`.
- [x] 2.2 Update `LibrariesService` to use the application-owned library repository contract.
- [x] 2.3 Update `BooksService` to use the application-owned book repository contract.
- [x] 2.4 Remove the BusinessLogic project reference to DataAccess.

## 3. Infrastructure Implementations

- [x] 3.1 Remove repository interfaces from `clean/src/HackerRank1.DataAccess/Repositories`.
- [x] 3.2 Update DataAccess repository implementations to import and implement BusinessLogic repository contracts.
- [x] 3.3 Add the DataAccess project reference to BusinessLogic so infrastructure can implement application contracts.
- [x] 3.4 Keep EF Core context, migrations, and provider packages inside DataAccess.

## 4. Api Composition Root

- [x] 4.1 Update `Startup.cs` imports to use repository contracts from BusinessLogic and implementations from DataAccess.
- [x] 4.2 Register repository implementations against application-owned contracts with scoped lifetime.
- [x] 4.3 Confirm controllers depend on business services rather than repositories or data context.

## 5. Verification

- [x] 5.1 Run `dotnet build` from `clean/` and confirm the solution compiles.
- [x] 5.2 Run `dotnet test` from `clean/` and record remaining failures as out-of-scope if they match known pending endpoint implementations.
- [x] 5.3 Verify BusinessLogic has no reference to DataAccess or EF Core packages.
- [x] 5.4 Verify `n-layer/` was not modified by this change.
