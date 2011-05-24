using Evil.Infrastructure.Nhibernate;
using Evil.Users;
using MbUnit.Framework;

namespace Evil.Infrastructure.Tests.DataAccess
{
    [TestFixture]
    public class When_retrieving_players : DbTest
    {
        private Repository<Player> _playerRepository;
        private Account _account;
        private Player _player;

        protected override void Arrange()
        {
            _account = When_retrieving_account.CreateNewAccount();
            _player = new Player
                          {
                              Account = _account,
                              Name = "test1"
                          };
            _playerRepository = new Repository<Player>(_session);
            _playerRepository.Save(_player);
        }

        [Test]
        public void Should_get_by_account()
        {
            var player = _playerRepository.Get.CurrentPlayerFor(_account);
            Assert.IsNotNull(player);
        }
    }
}