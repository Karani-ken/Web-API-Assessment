using Web_API_Assessment.Models;

namespace Web_API_Assessment.Services.IServices
{
    public interface IEventInterface
    {
        //Create Event
        Task<string> CreateEvent(Event Event);

        //Get slots available
        Task<Event> RemainingSlots(Guid id);

        //return events based on location
        Task<IEnumerable<Event>> basedOnLocation(string? location);
        //get all users for an events
        Task<IEnumerable<User>> GetAllUsers(Guid id);


    }
}
