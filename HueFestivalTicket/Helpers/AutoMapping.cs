﻿using AutoMapper;
using HueFestivalTicket.Data;
using HueFestivalTicket.Models;

namespace HueFestivalTicket.Helpers
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Role, RoleDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Event, EventDTO>().ReverseMap();
            CreateMap<ImageEvent, ImageEventDTO>().ReverseMap();
            CreateMap<Support, SupportDTO>().ReverseMap();
            CreateMap<Location, LocationDTO>().ReverseMap();
            CreateMap<Account, AccountDTO>().ReverseMap();
            CreateMap<Customer, CustomerDTO>().ReverseMap();
            CreateMap<Invoice, InvoiceDTO>().ReverseMap();
        }
    }
}
