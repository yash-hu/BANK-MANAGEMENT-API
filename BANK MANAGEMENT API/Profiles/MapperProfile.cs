

using AutoMapper;
using BANK_MANAGEMENT_API.DTOs;
using BANK_MANAGEMENT_API.Models;

public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<Customer,CustomerUpdateDTO>().ReverseMap();
            CreateMap<Account,AccountsListDTO>().ReverseMap();
        }
    }

