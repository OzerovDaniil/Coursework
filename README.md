# Sushi Management System

## Overview

**Sushi Management System** is a web-based application designed to automate and optimize the core business processes of a sushi restaurant. The system enables efficient management of orders, menu, customers, employees, and inventory through a modern web interface and a robust RESTful API.

> **Note:** The frontend part of this project is a basic implementation and serves as a demonstration of API integration. It does not include advanced UI/UX features or full production-level functionality.

---

## Table of Contents

- [Features](#features)
- [Project Structure](#project-structure)
- [Technologies Used](#technologies-used)
- [Getting Started](#getting-started)
- [API Documentation](#api-documentation)
- [Usage Examples](#usage-examples)
- [Authentication & Authorization](#authentication--authorization)
- [Contributing](#contributing)
- [FAQ](#faq)
- [License](#license)

---

## Features

- **Order Management:** Create, view, update, and delete orders.
- **Menu Management:** Add, edit, and remove menu items.
- **Customer & Employee Management:** Track and manage customers and staff.
- **Inventory Tracking:** Monitor and update stock levels.
- **Role-based Access Control:** Admin and employee roles with different permissions.
- **Authentication & Authorization:** Secure login with JWT tokens.
- **Data Validation & Error Handling:** Robust server-side validation and error responses.
- **Logging:** Event and error logging with Serilog.
- **API Documentation:** Interactive API docs via Swagger.
- **Simple Frontend:** Clean HTML/CSS/JS interface for basic operations.

---

## Project Structure

```
Coursework/
│
├── front/                        # Frontend (HTML, CSS, JS)
│   ├── components/               # UI components
│   ├── css/                      # Stylesheets
│   ├── images/                   # Images
│   ├── js/                       # JavaScript files
│   └── pages/                    # HTML pages
│
├── SushiManagementSystem.API/    # ASP.NET Core Web API
│   ├── Controllers/              # API controllers
│   ├── Filters/                  # Validation & exception filters
│   └── ...
│
├── SushiManagementSystem.Application/   # Business logic, DTOs, services
│   ├── DTOs/
│   ├── Interfaces/
│   ├── Services/
│   └── ...
│
├── SushiManagementSystem.Domain/        # Domain entities
│   └── Entities/
│
├── SushiManagementSystem.Infrastructure/ # Data access, repositories, DI
│   ├── Configurations/
│   ├── Data/
│   ├── Persistence/
│   └── ...
│
└── Coursework.sln                # Visual Studio solution file
```

---

## Technologies Used

### Backend
- **.NET 9.0** (ASP.NET Core Web API)
- **Entity Framework Core** (9.0.4, 9.0.6)
- **SQL Server**
- **AutoMapper** (12.0.1)
- **Serilog** (9.0.0, Console/File Sinks)
- **JWT Authentication**
- **Swagger (Swashbuckle.AspNetCore 8.1.1)**

### Frontend
- **HTML5, CSS3, JavaScript** (Vanilla, no frameworks)

### Other
- **Visual Studio / VS Code**
- **Postman** (for API testing)

---

## Getting Started

### Prerequisites
- [.NET 9.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- (Optional) [Node.js](https://nodejs.org/) if you want to use additional tooling for frontend

### Setup Instructions

1. **Clone the repository:**
   ```bash
   git clone https://github.com/OzerovDaniil/Coursework.git
   cd Coursework
   ```

2. **Configure the database:**
   - Edit `SushiManagementSystem.API/appsettings.json` and set your SQL Server connection string.

3. **Apply database migrations:**
   ```bash
   dotnet ef database update --project SushiManagementSystem.Infrastructure
   ```

4. **Run the backend API:**
   ```bash
   dotnet run --project SushiManagementSystem.API
   ```
   The API will be available at `https://localhost:5067` (or as configured).

5. **Open the frontend:**
   - Open `front/index.html` in your browser.

6. **Test the API:**
   - Visit `/swagger` (e.g., `https://localhost:5067/swagger`) for interactive API documentation.

---

## API Documentation

- **Swagger UI** is available at `/swagger` after running the backend.
- All endpoints are grouped by entity (Orders, Menu, Customers, Employees, Inventory, etc.).
- JWT authentication is required for protected endpoints. Use the "Authorize" button in Swagger to add your token.

---

## Usage Examples

### Authentication
```http
POST /api/auth/login
Content-Type: application/json

{
  "username": "admin",
  "password": "yourpassword"
}
```

### Get All Orders
```http
GET /api/orders
Authorization: Bearer <your-jwt-token>
```

### Add New Menu Item
```http
POST /api/menu
Authorization: Bearer <your-jwt-token>
Content-Type: application/json

{
  "name": "Philadelphia Roll",
  "price": 250,
  "description": "Salmon, cream cheese, cucumber, rice, nori",
  ...
}
```

---

## Authentication & Authorization

- **Login** via `/api/auth/login` to receive a JWT token.
- Add the token to the `Authorization: Bearer <token>` header for all protected requests.
- Roles: `Admin` (full access), `Employee` (limited access).

---

## Contributing

Contributions, bug reports, and feature requests are welcome!

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/YourFeature`)
3. Commit your changes (`git commit -am 'Add some feature'`)
4. Push to the branch (`git push origin feature/YourFeature`)
5. Open a Pull Request

---

## FAQ

**Q: Can I use another database?**
A: The project is optimized for SQL Server, but you can adapt it for other DBMS supported by Entity Framework Core.

**Q: Is there a demo user?**
A: You can seed demo users via the database seeder or create them via the API.

**Q: How do I reset the admin password?**
A: Update the user record directly in the database or via a custom endpoint.

---

## License

This project is for educational purposes only. 