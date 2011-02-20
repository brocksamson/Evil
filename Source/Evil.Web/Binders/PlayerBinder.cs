using System;
using System.Web.Mvc;
using Evil.Users;
using Evil.Web.Services;

namespace Evil.Web.Binders
{
    public class PlayerBinder : IFilteredModelBinder
    {
        private readonly IUserProvider _userProvider;

        public PlayerBinder(IUserProvider userProvider)
        {
            _userProvider = userProvider;
        }

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            return _userProvider.CurrentPlayer();
        }

        public bool IsMatch(Type type)
        {
            return type == typeof (Player);
        }
    }
}