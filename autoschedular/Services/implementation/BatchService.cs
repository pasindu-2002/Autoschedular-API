using Microsoft.EntityFrameworkCore;
using autoschedular.Model;
using autoschedular.Model.DTOs;

namespace autoschedular.Services.implementation
{
    public class BatchService : IBatchService
    {
        private readonly AutoSchedularDbContext _context;

        public BatchService(AutoSchedularDbContext context)
        {
            _context = context;
        }

        public async Task<BatchResponseDto?> GetBatchAsync(string batchCode)
        {
            var batch = await _context.Batches
                .Include(b => b.Lecturer)
                .Where(b => b.BatchCode == batchCode)
                .Select(b => new BatchResponseDto
                {
                    BatchCode = b.BatchCode,
                    CourseCode = b.CourseCode,
                    CourseDirector = b.CourseDirector,
                    LecturerDetails = b.Lecturer != null ? new LecturerInfo
                    {
                        EmpNo = b.Lecturer.EmpNo,
                        FullName = b.Lecturer.FullName,
                        Email = b.Lecturer.Email
                    } : null
                })
                .FirstOrDefaultAsync();

            return batch;
        }

        public async Task<List<BatchResponseDto>> GetAllBatchesAsync()
        {
            var batches = await _context.Batches
                .Include(b => b.Lecturer)
                .Select(b => new BatchResponseDto
                {
                    BatchCode = b.BatchCode,
                    CourseCode = b.CourseCode,
                    CourseDirector = b.CourseDirector,
                    LecturerDetails = b.Lecturer != null ? new LecturerInfo
                    {
                        EmpNo = b.Lecturer.EmpNo,
                        FullName = b.Lecturer.FullName,
                        Email = b.Lecturer.Email
                    } : null
                })
                .ToListAsync();

            return batches;
        }

        public async Task<bool> CreateBatchAsync(CreateBatchDto createBatchDto)
        {
            try
            {
                // Check if batch already exists
                var existingBatch = await _context.Batches
                    .AnyAsync(b => b.BatchCode == createBatchDto.BatchCode);

                if (existingBatch)
                {
                    return false; // Batch already exists
                }

                // Validate that the lecturer exists
                var lecturerExists = await _context.Lecturers
                    .AnyAsync(l => l.EmpNo == createBatchDto.CourseDirector);

                if (!lecturerExists)
                {
                    return false; // Lecturer not found
                }

                var batch = new Batch
                {
                    BatchCode = createBatchDto.BatchCode,
                    CourseCode = createBatchDto.CourseCode,
                    CourseDirector = createBatchDto.CourseDirector
                };

                _context.Batches.Add(batch);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateBatchAsync(string batchCode, UpdateBatchDto updateBatchDto)
        {
            try
            {
                var batch = await _context.Batches
                    .FirstOrDefaultAsync(b => b.BatchCode == batchCode);

                if (batch == null)
                {
                    return false; // Batch not found
                }

                bool hasChanges = false;

                if (!string.IsNullOrEmpty(updateBatchDto.CourseCode))
                {
                    batch.CourseCode = updateBatchDto.CourseCode;
                    hasChanges = true;
                }

                if (!string.IsNullOrEmpty(updateBatchDto.CourseDirector))
                {
                    // Validate that the lecturer exists
                    var lecturerExists = await _context.Lecturers
                        .AnyAsync(l => l.EmpNo == updateBatchDto.CourseDirector);

                    if (!lecturerExists)
                    {
                        return false; // Lecturer not found
                    }

                    batch.CourseDirector = updateBatchDto.CourseDirector;
                    hasChanges = true;
                }

                if (!hasChanges)
                {
                    return false; // No changes to make
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteBatchAsync(string batchCode)
        {
            try
            {
                var batch = await _context.Batches
                    .FirstOrDefaultAsync(b => b.BatchCode == batchCode);

                if (batch == null)
                {
                    return false; // Batch not found
                }

                _context.Batches.Remove(batch);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> BatchExistsAsync(string batchCode)
        {
            return await _context.Batches.AnyAsync(b => b.BatchCode == batchCode);
        }
    }
}
