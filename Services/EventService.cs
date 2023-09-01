﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Web_API_Assessment.Data;
using Web_API_Assessment.Models;
using Web_API_Assessment.Services.IServices;

namespace Web_API_Assessment.Services
{
    public class EventService : IEventInterface
    {
        private readonly AppDbContext _context;

        public EventService(AppDbContext context)
        {
            _context = context;
        }
      
        public async Task<IEnumerable<Event>> basedOnLocation(string? location)
        {
          
           return await _context.Events.Where(e=>e.Location.Contains(location)).ToListAsync();
        }

        public async Task<string> CreateEvent(Event Event)
        {
            _context.Events.Add(Event);
            await _context.SaveChangesAsync();
            return "Event added";
           
        }

        public async Task<IEnumerable<User>> GetAllUsers(Guid id)
        {
            var Event = await _context.Events.Where(e => e.Id == id).FirstOrDefaultAsync();
             var Users = Event.Users.ToList();
            return Users;
           
        }

        public async Task<Event> RemainingSlots(Guid id)
        {
            return await _context.Events.Where(e => e.Id == id).FirstOrDefaultAsync();
           
        }
    }
}