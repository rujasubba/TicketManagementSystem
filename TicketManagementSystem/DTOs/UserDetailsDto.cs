using TicketManagementSystem.Models;

namespace TicketManagementSystem.DTOs
{
    public class UserDetailsDto
    {

        public string UserId { get; set; }     
        public string FirstName { get; set; }
        public string LastName { get; set; }
     
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public int? DepartmentId { get; set; } 
    }

    public class UserSelectDto
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }

    public class DepartmentSelectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class UserDetailsFormDto
    {
        public List<UserSelectDto> Users { get; set; }
        public List<DepartmentSelectDto> Departments { get; set; }
    }
}
