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

        //Get user by email

        Task<User> GetUserByEmail(string email);

        //Delete User
        Task<string> DeleteUser(User user);
        //update user
        Task<string> UpdateUserById(User user);
        //get user by Id
        Task<User> GetUserById(Guid id);
       
    }
}
