# EShop API - Comprehensive E-Commerce Solution

A modern, secure, and scalable e-commerce API built with ASP.NET Core 8, Entity Framework, and JWT authentication.

## 🚀 Features

### ✅ **Security & Authentication**
- **JWT Token Authentication** with secure password hashing using BCrypt
- **Role-based Authorization** (Admin, User roles)
- **Input Validation** with Data Annotations
- **CORS Support** for cross-origin requests

### ✅ **Database & Data Management**
- **Entity Framework Core** with SQL Server
- **Code-First Approach** with automatic migrations
- **Soft Delete** functionality for data integrity
- **Audit Trail** with creation/update timestamps

### ✅ **API Features**
- **RESTful Design** following best practices
- **Comprehensive Error Handling** with structured responses
- **Logging** with structured logging
- **Swagger Documentation** with JWT support
- **Async/Await** throughout the application

### ✅ **E-Commerce Functionality**
- **Product Management** with categories and stock tracking
- **User Registration & Authentication**
- **Shopping Cart** functionality
- **Order Management** with status tracking
- **Payment Processing** with multiple payment methods
- **Invoice Generation**

## 🏗️ Architecture

```
EShop/
├── EShop/                    # API Layer (Controllers, Models)
├── EShop.Application/        # Business Logic Layer (Services)
├── EShop.Domain/            # Domain Layer (Entities, Repositories)
└── EShop.Application3.Tests/ # Test Projects
```

### **Clean Architecture Principles**
- **Separation of Concerns** with clear layer boundaries
- **Dependency Injection** for loose coupling
- **Repository Pattern** for data access
- **Service Layer** for business logic

## 🛠️ Technology Stack

- **.NET 8** - Latest framework with performance improvements
- **ASP.NET Core** - Modern web framework
- **Entity Framework Core** - ORM for database operations
- **SQL Server** - Relational database
- **JWT Bearer Tokens** - Stateless authentication
- **BCrypt** - Secure password hashing
- **Swagger/OpenAPI** - API documentation
- **Serilog** - Structured logging

## 📋 Prerequisites

- **.NET 8 SDK**
- **SQL Server** (LocalDB, Express, or full version)
- **Visual Studio 2022** or **Rider** (recommended)

## 🚀 Getting Started

### 1. **Clone the Repository**
```bash
git clone <repository-url>
cd pp4_project
```

### 2. **Database Setup**
The application uses Entity Framework Code-First approach. The database will be created automatically on first run.

**Connection String** (update in `appsettings.json`):
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=EShopDb;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

### 3. **Run the Application**
```bash
cd EShop
dotnet run
```

### 4. **Access the API**
- **API Base URL**: `https://localhost:7001` or `http://localhost:5001`
- **Swagger Documentation**: `https://localhost:7001` (root URL)
- **Health Check**: `https://localhost:7001/health`

## 🔐 Authentication

### **Registration**
```http
POST /api/auth/register
Content-Type: application/json

{
  "username": "john_doe",
  "email": "john@example.com",
  "password": "SecurePassword123!",
  "name": "John Doe",
  "phoneNumber": "+1234567890",
  "address": "123 Main St, City, Country"
}
```

### **Login**
```http
POST /api/auth/login
Content-Type: application/json

{
  "username": "john_doe",
  "password": "SecurePassword123!"
}
```

### **Using JWT Token**
```http
GET /api/products
Authorization: Bearer <your-jwt-token>
```

## 📚 API Endpoints

### **Authentication**
- `POST /api/auth/register` - User registration
- `POST /api/auth/login` - User login
- `GET /api/auth/profile` - Get user profile (authenticated)

### **Products** (Public)
- `GET /api/products` - Get all products
- `GET /api/products/{id}` - Get product by ID
- `GET /api/products/category/{categoryId}` - Get products by category

### **Products** (Admin Only)
- `POST /api/products` - Create new product
- `PUT /api/products/{id}` - Update product
- `DELETE /api/products/{id}` - Delete product

### **Categories** (Public)
- `GET /api/categories` - Get all categories
- `GET /api/categories/{id}` - Get category by ID

### **Categories** (Admin Only)
- `POST /api/categories` - Create new category
- `PUT /api/categories/{id}` - Update category
- `DELETE /api/categories/{id}` - Delete category

### **Orders** (Authenticated)
- `GET /api/order` - Get user orders
- `GET /api/order/{id}` - Get order by ID
- `POST /api/order/create/{clientId}` - Create new order
- `GET /api/order/payment-methods` - Get available payment methods

### **Cart** (Authenticated)
- `GET /api/cart/{clientId}` - Get user cart
- `POST /api/cart/add` - Add item to cart
- `DELETE /api/cart/remove` - Remove item from cart
- `POST /api/cart/clear` - Clear cart

## 🔧 Configuration

### **JWT Settings** (`appsettings.json`)
```json
{
  "JwtSettings": {
    "SecretKey": "your-super-secret-key-with-at-least-32-characters",
    "Issuer": "EShop",
    "Audience": "EShopUsers",
    "ExpirationInMinutes": 60
  }
}
```

### **Database Connection**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=EShopDb;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

## 🧪 Testing

### **Run Tests**
```bash
cd EShop.Application3.Tests
dotnet test
```

### **API Testing with Swagger**
1. Open `https://localhost:7001` in your browser
2. Use the interactive Swagger UI to test endpoints
3. Authenticate using the `/api/auth/login` endpoint
4. Copy the JWT token and click "Authorize" in Swagger
5. Test protected endpoints

## 📊 Data Models

### **Core Entities**
- **User** - Authentication and user management
- **Client** - Customer information
- **Product** - Product catalog with categories
- **Category** - Product categorization
- **Order** - Order management with status tracking
- **Cart** - Shopping cart functionality
- **Payment** - Payment processing
- **Invoice** - Invoice generation

### **Audit Trail**
All entities inherit from `BaseModel` which includes:
- `CreatedAt` - Creation timestamp
- `UpdatedAt` - Last update timestamp
- `CreatedBy` - Creator ID
- `UpdatedBy` - Last updater ID
- `IsDeleted` - Soft delete flag

## 🔒 Security Features

### **Password Security**
- **BCrypt Hashing** with salt rounds
- **Minimum Password Requirements** (6+ characters)
- **Secure Password Verification**

### **JWT Security**
- **Signed Tokens** with HMAC-SHA256
- **Token Expiration** (configurable)
- **Issuer/Audience Validation**
- **Clock Skew Protection**

### **Input Validation**
- **Data Annotations** for model validation
- **Custom Validation Logic** in services
- **SQL Injection Protection** via EF Core

## 🚀 Deployment

### **Docker Support**
The project includes Docker configuration for containerized deployment.

### **Environment Configuration**
- **Development**: Uses LocalDB
- **Production**: Configure production database connection
- **Secrets Management**: Use User Secrets for sensitive data

## 📈 Performance Considerations

- **Async/Await** throughout the application
- **Entity Framework** with optimized queries
- **Connection Pooling** for database efficiency
- **Caching** ready for implementation

## 🔄 Future Enhancements

- **Email Verification** for user registration
- **Password Reset** functionality
- **Two-Factor Authentication** (2FA)
- **API Rate Limiting**
- **Redis Caching**
- **Payment Gateway Integration**
- **Order Notifications**
- **Inventory Management**
- **Discount/Promotion System**

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests for new functionality
5. Submit a pull request

## 📄 License

This project is licensed under the MIT License.

## 🆘 Support

For support and questions:
- Create an issue in the repository
- Check the Swagger documentation
- Review the API response formats

---

**Built with ❤️ using ASP.NET Core 8**
