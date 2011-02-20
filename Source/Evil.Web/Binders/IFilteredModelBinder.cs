using System;
using System.Web.Mvc;

namespace Evil.Web.Binders
{
    public interface IFilteredModelBinder : IModelBinder
    {
        bool IsMatch(Type type);
    }
}