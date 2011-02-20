using Evil.Web.Controllers;
using MbUnit.Framework;

// ReSharper disable PossibleNullReferenceException
// ReSharper disable InconsistentNaming

namespace Evil.Web.UnitTests.Controllers.Home
{
    [TestFixture]
    public class When_displaying_welcome_screen
    {
        private HomeController _controller;

        [SetUp]
        public void SetUp()
        {
            _controller = new HomeController();
            
        }

        [Test]
        public void should_display_return_to_game_when_game_in_progress()
        {
            
        }
    }
}

// ReSharper restore PossibleNullReferenceException
// ReSharper restore InconsistentNaming