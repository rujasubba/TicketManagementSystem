namespace TicketManagementSystem.Models
{
    public class Status
    {
        public int StatusId { get; set; }
        public string Name { get; set; }
        public ICollection<Ticket> Tickets {get; set; }

    }
}
