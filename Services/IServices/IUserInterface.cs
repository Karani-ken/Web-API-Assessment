using Web_API_Assessment.Models;
using Web_API_Assessment.Requests;

namespace Web_API_Assessment.Services.IServices
{
    public interface IUserInterface
    {
        //Register user
        Task<string> RegisterUser(User user);
        //get All Users
        Task <IEnumerable<User>> GetUsers();
        //book event
        Task<string> BookEvent(BookEvent bookevent);
       
    }
}
