namespace TicketManagementSystem.DTOs
{
    public class UserListDto
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public int? Age { get; set; }
        public string? Address { get; set; }
        public string? DepartmentName { get; set; }
        public bool HasDetails { get; set; }
    }
}
