using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using WebApplicationNew.Models;
using Microsoft.AspNet.Identity;

namespace WebApplicationNew.App_Start
{
    public static class AutoMapperConfig
    {
        public static void RegisterConfig()
        {
            Mapper.Initialize(config => config. CreateMap<ApplicationUser, UserViewModel>().ReverseMap());
        }
    }
}