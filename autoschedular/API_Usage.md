# AutoSchedular API Usage

This API provides CRUD operations for managing courses and batches in the AutoSchedular system.

## Base URLs
```
Course API: https://localhost:7xxx/api/course
Batch API: https://localhost:7xxx/api/batch
```

## Course API Endpoints

### 1. Get Course
**GET** `/api/course?courseCode={courseCode}`

Retrieves a course by its course code.

**Parameters:**
- `courseCode` (query, required): The course code to search for

**Response:**
```json
{
  "message": "Course fetched successfully",
  "data": {
    "courseCode": "CS101",
    "courseName": "Introduction to Computer Science",
    "school": "School of Computing"
  }
}
```

### 2. Create Course
**POST** `/api/course`

Creates a new course.

**Request Body:**
```json
{
  "courseCode": "CS101",
  "courseName": "Introduction to Computer Science",
  "school": "School of Computing"
}
```

**Response:**
```json
{
  "message": "Course added successfully"
}
```

### 3. Update Course
**PUT** `/api/course?courseCode={courseCode}`

Updates an existing course.

**Parameters:**
- `courseCode` (query, required): The course code to update

**Request Body:**
```json
{
  "courseName": "Advanced Computer Science",
  "school": "School of Computing"
}
```
*Note: Both fields are optional. You can update just one field.*

**Response:**
```json
{
  "message": "Course updated successfully"
}
```

### 4. Delete Course
**DELETE** `/api/course?courseCode={courseCode}`

Deletes a course by its course code.

**Parameters:**
- `courseCode` (query, required): The course code to delete

**Response:**
```json
{
  "message": "Course deleted successfully"
}
```

## Error Responses

### 400 Bad Request
```json
{
  "message": "Please provide course code"
}
```

### 404 Not Found
```json
{
  "message": "Course not found"
}
```

### 409 Conflict
```json
{
  "message": "Course with this code already exists"
}
```

### 500 Internal Server Error
```json
{
  "message": "Internal server error",
  "data": {
    "details": "Error details here"
  }
}
```

## Database Setup

The API uses Entity Framework Core with SQL Server. Make sure to:

1. Update the connection string in `appsettings.json`
2. Run migrations to create the database:
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

## Database Schema

The `course_tbl` table has the following structure:
- `course_code` (varchar, Primary Key)
- `course_name` (varchar, Required)
- `school` (varchar, Required)

## Batch API Endpoints

### 1. Get Batch
**GET** `/api/batch?batchCode={batchCode}`

Retrieves a batch by its batch code.

**Parameters:**
- `batchCode` (query, required): The batch code to search for

**Response:**
```json
{
  "message": "Batch fetched successfully",
  "data": {
    "batchCode": "B2024-CS101",
    "courseCode": "CS101",
    "courseDirector": "Dr. Smith"
  }
}
```

### 2. Create Batch
**POST** `/api/batch`

Creates a new batch.

**Request Body:**
```json
{
  "batchCode": "B2024-CS101",
  "courseCode": "CS101",
  "courseDirector": "Dr. Smith"
}
```

**Response:**
```json
{
  "message": "Batch added successfully"
}
```

### 3. Update Batch
**PUT** `/api/batch?batchCode={batchCode}`

Updates an existing batch.

**Parameters:**
- `batchCode` (query, required): The batch code to update

**Request Body:**
```json
{
  "courseCode": "CS102",
  "courseDirector": "Dr. Johnson"
}
```
*Note: Both fields are optional. You can update just one field.*

**Response:**
```json
{
  "message": "Batch updated successfully"
}
```

### 4. Delete Batch
**DELETE** `/api/batch?batchCode={batchCode}`

Deletes a batch by its batch code.

**Parameters:**
- `batchCode` (query, required): The batch code to delete

**Response:**
```json
{
  "message": "Batch deleted successfully"
}
```

The `batch_tbl` table has the following structure:
- `batch_code` (varchar, Primary Key)
- `course_code` (varchar, Required)
- `course_director` (varchar, Required)
