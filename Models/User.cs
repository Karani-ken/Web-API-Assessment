namespace Web_API_Assessment.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string password { get; set; }
        public int Phone { get; set; }

        public string Email { get; set; }

        public string Role { get; set; } = "User";

        public List<Event> Events { get; set; } = new List<Event>();
    }
}
