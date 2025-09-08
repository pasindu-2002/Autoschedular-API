using Microsoft.EntityFrameworkCore;
using autoschedular.Model;
using autoschedular.Model.DTOs;

namespace autoschedular.Services.implementation
{
    public class PoStaffService : IPoStaffService
    {
        private readonly AutoSchedularDbContext _context;

        public PoStaffService(AutoSchedularDbContext context)
        {
            _context = context;
        }

        public async Task<PoStaffResponseDto?> GetEmployeeAsync(string empNo, string password)
        {
            var employee = await _context.PoStaffs
                .FirstOrDefaultAsync(p => p.EmpNo == empNo);

            if (employee == null)
            {
                return null; // Employee not found
            }

            // Verify password
            if (!BCrypt.Net.BCrypt.Verify(password, employee.Password))
            {
                return null; // Invalid password
            }

            return new PoStaffResponseDto
            {
                EmpNo = employee.EmpNo,
                FullName = employee.FullName,
                Email = employee.Email
            };
        }

        public async Task<bool> CreateEmployeeAsync(CreatePoStaffDto createPoStaffDto)
        {
            try
            {
                // Check if employee already exists
                var existingEmployee = await _context.PoStaffs
                    .AnyAsync(p => p.EmpNo == createPoStaffDto.EmpNo);

                if (existingEmployee)
                {
                    return false; // Employee already exists
                }

                // Hash the password
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(createPoStaffDto.Password);

                var employee = new PoStaff
                {
                    EmpNo = createPoStaffDto.EmpNo,
                    FullName = createPoStaffDto.FullName,
                    Email = createPoStaffDto.Email,
                    Password = hashedPassword
                };

                _context.PoStaffs.Add(employee);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateEmployeeAsync(string empNo, UpdatePoStaffDto updatePoStaffDto)
        {
            try
            {
                var employee = await _context.PoStaffs
                    .FirstOrDefaultAsync(p => p.EmpNo == empNo);

                if (employee == null)
                {
                    return false; // Employee not found
                }

                bool hasChanges = false;

                if (!string.IsNullOrEmpty(updatePoStaffDto.FullName))
                {
                    employee.FullName = updatePoStaffDto.FullName;
                    hasChanges = true;
                }

                if (!string.IsNullOrEmpty(updatePoStaffDto.Email))
                {
                    employee.Email = updatePoStaffDto.Email;
                    hasChanges = true;
                }

                if (!string.IsNullOrEmpty(updatePoStaffDto.Password))
                {
                    employee.Password = BCrypt.Net.BCrypt.HashPassword(updatePoStaffDto.Password);
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

        public async Task<bool> DeleteEmployeeAsync(string empNo)
        {
            try
            {
                var employee = await _context.PoStaffs
                    .FirstOrDefaultAsync(p => p.EmpNo == empNo);

                if (employee == null)
                {
                    return false; // Employee not found
                }

                _context.PoStaffs.Remove(employee);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> EmployeeExistsAsync(string empNo)
        {
            return await _context.PoStaffs.AnyAsync(p => p.EmpNo == empNo);
        }
    }
}
