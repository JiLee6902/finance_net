using Application.IService;
using AutoMapper;
using BusinessObject.DTOs;
using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service
{
    public class AutoMapperService : Profile
    {
        public AutoMapperService()
        {
            CreateMap<Budget, BudgetDTO>().ReverseMap();
            CreateMap<Transaction, TransactionDTO>().ReverseMap();
            CreateMap<Transaction, AddTransactionDTO>().ReverseMap();
            CreateMap<User, UserDTO>()
              .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
              .ForMember(dest => dest.AccountId, opt => opt.MapFrom(src => src.AccountId))
              .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.DisplayName))
              .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
              .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.Balance))
              .ForMember(dest => dest.Birthdate, opt => opt.MapFrom(src => src.Birthdate))
              .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Account.UserName))
              .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Account.Email))
              .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Account.Password))
              .ReverseMap();
            CreateMap<User, AddUserDTO>().ReverseMap();
        }
    }
}
