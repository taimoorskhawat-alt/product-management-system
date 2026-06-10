# 🛒 Product Management System (Full Stack)

A full-stack web application built using **Angular 21** and **ASP.NET Core Web API** with JWT Authentication and Role-Based Authorization.

---

## 🚀 Features

- 🔐 User Registration & Login (JWT Authentication)
- 👤 Role-Based Authorization (Admin / User)
- 📦 Product CRUD (Create, Read, Update, Delete)
- 🛡️ Secure API with JWT Token
- 🔄 Angular HTTP Interceptor for token handling
- 🎯 Protected Routes (Auth Guard)
- 🎨 Responsive UI using Bootstrap 5
- 🧾 Reactive Forms with validation

---

## 🛠️ Tech Stack

### Frontend
- Angular 21
- TypeScript
- Bootstrap 5
- RxJS

### Backend
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- JWT Authentication
- Password Hashing

---

## 📁 Project Structure


ProductMS/
│
├── frontend/ (Angular App)
├── backend/ (ASP.NET Core API)


---

## 🔐 Authentication Flow

- User registers in system
- Password is securely hashed in backend
- User logs in with credentials
- JWT token is generated
- Token stored in localStorage
- Angular interceptor sends token with every request

---

## ⚙️ How to Run

### Backend

cd backend
dotnet restore
dotnet run


### Frontend

cd frontend
npm install
ng serve


---

## 📌 API Endpoints

- POST `/api/auth/register`
- POST `/api/auth/login`
- GET `/api/products`
- POST `/api/products`
- PUT `/api/products/{id}`
- DELETE `/api/products/{id}`

---

## 👨‍💻 Author

**Taimoor Sakhawat**

- GitHub: https://github.com/taumurskhawat-alt

---
