using Evil.Common;
using MbUnit.Framework;

namespace Evil.Web.Tests.TestHelpers
{
    public static class ModelExtensions
    {
        public static void AssertIsValid(this Position position)
        {
            Assert.Between(position.Latitude, -90, 90,"Latitude was not valid");
            Assert.Between(position.Longitude, -180, 180, "Longitude was not valid");
        }

        public static void AssertIsEqualTo(this Position position, Position other)
        {
            Assert.AreEqual(position.Latitude, other.Latitude);
            Assert.AreEqual(position.Longitude, other.Longitude);
        }
    }
}
