# Student API Usage Guide

This document describes how to use the Student API endpoints that were created based on the PHP implementation.

## Relationship Information
- **One-to-Many**: Each Batch can have many Students
- **Foreign Key**: Students have a `BatchCode` that references the Batch they belong to
- **Constraint**: Students must be assigned to an existing Batch

## Base URL
```
/api/Student
```

## Endpoints

### 1. Get Student (Authentication)
**GET** `/api/Student?stuId={student_id}&password={password}`

Authenticates a student and returns their information.

**Parameters:**
- `stuId` (query): Student ID
- `password` (query): Student password

**Response (200 OK):**
```json
{
  "message": "Student fetched successfully",
  "data": {
    "stuId": "STU001",
    "fullName": "John Doe",
    "email": "john.doe@example.com",
    "batchCode": "BATCH001"
  }
}
```

**Error Responses:**
- `400`: Missing stuId or password
- `404`: Student not found or invalid password

### 2. Create Student
**POST** `/api/Student`

Creates a new student account.

**Request Body:**
```json
{
  "stuId": "STU001",
  "fullName": "John Doe",
  "email": "john.doe@example.com",
  "password": "password123",
  "batchCode": "BATCH001"
}
```

**Response (201 Created):**
```json
{
  "message": "Student added successfully"
}
```

**Error Responses:**
- `400`: Missing required fields or validation errors
- `409`: Student with this ID already exists

### 3. Update Student
**PUT** `/api/Student?stuId={student_id}`

Updates an existing student's information.

**Parameters:**
- `stuId` (query): Student ID

**Request Body (partial updates allowed):**
```json
{
  "fullName": "Jane Doe",
  "email": "jane.doe@example.com",
  "password": "newpassword123",
  "batchCode": "BATCH002"
}
```

**Response (200 OK):**
```json
{
  "message": "Student updated successfully"
}
```

**Error Responses:**
- `400`: Missing stuId or no fields to update
- `404`: Student not found

### 4. Delete Student
**DELETE** `/api/Student?stuId={student_id}`

Deletes a student account.

**Parameters:**
- `stuId` (query): Student ID

**Response (200 OK):**
```json
{
  "message": "Student deleted successfully"
}
```

**Error Responses:**
- `400`: Missing stuId
- `404`: Student not found

### 5. Get Students by Batch
**GET** `/api/Student/batch/{batchCode}`

Retrieves all students in a specific batch.

**Parameters:**
- `batchCode` (path): Batch code to filter students

**Response (200 OK):**
```json
{
  "message": "Students in batch BATCH001 fetched successfully",
  "data": [
    {
      "stuId": "STU001",
      "fullName": "John Doe",
      "email": "john.doe@example.com",
      "batchCode": "BATCH001"
    },
    {
      "stuId": "STU002",
      "fullName": "Jane Smith",
      "email": "jane.smith@example.com",
      "batchCode": "BATCH001"
    }
  ]
}
```

**Error Responses:**
- `400`: Missing batchCode

## Security Features

- **Password Hashing**: All passwords are hashed using BCrypt before storage
- **Password Verification**: Login endpoint verifies passwords against hashed values
- **Duplicate Prevention**: Cannot create students with existing IDs
- **Validation**: Input validation using data annotations
- **Referential Integrity**: Students must belong to an existing Batch

## Database Mapping

The Student entity maps to the `student_tbl` table with the following columns:
- `stu_id` → `StuId`
- `full_name` → `FullName`
- `email` → `Email`
- `password` → `Password` (hashed)
- `batch_code` → `BatchCode` (Foreign Key)

## Relationships
- **Student** belongs to **Batch** (Many-to-One)
- **Batch** has many **Students** (One-to-Many)
- Foreign Key: `Student.BatchCode` → `Batch.BatchCode`

## Example Usage

### Creating a Student
```bash
curl -X POST "https://localhost:7000/api/Student" \
  -H "Content-Type: application/json" \
  -d '{
    "stuId": "STU001",
    "fullName": "John Doe",
    "email": "john.doe@example.com",
    "password": "password123",
    "batchCode": "BATCH001"
  }'
```

### Authenticating a Student
```bash
curl -X GET "https://localhost:7000/api/Student?stuId=STU001&password=password123"
```

### Updating a Student
```bash
curl -X PUT "https://localhost:7000/api/Student?stuId=STU001" \
  -H "Content-Type: application/json" \
  -d '{
    "fullName": "Jane Doe",
    "email": "jane.doe@example.com",
    "batchCode": "BATCH002"
  }'
```

### Deleting a Student
```bash
curl -X DELETE "https://localhost:7000/api/Student?stuId=STU001"
```

### Getting Students by Batch
```bash
curl -X GET "https://localhost:7000/api/Student/batch/BATCH001"
```
