using System;
using System.Web.Mvc;
using Evil.Users;
using Evil.Web.Services;

namespace Evil.Web.Binders
{
    public class AccountBinder : IFilteredModelBinder
    {
        private readonly IUserProvider _userProvider;

        public AccountBinder(IUserProvider userProvider)
        {
            _userProvider = userProvider;
        }

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            return _userProvider.CurrentAccount();
        }

        public bool IsMatch(Type type)
        {
            return type == typeof (Account);
        }
    }
}