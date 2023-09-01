namespace Web_API_Assessment.Models
{
    public class Event
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }

        public int TicketAmount { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        public List<User> Users { get; set; } = new List<User>();
    }
}
