using Evil.Web.Models;
using MbUnit.Framework;

namespace Evil.Web.UnitTests.ViewModels
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class when_validating_player_creation : ValidationTestBase<CreatePlayerView>
    {
        private const double _latitude = 37.771008;
        private const double _longitude = -122.41175;

        protected override CreatePlayerView Model
        {
            get
            {
                return new CreatePlayerView
                           {
                               Name = "Dr. Evil",
                               BaseName = "My Base",
                               BaseLatitude = _latitude,
                               BaseLongitude = _longitude
                           };
            }
        }

        [Test]
        public void base_name_is_required()
        {
            AssertFieldError(m => m.Name, null);
        }

        [Test]
        public void latitude_must_be_greater_then_negative_90()
        {
            AssertFieldError(m => m.BaseLatitude, -91);
        }

        [Test]
        public void latitude_must_be_less_then_90()
        {
            AssertFieldError(m => m.BaseLatitude, 91);
        }

        [Test]
        public void longitude_must_be_greater_then_negative_180()
        {
            AssertFieldError(m => m.BaseLongitude, -181);
        }

        [Test]
        public void longitude_must_be_less_then_180()
        {
            AssertFieldError(m => m.BaseLongitude, 181);
        }
    }
    // ReSharper restore InconsistentNaming
}