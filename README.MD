# 🏪 Supermarket API

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

## 🔧 Configuration

### Database Connection
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Initial Catalog=supermarket;User ID=sa;Password=Tay52636@;TrustServerCertificate=True;Connection Timeout=30;"
  }
}
```

### Launch Settings
- **URL:** `http://localhost:5295`
- **Swagger:** `http://localhost:5295/swagger`
- **Environment:** Development

## 📡 API Endpoints

### Test Endpoints
- `GET /api/WeatherForecast` - Test endpoint
- `GET /api/WeatherForecast/test-db` - Database connection test

### Business Endpoints
- `GET /api/Product` - Product management
- `GET /api/NhanVien` - Employee management

## 🛠️ Troubleshooting

### Port 5295 already in use
```bash
# Kill processes using port 5295
lsof -ti:5295 | xargs -r kill -9

# Or use the run script
./run.sh
```

### Database connection issues
1. Check SQL Server is running on localhost:1433
2. Verify database `supermarket` exists
3. Check credentials: sa/Tay52636@

## 📁 Project Structure
```
Supermarket/
├── Controllers/          # API Controllers
├── Models/              # Data Models
├── DTOs/                # Data Transfer Objects
├── Services/            # Business Logic
├── Repositories/        # Data Access
├── Data/               # Database Context
├── Extensions/         # Custom Extensions
└── run.sh             # Run Script
```

## 🔄 Database Migrations
```bash
# Add migration
dotnet ef migrations add MigrationName

# Update database
dotnet ef database update
```