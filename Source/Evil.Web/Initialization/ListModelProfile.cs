using System;
using AutoMapper;
using Evil.Lairs;
using Evil.Web.Models;

namespace Evil.Web.Initialization
{
    public class LairModelProfile : Profile
    {
        protected override void Configure()
        {
            this.CreateMap<Lair, LairModel>();
        }
    }
}