# 💊 Pharmacy Inventory System

A full-stack web-based Pharmacy Inventory Management System built using ASP.NET Core MVC, Entity Framework Core, and SQL Server. The system helps manage products, suppliers, sales, orders, and payments with authentication and dashboard analytics.

---

## 🚀 Features

- User Authentication (Login/Register with JWT)
- Product Management (CRUD operations)
- Supplier Management
- Order Management
- Sales Tracking
- Payment Management
- Dashboard with Charts (Chart.js)
- Secure password hashing using BCrypt
- RESTful API architecture
- Fully containerized using Docker

---

## 🛠️ Technologies Used

- ASP.NET Core MVC
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- JWT Authentication
- BCrypt Password Hashing
- Bootstrap 5
- Chart.js
- JavaScript (Fetch API)
- Docker
- Docker Compose

---

## 🏗️ Project Architecture
-Controllers -> Buisness Logic
-Models -> Database Entries
-DTO -> Data Transfer Object
-Views -> Razor pages
-Data -> DbContext (EF Core)


---

## 📊 Dashboard Features

- Total Products
- Low Stock Alerts
- Out of Stock Items
- Sales Overview Charts
- Supplier Distribution

---

## 🔐 Authentication

- JWT-based authentication system
- Password hashing using BCrypt
- Secure API endpoints using `[Authorize]`

---

## ⚙️ Setup Instructions

1. Clone the repository
```bash
git clone https://github.com/your-username/pharmacy-inventory-system.git


## 🐳 Docker Setup

This project is fully containerized using Docker and Docker Compose.

### Run the project:

```bash
docker compose up --build

## ⚙️ Future Improvement
-Cloud Deployment

