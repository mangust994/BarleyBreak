using AutoMapper;
using Core.Entities;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Infrastructures
{
    public class EnterpriseProfile : Profile
    {
        public EnterpriseProfile()
        {
            CreateMap<Game, GameModel>().ReverseMap();
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<Button, ButtonModel>().ReverseMap();
        }
    }
}
