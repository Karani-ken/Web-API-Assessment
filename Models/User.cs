namespace Web_API_Assessment.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public int Phone { get; set; }

        public string Email { get; set; }

        public List<Event> Events { get; set; } = new List<Event>();
    }
}
