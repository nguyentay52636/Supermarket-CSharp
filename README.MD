# 🏪 Supermarket API - Backend System

## 📖 Mô tả dự án

**Supermarket API** là hệ thống backend quản lý siêu thị được xây dựng bằng **.NET 9.0** và **ASP.NET Core Web API**. Hệ thống cung cấp các API endpoints để quản lý nhân viên, sản phẩm và các hoạt động kinh doanh của siêu thị.
UML :https://dbdiagram.io/d/supermaket-68d8bf5dd2b621e42239053d
## 🛠️ Công nghệ sử dụng

### **Core Framework**
- **.NET 9.0** - Framework chính
- **ASP.NET Core Web API** - RESTful API framework
- **C# 12** - Ngôn ngữ lập trình

### **Database & ORM**
- **SQL Server** - Hệ quản trị cơ sở dữ liệu
- **Entity Framework Core 9.0** - ORM framework
- **Code First Migration** - Quản lý database schema

### **Architecture Pattern**
- **Repository Pattern** - Tách biệt data access logic
- **Service Layer Pattern** - Business logic layer
- **DTO Pattern** - Data Transfer Objects
- **Dependency Injection** - IoC container

### **API Documentation**
- **Swagger/OpenAPI** - API documentation
- **XML Comments** - Code documentation
- **ApiResponse Wrapper** - Standardized response format

### **Development Tools**
- **Entity Framework CLI** - Database migrations
- **SQL Server Management Studio** - Database management
- **Postman/curl** - API testing

## 🚀 Quick Start

### Cách 1: Sử dụng script (Khuyến nghị)
```bash
./run.sh
```

### Cách 2: Chạy trực tiếp
```bash
dotnet run
```

## 📋 Prerequisites
- .NET 9.0 SDK
- SQL Server (localhost:1433)
- Database: `supermarket`


## 🚀 Deployment

### **Development Environment**
- **URL:** `http://localhost:5295`
- **Database:** SQL Server (localhost:1433)
- **Environment:** Development

### **Production Considerations**
- **HTTPS Configuration** - SSL/TLS certificates
- **Database Security** - Connection string encryption
- **API Rate Limiting** - Prevent abuse
- **Logging & Monitoring** - Application insights
- **Health Checks** - System monitoring

## 👥 Team & Contact

**Developer:** [TYT]  
**Framework:** .NET 9.0 + ASP.NET Core  
**Database:** SQL Server  
**Documentation:** Swagger/OpenAPI  

---

*Dự án được phát triển với .NET 9.0 và ASP.NET Core Web API cho hệ thống quản lý siêu thị.*