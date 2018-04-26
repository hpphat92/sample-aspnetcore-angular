using AutoMapper;
using AutoMapper.QueryableExtensions;
using NewApp.Entities;
using NewApp.Models.DTOs;
using System;
using System.Linq;

namespace NewApp.Models
{
    public class AutoMapperProfileConfig : Profile
    {
        public AutoMapperProfileConfig() : this("MapperProfile")
        {
        }

        protected AutoMapperProfileConfig(string profileName) : base(profileName)
        {
            CreateMap<Category, CategoryModel>();
            CreateMap<CategoryModel, Category>();
        }
    }
}
