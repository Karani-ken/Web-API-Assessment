using Web_API_Assessment.Models;

namespace Web_API_Assessment.Services.IServices
{
    public interface IEventInterface
    {
        //Create Event
        Task<string> CreateEvent(Event Event);
        //update events
        Task<string> UpdateEvent(Event Event);
        //Delete Event
        Task<string> DeleteEvent(Event Event);

        //Get event by id
        Task<Event> GetEventById(Guid id);

        //get all events
        Task<IEnumerable<Event>> GetAllEvents();

        //return events based on location
        Task<IEnumerable<Event>> basedOnLocation(string? location);
        //get all users for an events
        Task<IEnumerable<User>> GetAllUsers(Guid id);


    }
}
