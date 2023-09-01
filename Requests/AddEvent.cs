namespace Web_API_Assessment.Requests
{
    public class AddEvent
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }

        public int TicketAmount { get; set; }
    }
}
