
# **Payment Processing API**

A .NET 6 Web API designed for payment processing and user authentication using JWT tokens. The project incorporates role-based authorization, database integration, and payment status handling with transaction support.

---

## **Features**

### 1. **Authentication and Authorization**
- User login with JWT token generation.
- Role-based access control (e.g., Admin, User).
- JWT token validation and claim-based user identification.

### 2. **Payment Workflow**
- **Initiate Payment**: Creates a payment record with status `Pending`.
- **Complete Payment**: Updates the status to `Succeed` upon successful processing and deducts balance from payer and add balance to recipient.
- **Cancel Payment**: Marks the payment status as `Failed`.
- **Retry Payment**: Retry the `Failed` payment to maximum of 5 times.

### 3. **Database Integration**
- Entity Framework Core with SQL Server for database access.
- Transaction support to ensure data integrity.

### 4. **API Documentation**
- Swagger UI integration for API testing.

---

## **Technologies Used**
- **Framework**: .NET 6
- **Database**: SQL Server
- **ORM**: Entity Framework Core
- **Authentication**: JWT (JSON Web Tokens)
- **API Documentation**: Swagger (Swashbuckle)

---

## **Setup Instructions**

### **1. Prerequisites**
- .NET 6 SDK
- SQL Server (LocalDB or other instance)
- Visual Studio 2022 or Visual Studio Code

---

### **2. Clone the Repository**
```bash
git clone https://github.com/Prateekgaur/PaymentsAPI.git
cd PaymentsAPI
```

---

### **3. Configure `appsettings.json`**
Update the following in the `appsettings.json` file:

```json
{
  "Jwt": {
    "Key": "YourSuperSecureKey12345",
    "Issuer": "PaymentsAPI",
    "Audience": ["User", "Admin"],
    "ExpiryInHours": 24
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=PaymentsAPI;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*"
}
```

---

### **4. Run Database Migrations**
Run the following commands to set up the database schema:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

---

### **5. Run the Application**
Start the API server:

```bash
dotnet run
```

---

### **6. Access Swagger UI**
Swagger UI is available at:

[http://localhost:5000](http://localhost:5000)

---

## **Endpoints**

### **Authentication**
1. **POST** `/api/auth/login`  
   Authenticate a user and generate a JWT token.  
   **Request Body**:
   ```json
   {
     "username": "testuser",
     "password": "password123"
   }
   ```
   **Response**:
   ```json
   {
     "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
   }
   ```

---

## **Folder Structure**

```
PaymentsAPI/
├── Controllers/
│   ├── AuthController.cs
│   ├── PaymentsController.cs
├── Data/
│   ├── AppDbContext.cs
├── DTOs/
│   ├── LoginRequestDto.cs
│   ├── PaymentRequestDto.cs
├── Migrations/
├── Models/
│   ├── Payment.cs
│   ├── User.cs
├── Services/
│   ├── CurrentUserService.cs
│   ├── IPaymentService.cs
│   ├── IUserService.cs
│   ├── PaymentService.cs
│   ├── UserService.cs
├── Utilities/
│   ├── Utilities.cs
├── appsettings.json
├── Program.cs

```

---

## **Design Decisions**
1. **JWT Authentication**:
   - Chosen for stateless and secure user authentication.
   - Includes user-specific claims for role-based authorization.

2. **Transaction Support**:
   - Ensures atomicity for payment-related operations.

3. **Extensibility**:
   - Payment processing logic is modular, allowing integration with external payment gateways.

---

## **Testing**
Use **Swagger UI** or **Postman** to test the API endpoints:
1. Authenticate using `/api/auth/login`.
2. Add the JWT token in the `Authorization: Bearer <token>` header for secured endpoints.


