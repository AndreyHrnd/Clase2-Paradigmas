## Purpose

Defines the authentication behavior for the books list API endpoint in the library service so it remains compatible with the HackerRank contract and integration tests.

## ADDED Requirements

### Requirement: Books list endpoint is anonymous
The system SHALL allow clients to call `GET /api/libraries/{libraryId}/books` without providing a JWT token.

#### Scenario: Anonymous books list request
- **WHEN** a client requests `GET /api/libraries/{libraryId}/books` without an `Authorization` header
- **THEN** the request is not rejected solely because the client is unauthenticated

#### Scenario: JWT support remains available elsewhere
- **WHEN** authentication middleware is configured for the API
- **THEN** removing authentication from the books list endpoint does not remove the `/login` token flow
