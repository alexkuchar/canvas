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

### Authentication API Endpoints

- `POST /api/auth/register` - User registration with email validation
- `POST /api/auth/login` - User login with JWT access tokens and refresh tokens
- `POST /api/auth/refresh` - Session refresh mechanism
- `POST /api/auth/verify` - Email verification via verification tokens
- `POST /api/auth/forgot-password` - Initiate password reset flow
- `POST /api/auth/reset-password` - Complete password reset with token
- `POST /api/auth/resend-verification-email` - Resend email verification

### User Management

- `PUT /api/users/update` - Update user profile (first name, last name, email, password)
- Protected endpoints require JWT authentication

### Security

- Argon2 password hashing
- JWT token-based authentication with access and refresh tokens
- Session management with token revocation
- Email verification tokens with expiration
- Password reset tokens with expiration
- Automatic token refresh on 401 responses

### Domain Entities

- **User**: First name, last name, email, password hash, verification status, active status
- **Session**: Refresh token management with expiration and revocation
- **VerificationToken**: Supports email verification and password reset token types

### Infrastructure

- PostgreSQL 16 database with Entity Framework Core
- Repository pattern for data access
- Email repository interface (implementation ready)
- Exception handling middleware
- Health check endpoint (`/api/health`)
- Scalar API documentation integrated

## Frontend

### Mobile (Flutter)

**Technology Stack:**

- Flutter with Dart SDK ^3.10.4
- Cross-platform support: iOS, Android, Web, Linux, macOS, Windows
- Material Design 3

**Key Dependencies:**

- `dio` (^5.9.0) - HTTP client with interceptors
- `flutter_secure_storage` (^10.0.0) - Secure token storage
- `open_mail` (^1.2.1) - Email client integration
- `http` (^1.1.0) - Additional HTTP utilities

**Implemented Features:**

- **Authentication Screens:**
  - Onboarding screen
  - User registration with validation
  - Login with email/password
  - Email verification notice
  - Forgot password flow
- **Dashboard:**
  - Protected dashboard overview screen
  - Navigation structure with bottom navigation bar
- **Services:**
  - `AuthService` - Complete authentication flow (register, login, forgot password, resend verification)
  - `StorageService` - Secure storage for tokens and user data
  - `ApiService` - Centralized API configuration
- **Security:**
  - Auth guard for protected routes
  - Automatic token refresh via interceptor
  - Secure token storage
  - Error handling with user-friendly messages

### Web (Angular)

**Technology Stack:**

- Angular 21.0.0
- Angular Material 21.0.5 for UI components
- SCSS for styling
- TypeScript 5.9.2

**Implemented Features:**

- **Authentication Pages:**
  - User registration
  - Login with form validation
  - Forgot password
  - Reset password
  - Email verification
- **Dashboard:**
  - Protected dashboard layout
  - Overview page
  - Settings page
- **Core Services:**
  - `AuthService` - Authentication and user management
  - `UserService` - User profile operations
- **Security:**
  - Auth guard for protected routes
  - Guest guard for public routes
  - Auth interceptor with automatic token refresh
  - HTTP error handling utilities
- **Routing:**
  - Feature-based routing structure
  - Protected and public route guards

## Development Setup

### Database

- PostgreSQL 16 via Docker Compose
- Adminer database management tool (port 8080)
- Database migrations configured with Entity Framework Core

### Technology Stack

**Backend:**

- .NET 10.0
- ASP.NET Core
- Entity Framework Core 10.0.1
- Npgsql.EntityFrameworkCore.PostgreSQL 10.0.0
- PostgreSQL 16
- JWT Bearer Authentication (System.IdentityModel.Tokens.Jwt 8.15.0)
- Argon2 password hashing (Konscious.Security.Cryptography.Argon2 1.3.1)
- Scalar.AspNetCore 2.11.9 for API documentation

**Frontend:**

- **Mobile**: Flutter (Dart SDK ^3.10.4), Dio, Flutter Secure Storage
- **Web**: Angular 21.0.0, Angular Material 21.0.5, SCSS, TypeScript 5.9.2
