# Canvas

Canvas is a full-stack Trello alternative.

> Do note, Canvas is currently in active Development—and is therefore, nowhere near finished.

---

## Architecture

The project follows Clean Architecture principles with the following layers:

- **Canvas.Domain**: Domain entities and business logic
- **Canvas.Application**: Application layer with CQRS-style commands and handlers
- **Canvas.Infrastructure**: Data persistence, repositories, and external service implementations
- **Canvas.Api**: REST API controllers and middleware

## Backend Features

### Authentication

- User registration with email validation
- User login with JWT access tokens and refresh tokens
- Session refresh mechanism
- Email verification via verification tokens
- Password reset flow (forgot password + reset password)

### Security

- Argon2 password hashing
- JWT token-based authentication
- Session management with token revocation
- Email verification tokens with expiration
- Password reset tokens with expiration

### Domain Entities

- **User**: First name, last name, email, password hash, verification status, active status
- **Session**: Refresh token management with expiration and revocation
- **VerificationToken**: Supports email verification and password reset token types

### Infrastructure

- PostgreSQL database with Entity Framework Core
- Repository pattern for data access
- Email repository interface (implementation ready)
- Exception handling middleware
- Health check endpoint (`/api/health`)

### API Documentation

- Scalar API documentation integrated

## Frontend

### Mobile (Flutter)

- Flutter with Dart SDK ^3.10.4
- Cross-platform mobile application (iOS, Android, Web, Linux, macOS, Windows)
- Material Design support

### Web (Angular)

- Angular 21.0.0
- Angular Material for UI components
- SCSS for styling
- TypeScript

## Development Setup

### Database

- PostgreSQL 16 via Docker Compose
- Adminer database management tool (port 8080)
- Database migrations configured

### Technology Stack

**Backend:**

- .NET 10.0
- ASP.NET Core
- Entity Framework Core
- PostgreSQL
- JWT Bearer Authentication
- Argon2 password hashing

**Frontend:**

- **Mobile**: Flutter (Dart SDK ^3.10.4)
- **Web**: Angular 21.0.0, Angular Material, SCSS, TypeScript
