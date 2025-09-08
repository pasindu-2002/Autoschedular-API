# Assign Module API Usage

This document describes how to use the Assign Module API endpoints for managing module assignments to batches and lecturers.

## Base URL
```
https://localhost:7XXX/api/AssignModule
```

## Endpoints

### 1. Get All Module Assignments
**GET** `/api/AssignModule`

Returns all module assignments.

**Response:**
```json
{
  "message": "Module assignments fetched successfully",
  "data": [
    {
      "id": "550e8400-e29b-41d4-a716-446655440000",
      "batchId": "BATCH001",
      "lecturerId": "EMP001",
      "moduleId": "MOD001",
      "date": "2024-01-15T00:00:00",
      "status": "Active",
      "sessionType": "Lecture",
      "batchName": "Computer Science Degree",
      "lecturerName": "Dr. John Smith",
      "moduleName": "Programming Fundamentals"
    }
  ]
}
```

### 2. Get Module Assignment by ID
**GET** `/api/AssignModule/{id}`

Returns a specific module assignment by ID.

**Parameters:**
- `id` (path parameter): Assignment ID

**Response:**
```json
{
  "message": "Module assignment fetched successfully",
  "data": {
    "id": 1,
    "batchId": "BATCH001",
    "lecturerId": "EMP001",
    "moduleId": "MOD001",
    "date": "2024-01-15T00:00:00",
    "status": "Active",
    "sessionType": "Lecture",
    "batchName": "Computer Science Degree",
    "lecturerName": "Dr. John Smith",
    "moduleName": "Programming Fundamentals"
  }
}
```

### 3. Get Module Assignment with Full Details
**GET** `/api/AssignModule/{id}/details`

Returns a module assignment with complete details of batch, lecturer, and module.

**Parameters:**
- `id` (path parameter): Assignment ID

**Response:**
```json
{
  "message": "Module assignment with details fetched successfully",
  "data": {
    "id": 1,
    "batchId": "BATCH001",
    "lecturerId": "EMP001",
    "moduleId": "MOD001",
    "date": "2024-01-15T00:00:00",
    "status": "Active",
    "sessionType": "Lecture",
    "batch": {
      "batchCode": "BATCH001",
      "courseCode": "CS001",
      "courseName": "Computer Science Degree"
    },
    "lecturer": {
      "empNo": "EMP001",
      "fullName": "Dr. John Smith",
      "email": "john.smith@university.edu"
    },
    "module": {
      "moduleCode": "MOD001",
      "moduleName": "Programming Fundamentals",
      "moduleHours": 40,
      "courseCode": "CS001"
    }
  }
}
```

### 4. Get Module Assignments by Batch ID
**GET** `/api/AssignModule/batch/{batchId}`

Returns all module assignments for a specific batch.

**Parameters:**
- `batchId` (path parameter): Batch code

**Example:** `/api/AssignModule/batch/BATCH001`

### 5. Get Module Assignments by Lecturer ID
**GET** `/api/AssignModule/lecturer/{lecturerId}`

Returns all module assignments for a specific lecturer.

**Parameters:**
- `lecturerId` (path parameter): Lecturer's employee number

**Example:** `/api/AssignModule/lecturer/EMP001`

### 6. Get Module Assignments by Module ID
**GET** `/api/AssignModule/module/{moduleId}`

Returns all assignments for a specific module.

**Parameters:**
- `moduleId` (path parameter): Module code

**Example:** `/api/AssignModule/module/MOD001`

### 7. Get Module Assignments by Date
**GET** `/api/AssignModule/date/{date}`

Returns all module assignments for a specific date.

**Parameters:**
- `date` (path parameter): Date in format YYYY-MM-DD

**Example:** `/api/AssignModule/date/2024-01-15`

### 8. Get Module Assignments by Date Range
**GET** `/api/AssignModule/daterange?startDate={startDate}&endDate={endDate}`

Returns all module assignments within a date range.

**Parameters:**
- `startDate` (query parameter): Start date in format YYYY-MM-DD
- `endDate` (query parameter): End date in format YYYY-MM-DD

**Example:** `/api/AssignModule/daterange?startDate=2024-01-01&endDate=2024-01-31`

### 9. Get Module Assignments by Status
**GET** `/api/AssignModule/status/{status}`

Returns all module assignments with a specific status.

**Parameters:**
- `status` (path parameter): Status value (e.g., "Active", "Inactive", "Completed")

**Example:** `/api/AssignModule/status/Active`

### 10. Get Module Assignments by Session Type
**GET** `/api/AssignModule/sessiontype/{sessionType}`

Returns all module assignments with a specific session type.

**Parameters:**
- `sessionType` (path parameter): Session type (e.g., "Lecture", "Lab", "Tutorial")

**Example:** `/api/AssignModule/sessiontype/Lecture`

### 11. Create Module Assignment
**POST** `/api/AssignModule`

Creates a new module assignment.

**Request Body:**
```json
{
  "batchId": "BATCH001",
  "lecturerId": "EMP001",
  "moduleId": "MOD001",
  "date": "2024-01-15T09:00:00",
  "status": "Active",
  "sessionType": "Lecture"
}
```

**Response:**
```json
{
  "message": "Module assignment created successfully"
}
```

### 12. Update Module Assignment
**PUT** `/api/AssignModule/{id}`

Updates an existing module assignment.

**Parameters:**
- `id` (path parameter): Assignment ID

**Request Body:**
```json
{
  "date": "2024-01-16T10:00:00",
  "status": "Completed",
  "sessionType": "Lab"
}
```

**Response:**
```json
{
  "message": "Module assignment updated successfully"
}
```

### 13. Delete Module Assignment
**DELETE** `/api/AssignModule/{id}`

Deletes a module assignment.

**Parameters:**
- `id` (path parameter): Assignment ID

**Response:**
```json
{
  "message": "Module assignment deleted successfully"
}
```

## Error Responses

### 400 Bad Request
```json
{
  "message": "Invalid assignment ID"
}
```

```json
{
  "message": "Failed to create module assignment. Please ensure the batch, lecturer, and module exist."
}
```

### 404 Not Found
```json
{
  "message": "Module assignment not found"
}
```

## Field Descriptions

- **batchId**: The batch code (references batch_tbl.batch_code)
- **lecturerId**: The lecturer's employee number (references lecturers_tbl.emp_no)
- **moduleId**: The module code (references modules_tbl.module_code)
- **date**: The date when the module is assigned/scheduled
- **status**: Current status of the assignment (e.g., "Active", "Inactive", "Completed")
- **sessionType**: Type of session (e.g., "Lecture", "Lab", "Tutorial", "Workshop")

## Notes

1. All referenced IDs (batchId, lecturerId, moduleId) must exist in their respective tables.
2. The system validates foreign key relationships before creating assignments.
3. Dates are handled in ISO 8601 format.
4. Status and SessionType fields are required and have maximum lengths of 20 and 50 characters respectively.
5. All endpoints return data wrapped in a standardized `ApiResponse` format.
6. The database uses NO ACTION on delete to prevent cascading deletes and maintain data integrity.

## Common Status Values
- "Active" - Assignment is currently active
- "Inactive" - Assignment is temporarily inactive
- "Completed" - Assignment has been completed
- "Cancelled" - Assignment has been cancelled

## Common Session Types
- "Lecture" - Traditional lecture session
- "Lab" - Laboratory/practical session
- "Tutorial" - Tutorial/discussion session
- "Workshop" - Workshop session
- "Seminar" - Seminar session
- "Assessment" - Assessment/exam session
