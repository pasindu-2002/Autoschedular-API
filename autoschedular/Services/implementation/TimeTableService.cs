using autoschedular.Model;
using autoschedular.Model.DTOs;
using autoschedular.Services;
using Microsoft.EntityFrameworkCore;

namespace autoschedular.Services.implementation
{
    public class TimeTableService : ITimeTableService
    {
        private readonly AutoSchedularDbContext _context;
        private readonly Random _random;

        public TimeTableService(AutoSchedularDbContext context)
        {
            _context = context;
            _random = new Random();
        }

        public async Task<GenerateTimetableResponseDTO> GenerateTimetableAsync(GenerateTimetableRequestDTO request)
        {
            try
            {
                // Validate inputs
                if (string.IsNullOrEmpty(request.LecturerId) ||
                    string.IsNullOrEmpty(request.ModuleCode) ||
                    string.IsNullOrEmpty(request.BatchCode) ||
                    string.IsNullOrEmpty(request.SessionType) ||
                    request.StartDate == default ||
                    request.EndDate == default)
                {
                    return new GenerateTimetableResponseDTO
                    {
                        Success = false,
                        Message = "Missing required fields"
                    };
                }

                // Check if module exists and get module hours
                var module = await _context.Modules
                    .FirstOrDefaultAsync(m => m.ModuleCode == request.ModuleCode);

                if (module == null)
                {
                    return new GenerateTimetableResponseDTO
                    {
                        Success = false,
                        Message = "Module not found"
                    };
                }

                int moduleHours = module.ModuleHours;

                // Fetch existing dates for the lecturer
                var lecturerExistingDates = await _context.LecturerTimetables
                    .Where(lt => lt.LecturerId == request.LecturerId)
                    .Select(lt => lt.Date.Date)
                    .ToListAsync();

                // Fetch existing dates for the batch
                var batchExistingDates = await _context.AssignModules
                    .Where(am => am.BatchId == request.BatchCode)
                    .Select(am => am.Date.Date)
                    .ToListAsync();

                // Generate timetable based on module hours
                if (moduleHours == 60)
                {
                    var generatedDates = await GenerateRandomDatesAsync(
                        request.StartDate,
                        request.EndDate,
                        lecturerExistingDates,
                        batchExistingDates,
                        10); // Generate 10 dates for 60-hour modules

                    if (generatedDates.Count < 10)
                    {
                        return new GenerateTimetableResponseDTO
                        {
                            Success = false,
                            Message = "Not enough available dates"
                        };
                    }

                    // Insert dates into the database using transaction
                    using var transaction = await _context.Database.BeginTransactionAsync();
                    try
                    {
                        foreach (var date in generatedDates)
                        {
                            // Insert into AssignModule
                            var assignModule = new AssignModule
                            {
                                BatchId = request.BatchCode,
                                LecturerId = request.LecturerId,
                                ModuleId = request.ModuleCode,
                                Date = date,
                                Status = "0",
                                SessionType = request.SessionType
                            };
                            _context.AssignModules.Add(assignModule);

                            // Insert into LecturerTimetable
                            var lecturerTimetable = new LecturerTimetable
                            {
                                LecturerId = request.LecturerId,
                                Description = "Lectures",
                                Date = date
                            };
                            _context.LecturerTimetables.Add(lecturerTimetable);
                        }

                        await _context.SaveChangesAsync();
                        await transaction.CommitAsync();

                        return new GenerateTimetableResponseDTO
                        {
                            Success = true,
                            Message = "Timetable successfully generated",
                            GeneratedDates = generatedDates.Select(d => d.ToString("yyyy-MM-dd")).ToList()
                        };
                    }
                    catch (Exception)
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
                else
                {
                    return new GenerateTimetableResponseDTO
                    {
                        Success = false,
                        Message = "Module hours not supported"
                    };
                }
            }
            catch (Exception ex)
            {
                return new GenerateTimetableResponseDTO
                {
                    Success = false,
                    Message = "Database error",
                    Details = ex.Message
                };
            }
        }

        private async Task<List<DateTime>> GenerateRandomDatesAsync(
            DateTime startDate,
            DateTime endDate,
            List<DateTime> lecturerExistingDates,
            List<DateTime> batchExistingDates,
            int requiredCount)
        {
            var generatedDates = new List<DateTime>();
            var startDateOnly = startDate.Date;
            var endDateOnly = endDate.Date;
            var totalDays = (int)(endDateOnly - startDateOnly).TotalDays + 1;

            // Create a list of all existing dates (combined)
            var allExistingDates = lecturerExistingDates.Concat(batchExistingDates).Distinct().ToList();

            int attempts = 0;
            int maxAttempts = totalDays * 10; // Prevent infinite loop

            while (generatedDates.Count < requiredCount && attempts < maxAttempts)
            {
                attempts++;

                // Generate random date within range
                int randomDays = _random.Next(0, totalDays);
                var randomDate = startDateOnly.AddDays(randomDays);

                // Check if date is not already used
                if (!allExistingDates.Contains(randomDate) && !generatedDates.Contains(randomDate))
                {
                    generatedDates.Add(randomDate);
                }

                // Break if we've exhausted all possible dates
                if (generatedDates.Count + allExistingDates.Count >= totalDays)
                {
                    break;
                }
            }

            return generatedDates;
        }
    }
}
