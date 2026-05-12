namespace TicketManagementSystem.DTOs.Ticket
{
    public class TicketFilterDto
    {
        public string? Search { get; set; }           // title/description keyword
        public int? PriorityId { get; set; }
        public int? StatusId { get; set; }
        public int? CategoryId { get; set; }
        public int? DepartmentId { get; set; }
        public string SortBy { get; set; } = "CreatedDate";
        public string SortDir { get; set; } = "desc";  // "asc" | "desc"
    }
}
