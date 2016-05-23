using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using BLL.Models;
using Lab1_des.Models;
using Models;

namespace Lab1_des.App_Start
{
    public static class AutoMapperConfig
    {
        public static void RegisterMaps()
        {
            Mapper.CreateMap<GoogleUser, User>();
            Mapper.CreateMap<RsaKeys, RSAKeysViewModel>().ReverseMap();
        }
    }
}