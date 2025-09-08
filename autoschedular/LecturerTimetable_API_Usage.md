# Lecturer Timetable API Usage

This document describes how to use the Lecturer Timetable API endpoints.

## Base URL
```
https://localhost:7XXX/api/LecturerTimetable
```

## Endpoints

### 1. Get All Lecturer Timetables
**GET** `/api/LecturerTimetable`

Returns all lecturer timetables.

**Response:**
```json
{
  "message": "Lecturer timetables fetched successfully",
  "data": [
    {
      "id": 1,
      "lecturerId": "EMP001",
      "description": "Mathematics Lecture - Room A101",
      "date": "2024-01-15T00:00:00",
      "lecturerName": "Dr. John Smith"
    }
  ]
}
```

### 2. Get Lecturer Timetable by ID
**GET** `/api/LecturerTimetable/{id}`

Returns a specific lecturer timetable by ID.

**Parameters:**
- `id` (path parameter): Timetable ID

**Response:**
```json
{
  "message": "Lecturer timetable fetched successfully",
  "data": {
    "id": 1,
    "lecturerId": "EMP001",
    "description": "Mathematics Lecture - Room A101",
    "date": "2024-01-15T00:00:00",
    "lecturerName": "Dr. John Smith"
  }
}
```

### 3. Get Lecturer Timetables by Lecturer ID
**GET** `/api/LecturerTimetable/lecturer/{lecturerId}`

Returns all timetables for a specific lecturer.

**Parameters:**
- `lecturerId` (path parameter): Lecturer's employee number

**Response:**
```json
{
  "message": "Lecturer timetables fetched successfully",
  "data": [
    {
      "id": 1,
      "lecturerId": "EMP001",
      "description": "Mathematics Lecture - Room A101",
      "date": "2024-01-15T00:00:00",
      "lecturerName": "Dr. John Smith"
    }
  ]
}
```

### 4. Get Lecturer Timetables by Date
**GET** `/api/LecturerTimetable/date/{date}`

Returns all lecturer timetables for a specific date.

**Parameters:**
- `date` (path parameter): Date in format YYYY-MM-DD

**Example:** `/api/LecturerTimetable/date/2024-01-15`

### 5. Get Lecturer Timetables by Date Range
**GET** `/api/LecturerTimetable/daterange?startDate={startDate}&endDate={endDate}`

Returns all lecturer timetables within a date range.

**Parameters:**
- `startDate` (query parameter): Start date in format YYYY-MM-DD
- `endDate` (query parameter): End date in format YYYY-MM-DD

**Example:** `/api/LecturerTimetable/daterange?startDate=2024-01-01&endDate=2024-01-31`

### 6. Create Lecturer Timetable
**POST** `/api/LecturerTimetable`

Creates a new lecturer timetable entry.

**Request Body:**
```json
{
  "lecturerId": "EMP001",
  "description": "Mathematics Lecture - Room A101",
  "date": "2024-01-15T09:00:00"
}
```

**Response:**
```json
{
  "message": "Lecturer timetable created successfully"
}
```

### 7. Update Lecturer Timetable
**PUT** `/api/LecturerTimetable/{id}`

Updates an existing lecturer timetable.

**Parameters:**
- `id` (path parameter): Timetable ID

**Request Body:**
```json
{
  "description": "Updated Mathematics Lecture - Room B201",
  "date": "2024-01-15T10:00:00"
}
```

**Response:**
```json
{
  "message": "Lecturer timetable updated successfully"
}
```

### 8. Delete Lecturer Timetable
**DELETE** `/api/LecturerTimetable/{id}`

Deletes a lecturer timetable entry.

**Parameters:**
- `id` (path parameter): Timetable ID

**Response:**
```json
{
  "message": "Lecturer timetable deleted successfully"
}
```

## Error Responses

### 400 Bad Request
```json
{
  "message": "Invalid timetable ID"
}
```

### 404 Not Found
```json
{
  "message": "Lecturer timetable not found"
}
```

## Notes

1. The `lecturerId` must correspond to an existing lecturer's `EmpNo` in the system.
2. Dates are handled in ISO 8601 format.
3. The `description` field is optional and can be null.
4. All endpoints return data wrapped in a standardized `ApiResponse` format.
5. When creating a timetable, ensure the lecturer exists in the system, otherwise the creation will fail.
