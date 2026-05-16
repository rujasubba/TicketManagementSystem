using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TicketManagementSystem.DTOs;
using TicketManagementSystem.Interfaces;
using TicketManagementSystem.Models;
using TicketManagementSystem.Persistent;

namespace TicketManagementSystem.Services
{
    public class UserDetailsService(
       AppDbContext dbContext,
       UserManager<User> userManager) : IUserDetailsService
    {
        public async Task<UserDetailsFormDto> GetFormDataAsync()
        {
            var users = await userManager.Users
                .Select(u => new UserSelectDto
                {
                    Id = u.Id,
                    DisplayName = u.FirstName + " " + u.LastName,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    Address = u.Address
                })
                .ToListAsync();

            var departments = await dbContext.Departments
                .Select(d => new DepartmentSelectDto
                {
                    Id = d.DepartmentId,
                    Name = d.Name
                })
                .ToListAsync();

            return new UserDetailsFormDto
            {
                Users = users,
                Departments = departments
            };
        }

        public async Task<UserDetailsDto> CreateAsync(UserDetailsDto dto)
        {
            var user = await userManager.FindByIdAsync(dto.UserId);
            if (user == null)
                throw new Exception("User not found");

            var userDetails = new UserDetails
            {
                UserId = dto.UserId,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                FullName = $"{dto.FirstName} {dto.LastName}",
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Age = dto.Age,
                Address = dto.Address,
                DepartmentId = dto.DepartmentId
            };

            await dbContext.UserDetails.AddAsync(userDetails);
            await dbContext.SaveChangesAsync();

            return dto;
        }

        public async Task<List<UserListDto>> GetAllAsync()
        {
            return await dbContext.UserDetails
                .Include(ud => ud.User)
                .Include(ud => ud.Department)
                .Select(ud => new UserListDto
                {
                    UserId = ud.UserId,
                    FirstName = ud.FirstName,
                    LastName = ud.LastName,
                    
                    Email = ud.Email,
                    PhoneNumber = ud.PhoneNumber,
                    Age = ud.Age,
                    Address = ud.Address,
                    DepartmentName = ud.Department != null ? ud.Department.Name : "N/A"
                })
                .ToListAsync();
        }

        public async Task<UserDetailsDto> GetByIdAsync(int id)
        {
            var ud = await dbContext.UserDetails
                .Include(u => u.User)
                .Include(u => u.Department)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (ud == null)
                throw new Exception("UserDetails not found");

            return new UserDetailsDto
            {
                Id = ud.Id,
                UserId = ud.UserId,
                FirstName = ud.FirstName,
                LastName = ud.LastName,
                Email = ud.Email,
                PhoneNumber = ud.PhoneNumber,
                Age = ud.Age,
                Address = ud.Address,
                DepartmentId = ud.DepartmentId
            };
        }

        public async Task<List<UserListDto>> GetAllUsersAsync()
        {
            var users = await userManager.Users.ToListAsync();

            var userDetails = await dbContext.UserDetails
                .Include(ud => ud.Department)
                .ToListAsync();

            var result = users.Select(u =>
            {
                var details = userDetails.FirstOrDefault(ud => ud.UserId == u.Id);

                return new UserListDto
                {
                    UserId = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,

                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    IsActive = u.IsActive,
                    Age = details?.Age,
                    Address = details?.Address,
                    DepartmentName = details?.Department?.Name,
                    HasDetails = details != null
                };
            }).ToList();

            return result;
        }
    }
}
