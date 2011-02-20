using System.Security.Principal;
using System.Web;
using Evil.Common;
using Evil.Users;

namespace Evil.Web.Services
{
    public interface IUserProvider
    {
        Account CurrentAccount();
        Player CurrentPlayer();
    }

    public class UserProvider : IUserProvider
    {
        private readonly IRepository<Account> _accountRepository;
        private readonly IRepository<Player> _playerRepository;
        private readonly IHttpSession _httpSession;
        private const string PlayerKey = "Player";
        private const string AccountKey = "Account";

        /// <summary>
        /// Testing seam because HttpContext.Current.User is annoying.
        /// </summary>
        protected virtual IPrincipal User
        {
            get
            {
                return HttpContext.Current.User;
            }
        }

        public UserProvider(IRepository<Account> accountRepository, IRepository<Player> playerRepository, 
            IHttpSession httpSession)
        {
            _accountRepository = accountRepository;
            _playerRepository = playerRepository;
            _httpSession = httpSession;
        }

        public Account CurrentAccount()
        {
            if (User.Identity.IsAuthenticated)
            {
                var account = _httpSession[AccountKey] as Account;
                if(account == null)
                {
                    account = _accountRepository.Get.ByEmailAddress(User.Identity.Name);
                    _httpSession[AccountKey] = account;
                }
                return account;
            }
            return null;
        }

        public Player CurrentPlayer()
        {
            if (User.Identity.IsAuthenticated)
            {
                var player = _httpSession[PlayerKey] as Player;
                if (player == null)
                {
                    var account = CurrentAccount();
                    if (account != null)
                    {
                        player = _playerRepository.Get.CurrentPlayerFor(account);
                        _httpSession[PlayerKey] = player;
                    }
                }
                return player;
            }
            return null;
        }
    }
}