﻿using AutoMapper;
using HueFestivalTicket.Contexts;
using HueFestivalTicket.Data;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.IRepositories;
using HueFestivalTicket.Repositories.RepositoryService;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace HueFestivalTicket.Repositories
{
    public class EventLocationRepository : RepositoryBase<EventLocation>, IEventLocationRepository
    {
        private readonly IMapper _mapper;

        public EventLocationRepository(ApplicationDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task DeleteEventLocationAsync(EventLocation eventLocation)
        {
            await DeleteAsync(eventLocation);
        }

        public async Task<EventLocation?> GetEventLocationByIdAsync(Guid id)
        {
            var result = await _dbSet.FirstOrDefaultAsync(el => el.IdEventLocation == id);
            return result;
        }

        public async Task<List<EventLocation>> GetAllEventLocationsAsync()
        {
            return await GetAllWithIncludesAsync(el => el.Event!, el => el.Location!.TypeLocation!);
        }

        public async Task<EventLocation> InsertEventLocationAsync(EventLocationDTO eventLocation)
        {
            var newEventLocation = new EventLocation
            {
                DateStart = DateTime.ParseExact(eventLocation.DateStart!, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                DateEnd = DateTime.ParseExact(eventLocation.DateEnd!, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                Time = DateTime.ParseExact(eventLocation.Time!, "HH:mm", CultureInfo.InvariantCulture),
                NumberSlot = eventLocation.NumberSlot,
                Price = eventLocation.Price,
                IdEvent = eventLocation.IdEvent,
                IdLocation = eventLocation.IdLocation
            };
            await InsertAsync(newEventLocation);
            return newEventLocation;
        }

        public async Task UpdateEventLocationAsync(EventLocation oldEventLocation, EventLocationDTO newEventLocation)
        {
            oldEventLocation.DateStart = DateTime.ParseExact(newEventLocation.DateStart!, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            oldEventLocation.DateEnd = DateTime.ParseExact(newEventLocation.DateEnd!, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            oldEventLocation.Time = DateTime.ParseExact(newEventLocation.Time!, "HH:mm", CultureInfo.InvariantCulture);
            oldEventLocation.NumberSlot = newEventLocation.NumberSlot;
            oldEventLocation.Price = newEventLocation.Price;
            oldEventLocation.IdEvent = newEventLocation.IdEvent;
            oldEventLocation.IdLocation = newEventLocation.IdLocation;

            await UpdateAsync(oldEventLocation);
        }

        public async Task<string> CheckDateTimeEventLocation(EventLocationDTO eventLocation)
        {
            if (eventLocation.DateStart == null || eventLocation.DateEnd == null || eventLocation.Time == null)
            {
                return "Please enter Date and Time";
            }
            DateTime? dStart = DateTime.ParseExact(eventLocation.DateStart, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime? dEnd = DateTime.ParseExact(eventLocation.DateEnd, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime? elTime = DateTime.ParseExact(eventLocation.Time, "HH:mm", CultureInfo.InvariantCulture);

            var checkDateStart = await _dbSet.FirstOrDefaultAsync(cds => cds.DateStart <= dStart && cds.DateEnd >= dStart && cds.IdLocation == eventLocation.IdLocation);
            if (checkDateStart != null)
            {
                return "Unable to start at this time at this location";
            }
            if (dStart <= DateTime.UtcNow)
            {
                return "DateStart Invalid";
            }
            if (dEnd < dStart)
            {
                return "DateEnd Invalid";
            }
            return string.Empty;
        }

        public async Task<string> CheckDateTimeEventLocationToUpdate(Guid id, EventLocationDTO eventLocation)
        {
            if (eventLocation.DateStart == null || eventLocation.DateEnd == null || eventLocation.Time == null)
            {
                return "Please enter Date and Time";
            }
            DateTime? dStart = DateTime.ParseExact(eventLocation.DateStart, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime? dEnd = DateTime.ParseExact(eventLocation.DateEnd, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime? elTime = DateTime.ParseExact(eventLocation.Time, "HH:mm", CultureInfo.InvariantCulture);

            var checkDateStart = await _dbSet.FirstOrDefaultAsync(cds => cds.DateStart <= dStart && cds.DateEnd >= dStart && cds.IdLocation == eventLocation.IdLocation && cds.IdEventLocation != id);
            if (checkDateStart != null)
            {
                return "Unable to start at this time at this location";
            }
            if (dStart <= DateTime.UtcNow)
            {
                return "DateStart Invalid";
            }
            if (dEnd < dStart)
            {
                return "DateEnd Invalid";
            }
            return string.Empty;
        }
    }
}
