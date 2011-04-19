using System;
using AutoMapper;
using Evil.Lairs;
using Evil.Web.Models;

namespace Evil.Web.Initialization
{
    public class ModelProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Lair, LairModel>();
        }
    }
}