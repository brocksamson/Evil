using AutoMapper;
using Evil.Common;

namespace Evil.Web.Initialization
{
    public class RegisterMaps :IStartupTask
    {
        public void Execute()
        {
            Mapper.Initialize(config => config.AddProfile(new LairModelProfile()));
        }
    }
}