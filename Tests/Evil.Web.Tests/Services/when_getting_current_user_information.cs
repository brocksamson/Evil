using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Principal;
using Evil.Common;
using Evil.Users;
using Evil.Web.Services;
using MbUnit.Framework;
using Rhino.Mocks;

// ReSharper disable InconsistentNaming
// ReSharper disable PossibleNullReferenceException

namespace Evil.Web.Tests.Services
{
    [TestFixture]
    public class when_getting_current_user_information
    {
        private UserProvider _userProvider;
        private Player _sessionPlayer;
        private Player _databasePlayer;
        private Account _databaseAccount;
        private Account _sessionAccount;
        private IHttpSession _httpSession;
        private IPrincipal _user;
        private IIdentity _identity;
        private IRepository<Account> _accountRepository;
        private IRepository<Player> _playerRepository;


        [SetUp]
        public void SetUp()
        {
            _sessionPlayer = new Player();
            _databaseAccount = new Account {EmailAddress = "email@address.com"};
            _databasePlayer = new Player { Account = _databaseAccount };
            _sessionAccount = new Account();

            _httpSession = MockRepository.GenerateMock<IHttpSession>();
            _user = MockRepository.GenerateMock<IPrincipal>();
            _identity = MockRepository.GenerateMock<IIdentity>();
            _accountRepository = MockRepository.GenerateMock<IRepository<Account>>();
            _playerRepository = MockRepository.GenerateMock<IRepository<Player>>();

            _user.Stub(m => m.Identity).Return(_identity);
            _identity.Stub(m => m.Name).Return(_databaseAccount.EmailAddress);
            _accountRepository.Stub(m => m.Get).Return((new Collection<Account>{_databaseAccount}).AsQueryable());

            _userProvider = new TestUserProvider(_accountRepository, _playerRepository, _httpSession, _user);
        }

        [Test]
        public void player_returns_null_if_not_authenticated()
        {
            _identity.Stub(m => m.IsAuthenticated).Return(false);
            Assert.IsNull(_userProvider.CurrentPlayer());
        }

        [Test]
        public void returns_null_if_no_current_player()
        {
            _playerRepository.Stub(m => m.Get).Return(new Collection<Player>().AsQueryable());
            _identity.Stub(m => m.IsAuthenticated).Return(true);
            Assert.IsNull(_userProvider.CurrentPlayer());
        }

        [Test]
        public void returns_current_player_from_session()
        {
            _httpSession.Stub(m => m["Player"]).Return(_sessionPlayer);
            _identity.Stub(m => m.IsAuthenticated).Return(true);
            var player = _userProvider.CurrentPlayer();
            Assert.AreEqual(player, _sessionPlayer);
        }

        [Test]
        public void retrieves_player_and_stores_in_session()
        {
            _playerRepository.Stub(m => m.Get).Return((new Collection<Player>{_databasePlayer}).AsQueryable());
            _identity.Stub(m => m.IsAuthenticated).Return(true);
            var player = _userProvider.CurrentPlayer();
            Assert.AreEqual(player, _databasePlayer);
            _httpSession.AssertWasCalled(m => m["Player"] = _databasePlayer);
        }

        [Test]
        public void account_returns_null_if_not_authenticated()
        {
            _identity.Stub(m => m.IsAuthenticated).Return(false);
            Assert.IsNull(_userProvider.CurrentAccount());
        }

        [Test]
        public void returns_account_from_session()
        {
            _identity.Stub(m => m.IsAuthenticated).Return(true);
            _httpSession.Stub(m => m["Account"]).Return(_sessionAccount);
            var account = _userProvider.CurrentAccount();
            Assert.AreEqual(account, _sessionAccount);
        }

        [Test]
        public void retrieves_account_and_stores_in_session()
        {
            _identity.Stub(m => m.IsAuthenticated).Return(true);
            var account = _userProvider.CurrentAccount();
            Assert.AreEqual(account, _databaseAccount);
            _httpSession.AssertWasCalled(m => m["Account"] = _databaseAccount);
        }
    }

    public class TestUserProvider : UserProvider
    {
        protected override IPrincipal User
        {
            get
            {
                return _user;
            }
        }

        private readonly IPrincipal _user;

        public TestUserProvider(IRepository<Account> accountRepository, IRepository<Player> playerRepository, IHttpSession httpSession, IPrincipal user) 
            : base(accountRepository, playerRepository, httpSession)
        {
            _user = user;
        }
    }
}

// ReSharper restore InconsistentNaming
// ReSharper restore PossibleNullReferenceException
