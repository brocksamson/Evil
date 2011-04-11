using System;
using Evil.Lairs;
using MbUnit.Framework;

// ReSharper disable PossibleNullReferenceException
// ReSharper disable InconsistentNaming

namespace Evil.Tests.Lairs.Bars
{
    public class BarTestsBase
    {
        protected Lair _bar;

        [SetUp]
        public void SetUp()
        {
            _bar = new Lair();
        }
    }
    [TestFixture]
    public class When_upgrading_the_bar : BarTestsBase
    {
         [Test]
        public void Should_be_able_to_upgrade_if_no_current_upgrade()
        {
            Assert.IsTrue(_bar.CanUpgrade());
        }

        [Test]
        public void Should_not_be_able_to_upgrade_if_current_upgrade()
        {
            _bar.BeginUpgrade();
            Assert.IsFalse(_bar.CanUpgrade());
        }

        [Test]
        public void Level_2_upgrade_should_take_1_hour()
        {
            var timeTillComplete = _bar.BeginUpgrade();
            Assert.AreEqual(timeTillComplete, new TimeSpan(1, 0,0));
        }

        [Test]
        public void Complete_upgrade_should_increase_level()
        {
            Assert.AreEqual(1, _bar.CurrentLevel);
            _bar.BeginUpgrade();
            _bar.CompleteUpgrade();
            Assert.AreEqual(2, _bar.CurrentLevel);
        }

        [Test]
        public void Bar_max_level_should_be_20()
        {
            Assert.AreEqual(20, _bar.GetMaxLevel());
            Assert.AreEqual(1, _bar.CurrentLevel);
            for (int i = 1; i < 20; i++)
            {
                _bar.BeginUpgrade();
                _bar.CompleteUpgrade();
            }
            Assert.AreEqual(20, _bar.CurrentLevel);
            Assert.Throws<InvalidOperationException>(() => _bar.BeginUpgrade());
        }
    }

    [TestFixture]
    public class When_earning_money_from_a_bar
    {
        [SetUp]
        public void Arrange()
        {
            
        }
    }

    [TestFixture]
    public class When_adding_sections_to_the_bar
    {
        [Test]
        public void Should_list_available_upgrades()
        {
            
        }

        [Test]
        public void Should_report_upgrade_complete_time()
        {
            
        }
    }

    [TestFixture]
    public class When_assigning_agents_to_the_bar
    {
        [SetUp]
        public void Arrange()
        {
            
        }

        [Test]
        public void should_get_available_agent_types()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void should_allow_bartender_to_be_assigned()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void should_allow_waiter_to_be_assigned()
        {
            throw new NotImplementedException();
        }
    }
}

// ReSharper restore PossibleNullReferenceException
// ReSharper restore InconsistentNaming