using Microsoft.EntityFrameworkCore;
using Web_API_Assessment.Data;
using Web_API_Assessment.Models;
using Web_API_Assessment.Requests;
using Web_API_Assessment.Services.IServices;

namespace Web_API_Assessment.Services
{
    public class UserServices : IUserInterface
    {
        private readonly AppDbContext _context;

        public UserServices(AppDbContext context)
        {
                _context= context;
        }
      

        public async Task<string> RegisterUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return "User added successfully";
           
        }

      public  async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
           
        }

        async Task<string> IUserInterface.BookEvent(BookEvent bookevent)
        {
            var User = await _context.Users.Where(u => u.Id == bookevent.UserId).FirstOrDefaultAsync();
            var Event = await _context.Events.Where(e => e.Id == bookevent.EventId).FirstOrDefaultAsync();
            if(User != null && Event!= null)
            {
                User.Events.Add(Event);
                await _context.SaveChangesAsync();
                return ("Course Purchased");
            }
            throw new Exception("Invalid ids");
        }
    }
}
