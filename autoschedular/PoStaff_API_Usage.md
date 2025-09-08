# PoStaff API Usage Guide

This document outlines how to use the PoStaff API endpoints for managing Post Office Staff (PoStaff) data.

## Base URL
```
https://localhost:7xxx/api/postaff
```

## Authentication
All endpoints require valid employee credentials (emp_no and password) for authentication.

## Endpoints

### 1. Get Employee (Login/Authentication)
**GET** `/api/postaff`

Retrieves employee information after validating credentials.

**Query Parameters:**
- `empNo` (string, required): Employee number
- `password` (string, required): Employee password

**Example Request:**
```http
GET /api/postaff?empNo=EMP001&password=mypassword123
```

**Success Response (200 OK):**
```json
{
    "message": "Employee fetched successfully",
    "data": {
        "empNo": "EMP001",
        "fullName": "John Doe",
        "email": "john.doe@company.com"
    }
}
```

**Error Responses:**
- **400 Bad Request:** Missing emp_no or password
- **404 Not Found:** Employee not found or invalid password

### 2. Create Employee
**POST** `/api/postaff`

Creates a new employee record.

**Request Body:**
```json
{
    "empNo": "EMP002",
    "fullName": "Jane Smith",
    "email": "jane.smith@company.com",
    "password": "securepassword123"
}
```

**Success Response (201 Created):**
```json
{
    "message": "Employee added successfully"
}
```

**Error Responses:**
- **400 Bad Request:** Missing required fields or validation errors
- **409 Conflict:** Employee number already exists

### 3. Update Employee
**PUT** `/api/postaff`

Updates an existing employee's information.

**Query Parameters:**
- `empNo` (string, required): Employee number to update

**Request Body (all fields optional):**
```json
{
    "fullName": "Jane Doe Smith",
    "email": "jane.doesmith@company.com",
    "password": "newpassword123"
}
```

**Success Response (200 OK):**
```json
{
    "message": "Employee updated successfully"
}
```

**Error Responses:**
- **400 Bad Request:** Missing emp_no or no fields to update
- **404 Not Found:** Employee not found or no changes made

### 4. Delete Employee
**DELETE** `/api/postaff`

Deletes an employee record.

**Query Parameters:**
- `empNo` (string, required): Employee number to delete

**Example Request:**
```http
DELETE /api/postaff?empNo=EMP002
```

**Success Response (200 OK):**
```json
{
    "message": "Employee deleted successfully"
}
```

**Error Responses:**
- **400 Bad Request:** Missing emp_no
- **404 Not Found:** Employee not found

## Data Validation Rules

### Employee Number (empNo)
- Required for all operations
- Maximum 50 characters
- Must be unique

### Full Name (fullName)
- Required when creating
- Maximum 255 characters

### Email
- Required when creating
- Maximum 255 characters
- Must be valid email format

### Password
- Required when creating
- Minimum 6 characters
- Automatically hashed using BCrypt before storage

## Error Response Format
All error responses follow this structure:
```json
{
    "message": "Error description"
}
```

## Security Notes
- Passwords are hashed using BCrypt before storage
- Never store or transmit passwords in plain text
- The GET endpoint requires both emp_no and password for authentication
- Password verification is done server-side using secure hash comparison

## Example Usage with cURL

### Login/Get Employee
```bash
curl -X GET "https://localhost:7xxx/api/postaff?empNo=EMP001&password=mypassword123"
```

### Create Employee
```bash
curl -X POST "https://localhost:7xxx/api/postaff" \
  -H "Content-Type: application/json" \
  -d '{
    "empNo": "EMP002",
    "fullName": "Jane Smith",
    "email": "jane.smith@company.com",
    "password": "securepassword123"
  }'
```

### Update Employee
```bash
curl -X PUT "https://localhost:7xxx/api/postaff?empNo=EMP002" \
  -H "Content-Type: application/json" \
  -d '{
    "fullName": "Jane Doe Smith",
    "email": "jane.doesmith@company.com"
  }'
```

### Delete Employee
```bash
curl -X DELETE "https://localhost:7xxx/api/postaff?empNo=EMP002"
```
