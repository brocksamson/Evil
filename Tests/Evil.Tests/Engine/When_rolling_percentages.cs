using System;
using Evil.Engine;
using MbUnit.Framework;

namespace Evil.Tests.Engine
{
    [TestFixture]
    public class When_rolling_percentages
    {
        [Test]
        [Row(0, .05)]
        [Row(5, .05)]
        [Row(1, .2)]
        [Row(20, .2)]
        public void Should_evaluate_to_true_if_less_than_equal(int randomResult, decimal maxTrue)
        {
            var dice = new Dice(m => randomResult);
            Assert.IsTrue(dice.RollPercentage(maxTrue));
        }

        [Test]
        [Row(6, .05)]
        [Row(50, .05)]
        [Row(21, .20)]
        public void Should_evaluate_to_false_if_greater_than(int randomResult, int maxTrue)
        {
            var dice = new Dice(m => randomResult);
            Assert.IsFalse(dice.RollPercentage(maxTrue));
        }

    }
}
