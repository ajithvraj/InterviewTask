# Task Management API

## Overview

This project is a **Task Management Web API** built using **ASP.NET Core (.NET 8)** following **Clean Architecture principles**.

The system allows authenticated users to manage tasks while enforcing **role-based authorization**.

Features implemented:

* Create tasks
* Update tasks
* View tasks
* Mark tasks as completed
* Role-based authorization (Admin / User)
* Standardized API responses
* Unit testing using xUnit

---

# Project Setup Instructions

### 1. Clone the repository

```
git clone https://github.com/yourusername/TaskManagementAPI.git
```

### 2. Navigate to the project folder

```
cd TaskManagementAPI
```

### 3. Restore dependencies

```
dotnet restore
```

### 4. Run the project

```
dotnet run
```

### 5. Open Swagger UI

```
https://localhost:{port}/swagger
```

Swagger provides an interface to test all APIs.

---

# API Usage Guide

## Authentication

Authentication is **simulated using request headers**.

All task APIs require the following headers:

```
x-user-id
x-user-role
```

---

# API Endpoints

## 1. Login

Endpoint

```
POST /api/auth/login
```

Request body

```
{
  "username": "admin",
  "password": "admin123"
}
```

Response

```
{
  "success": true,
  "message": "Login successful",
  "data": {
    "username": "admin",
    "role": "Admin"
  }
}
```

---

## 2. Create Task

Endpoint

```
POST /api/taskmanagement
```

Headers

```
x-user-id
x-user-role
```

Request body

```
{
  "title": "Complete project",
  "description": "Finish API implementation",
  "dueDate": "2026-03-20"
}
```

Behavior

* CreatedAt is automatically set
* IsCompleted defaults to false

---

## 3. View Tasks

Endpoint

```
GET /api/taskmanagement
```

Authorization rules

Admin → can view all tasks
User → can view only their own tasks

Headers required

```
x-user-id
x-user-role
```

---

## 4. Update Task

Endpoint

```
PUT /api/taskmanagement/{id}
```

Rules

* Users can update only their own tasks
* Editable fields:

  * Title
  * Description
  * DueDate

---

## 5. Mark Task as Completed

Endpoint

```
PUT /api/taskmanagement/{id}/complete
```

Authorization rule

Only **Admin users** can mark tasks as completed.

---

# Credentials for Test Users

Two users are hardcoded in the system.

### Admin User

Username

```
admin
```

Password

```
admin123
```

Headers

```
x-user-id: 1
x-user-role: Admin
```

---

### Regular User

Username

```
TestUser
```

Password

```
user123
```

Headers

```
x-user-id: 2
x-user-role: User
```

---

# Running Unit Tests

Unit tests are implemented using **xUnit**.

Run tests using:

```
dotnet test
```

---

# Architecture

The project follows **Clean Architecture** with the following layers:

```
TaskManagement.API
TaskManagement.Application
TaskManagement.Domain
TaskManagement.Infrastructure
TaskManagement.Tests
```

API → Controllers and endpoints
Application → Business logic and services
Domain → Entities and interfaces
Infrastructure → Data access using EF Core
Tests → Unit tests

---

# Author
Ajith V Raj   .Net candidate
