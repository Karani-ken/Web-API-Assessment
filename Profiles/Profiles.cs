using AutoMapper;
using Web_API_Assessment.Models;
using Web_API_Assessment.Requests;

namespace Web_API_Assessment.Profiles
{
    public class Profiles:Profile
    {
        public Profiles()
        {
            CreateMap<AddUser, User>().ReverseMap();
            CreateMap<AddEvent, Event>().ReverseMap();
        }
    }
}
