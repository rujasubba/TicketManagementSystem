using System.ComponentModel.DataAnnotations;

namespace TicketManagementSystem.DTOs.Ticket
{
    public class UpdateTicketDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is Required"), MaxLength(150)]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Please select Priority")]
        public int PriorityId { get; set; }
        [Required(ErrorMessage = "Please select Category")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Please select Status")]
        public int StatusId { get; set; }
        public int DepartmentId { get; set; }
    }
}
