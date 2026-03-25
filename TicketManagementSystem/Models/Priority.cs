namespace TicketManagementSystem.Models
{
    public class Priority
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}
