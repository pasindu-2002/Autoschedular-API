# AutoSchedular API Documentation

A comprehensive API for managing academic schedules, including courses, batches, lecturers, students, modules, and timetable generation.

## Base URL
```
http://localhost:5000/api
```

## Response Format
All API responses follow a consistent format:
```json
{
  "message": "Success message",
  "data": {}, // Response data (varies by endpoint)
  "statusCode": 200, // HTTP status code
  "details": {} // Additional details (optional)
}
```

---

## 📚 Course Management

### Get Course
**GET** `/api/course?courseCode={courseCode}`

Retrieve a specific course by course code.

**Query Parameters:**
- `courseCode` (string, required): The course code to search for

**Response:**
```json
{
  "message": "Course fetched successfully",
  "data": {
    "courseCode": "CS101",
    "courseName": "Computer Science Fundamentals",
    "school": "School of Computing"
  }
}
```

### Get All Courses
**GET** `/api/course/all`

Retrieve all courses.

**Response:**
```json
{
  "message": "All courses fetched successfully",
  "data": [
    {
      "courseCode": "CS101",
      "courseName": "Computer Science Fundamentals",
      "school": "School of Computing"
    }
  ]
}
```

### Create Course
**POST** `/api/course`

Create a new course.

**Request Body:**
```json
{
  "courseCode": "CS101",
  "courseName": "Computer Science Fundamentals",
  "school": "School of Computing"
}
```

### Update Course
**PUT** `/api/course?courseCode={courseCode}`

Update an existing course.

**Query Parameters:**
- `courseCode` (string, required): The course code to update

**Request Body:**
```json
{
  "courseName": "Updated Course Name",
  "school": "Updated School Name"
}
```

### Delete Course
**DELETE** `/api/course?courseCode={courseCode}`

Delete a course.

**Query Parameters:**
- `courseCode` (string, required): The course code to delete

---

## 🎓 Batch Management

### Get Batch
**GET** `/api/batch?batchCode={batchCode}`

Retrieve a specific batch by batch code.

**Query Parameters:**
- `batchCode` (string, required): The batch code to search for

**Response:**
```json
{
  "message": "Batch fetched successfully",
  "data": {
    "batchCode": "CS101-2024",
    "courseCode": "CS101",
    "courseDirector": "EMP001",
    "lecturerDetails": {
      "empNo": "EMP001",
      "fullName": "John Doe",
      "email": "john.doe@university.edu"
    }
  }
}
```

### Get All Batches
**GET** `/api/batch/all`

Retrieve all batches.

### Create Batch
**POST** `/api/batch`

Create a new batch.

**Request Body:**
```json
{
  "batchCode": "CS101-2024",
  "courseCode": "CS101",
  "courseDirector": "EMP001"
}
```

### Update Batch
**PUT** `/api/batch?batchCode={batchCode}`

Update an existing batch.

**Query Parameters:**
- `batchCode` (string, required): The batch code to update

**Request Body:**
```json
{
  "courseCode": "CS102",
  "courseDirector": "EMP002"
}
```

### Delete Batch
**DELETE** `/api/batch?batchCode={batchCode}`

Delete a batch.

---

## 👨‍🏫 Lecturer Management

### Get Lecturer (Login)
**GET** `/api/lecturer?empNo={empNo}&password={password}`

Authenticate and retrieve lecturer information.

**Query Parameters:**
- `empNo` (string, required): Employee number
- `password` (string, required): Password

**Response:**
```json
{
  "message": "Lecturer fetched successfully",
  "data": {
    "empNo": "EMP001",
    "fullName": "John Doe",
    "email": "john.doe@university.edu"
  }
}
```

### Get All Lecturers
**GET** `/api/lecturer/all`

Retrieve all lecturers.

### Get Lecturer with Batches
**GET** `/api/lecturer/{empNo}/batches`

Retrieve lecturer information along with their assigned batches.

**Path Parameters:**
- `empNo` (string, required): Employee number

**Response:**
```json
{
  "message": "Lecturer with batches fetched successfully",
  "data": {
    "empNo": "EMP001",
    "fullName": "John Doe",
    "email": "john.doe@university.edu",
    "batches": [
      {
        "batchCode": "CS101-2024",
        "courseCode": "CS101",
        "courseName": "Computer Science Fundamentals"
      }
    ]
  }
}
```

### Create Lecturer
**POST** `/api/lecturer`

Create a new lecturer.

**Request Body:**
```json
{
  "empNo": "EMP001",
  "fullName": "John Doe",
  "email": "john.doe@university.edu",
  "password": "securepassword"
}
```

### Update Lecturer
**PUT** `/api/lecturer?empNo={empNo}`

Update lecturer information.

**Request Body:**
```json
{
  "fullName": "John Smith",
  "email": "john.smith@university.edu",
  "password": "newpassword"
}
```

### Delete Lecturer
**DELETE** `/api/lecturer?empNo={empNo}`

Delete a lecturer.

---

## 🎓 Student Management

### Get Student (Login)
**GET** `/api/student?stuId={stuId}&password={password}`

Authenticate and retrieve student information.

**Query Parameters:**
- `stuId` (string, required): Student ID
- `password` (string, required): Password

### Get All Students
**GET** `/api/student/all`

Retrieve all students.

### Get Students by Batch
**GET** `/api/student/batch/{batchCode}`

Retrieve all students in a specific batch.

**Path Parameters:**
- `batchCode` (string, required): Batch code

### Create Student
**POST** `/api/student`

Create a new student.

**Request Body:**
```json
{
  "stuId": "STU001",
  "fullName": "Jane Doe",
  "email": "jane.doe@university.edu",
  "password": "securepassword",
  "batchCode": "CS101-2024"
}
```

### Update Student
**PUT** `/api/student?stuId={stuId}`

Update student information.

### Delete Student
**DELETE** `/api/student?stuId={stuId}`

Delete a student.

---

## 👥 PO Staff Management

### Get Employee (Login)
**GET** `/api/postaff?empNo={empNo}&password={password}`

Authenticate and retrieve PO staff information.

### Get All Employees
**GET** `/api/postaff/all`

Retrieve all PO staff members.

### Create Employee
**POST** `/api/postaff`

Create a new PO staff member.

**Request Body:**
```json
{
  "empNo": "PO001",
  "fullName": "Admin User",
  "email": "admin@university.edu",
  "password": "securepassword"
}
```

### Update Employee
**PUT** `/api/postaff?empNo={empNo}`

Update PO staff information.

### Delete Employee
**DELETE** `/api/postaff?empNo={empNo}`

Delete a PO staff member.

---

## 📖 Module Management

### Get Module
**GET** `/api/module/{moduleCode}`

Retrieve a specific module by module code.

**Path Parameters:**
- `moduleCode` (string, required): The module code

**Response:**
```json
{
  "statusCode": 200,
  "message": "Module fetched successfully",
  "data": {
    "moduleCode": "CS101-MOD1",
    "moduleName": "Introduction to Programming",
    "moduleHours": 40,
    "courseCode": "CS101",
    "courseName": "Computer Science Fundamentals"
  }
}
```

### Get All Modules
**GET** `/api/module`

Retrieve all modules.

### Create Module
**POST** `/api/module`

Create a new module.

**Request Body:**
```json
{
  "moduleCode": "CS101-MOD1",
  "moduleName": "Introduction to Programming",
  "moduleHours": 40,
  "courseCode": "CS101"
}
```

### Update Module
**PUT** `/api/module/{moduleCode}`

Update an existing module.

**Request Body:**
```json
{
  "moduleName": "Advanced Programming",
  "moduleHours": 60,
  "courseCode": "CS102"
}
```

### Delete Module
**DELETE** `/api/module/{moduleCode}`

Delete a module.

---

## 📅 Assign Module Management

### Get All Assign Modules
**GET** `/api/assignmodule`

Retrieve all module assignments.

### Get Assign Module by ID
**GET** `/api/assignmodule/{id}`

Retrieve a specific assignment by ID.

**Path Parameters:**
- `id` (GUID, required): Assignment ID

### Get Assign Module with Details
**GET** `/api/assignmodule/{id}/details`

Retrieve assignment with detailed information about batch, lecturer, and module.

### Get Assignments by Batch
**GET** `/api/assignmodule/batch/{batchId}`

Retrieve all assignments for a specific batch.

### Get Assignments by Lecturer
**GET** `/api/assignmodule/lecturer/{lecturerId}`

Retrieve all assignments for a specific lecturer.

### Get Assignments by Module
**GET** `/api/assignmodule/module/{moduleId}`

Retrieve all assignments for a specific module.

### Get Assignments by Date
**GET** `/api/assignmodule/date/{date}`

Retrieve assignments for a specific date.

### Get Assignments by Date Range
**GET** `/api/assignmodule/daterange?startDate={startDate}&endDate={endDate}`

Retrieve assignments within a date range.

**Query Parameters:**
- `startDate` (DateTime, required): Start date
- `endDate` (DateTime, required): End date

### Get Assignments by Status
**GET** `/api/assignmodule/status/{status}`

Retrieve assignments by status.

### Get Assignments by Session Type
**GET** `/api/assignmodule/sessiontype/{sessionType}`

Retrieve assignments by session type.

### Create Assignment
**POST** `/api/assignmodule`

Create a new module assignment.

**Request Body:**
```json
{
  "batchId": "CS101-2024",
  "lecturerId": "EMP001",
  "moduleId": "CS101-MOD1",
  "date": "2024-01-15T09:00:00Z",
  "status": "Scheduled",
  "sessionType": "Lecture"
}
```

### Update Assignment
**PUT** `/api/assignmodule/{id}`

Update an existing assignment.

**Request Body:**
```json
{
  "date": "2024-01-16T09:00:00Z",
  "status": "Completed",
  "sessionType": "Lab"
}
```

### Delete Assignment
**DELETE** `/api/assignmodule/{id}`

Delete an assignment.

---

## 📋 Lecturer Timetable Management

### Get All Lecturer Timetables
**GET** `/api/lecturertimetable`

Retrieve all lecturer timetables.

### Get Lecturer Timetable by ID
**GET** `/api/lecturertimetable/{id}`

Retrieve a specific timetable entry by ID.

### Get Timetables by Lecturer
**GET** `/api/lecturertimetable/lecturer/{lecturerId}`

Retrieve all timetable entries for a specific lecturer.

### Get Timetables by Date
**GET** `/api/lecturertimetable/date/{date}`

Retrieve timetables for a specific date.

### Get Timetables by Date Range
**GET** `/api/lecturertimetable/daterange?startDate={startDate}&endDate={endDate}`

Retrieve timetables within a date range.

### Create Lecturer Timetable
**POST** `/api/lecturertimetable`

Create a new lecturer timetable entry.

**Request Body:**
```json
{
  "lecturerId": "EMP001",
  "description": "Faculty Meeting",
  "date": "2024-01-15T14:00:00Z"
}
```

### Update Lecturer Timetable
**PUT** `/api/lecturertimetable/{id}`

Update an existing timetable entry.

### Delete Lecturer Timetable
**DELETE** `/api/lecturertimetable/{id}`

Delete a timetable entry.

---

## 🗓️ Timetable Generation

### Generate Timetable
**POST** `/api/timetable/generate`

Generate a timetable based on the provided parameters.

**Request Body:**
```json
{
  "lecturerId": "EMP001",
  "moduleCode": "CS101-MOD1",
  "batchCode": "CS101-2024",
  "sessionType": "Lecture",
  "startDate": "2024-01-15T00:00:00Z",
  "endDate": "2024-03-15T00:00:00Z"
}
```

**Response:**
```json
{
  "success": true,
  "message": "Timetable generated successfully",
  "generatedDates": [
    "2024-01-15",
    "2024-01-22",
    "2024-01-29"
  ]
}
```

---

## 🚨 Error Responses

### 400 Bad Request
```json
{
  "message": "Missing required fields",
  "data": null
}
```

### 404 Not Found
```json
{
  "message": "Resource not found",
  "data": null
}
```

### 409 Conflict
```json
{
  "message": "Resource already exists",
  "data": null
}
```

### 500 Internal Server Error
```json
{
  "message": "An internal server error occurred",
  "data": null
}
```

---

## 📝 Notes

1. **Authentication**: The API currently uses simple username/password authentication for lecturers, students, and PO staff.

2. **Date Format**: All dates should be provided in ISO 8601 format (e.g., `2024-01-15T09:00:00Z`).

3. **Validation**: All endpoints include proper validation. Required fields must be provided, and data types must match the specifications.

4. **Session Types**: Common session types include "Lecture", "Lab", "Tutorial", "Seminar".

5. **Status Values**: Common status values for assignments include "Scheduled", "In Progress", "Completed", "Cancelled".

6. **ID Formats**:
   - Course Code: String (max 50 characters)
   - Batch Code: String (max 50 characters)
   - Employee Number: String (max 50 characters)
   - Student ID: String (max 50 characters)
   - Module Code: String (max 50 characters)
   - Assignment ID: GUID

## 🔧 Development Setup

1. Ensure .NET 8.0 SDK is installed
2. Update connection string in `appsettings.json`
3. Run database migrations: `dotnet ef database update`
4. Start the application: `dotnet run`
5. API will be available at `http://localhost:5000`

## 📚 Additional Resources

- [Entity Framework Core Documentation](https://docs.microsoft.com/en-us/ef/core/)
- [ASP.NET Core Web API Documentation](https://docs.microsoft.com/en-us/aspnet/core/web-api/)
