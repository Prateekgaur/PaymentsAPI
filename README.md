Payment Processing API
This project is a .NET Core Web API designed to handle user authentication and payment processing with robust authentication and database integration. The API uses JWT tokens for secure authorization and follows good coding practices.

Features Implemented So Far
User Authentication

Endpoint: /api/auth/login
Users can log in using a username and password to obtain a JWT token for authorization.
Role-based authorization for sensitive operations.
JWT Token-Based Authentication

Secure user authentication using JWT tokens.
Tokens include role-based claims (e.g., Admin, User) for scoped actions.
Database Integration

Entity Framework Core is used for database access.
Models for User and Payment have been implemented with appropriate relationships.
SQL Server (LocalDB) is used for the database.
Partial Payment Functionality

The groundwork for payment processing has been laid, including:
Model: Payment with fields like Amount, RecipientId, PaymentMethod, and Status.
Validation for the POST /payments endpoint is ongoing.
Technologies Used
Backend Framework: ASP.NET Core
Database: SQL Server (LocalDB)
Authentication: JWT (JSON Web Tokens)
ORM: Entity Framework Core
API Documentation: Swagger (Swashbuckle)
IDE: Visual Studio 2022
Version Control: GitHub
Getting Started

 Prerequisites
.NET 6 SDK or later
SQL Server (LocalDB or another instance)
Visual Studio 2022 or Visual Studio Code
Postman or another HTTP client for testing

