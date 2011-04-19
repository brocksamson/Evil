using System;
using AutoMapper;
using Evil.Common;
using Evil.Lairs;
using Evil.Tests.Extensions;
using Evil.Tests.TestHelpers;
using Evil.Web.Controllers;
using Evil.Web.Models;
using MbUnit.Framework;
using Rhino.Mocks;
using MvcContrib.TestHelper;

// ReSharper disable PossibleNullReferenceException
// ReSharper disable InconsistentNaming

namespace Evil.Web.UnitTests.Controllers.Lairs
{
    [TestFixture]
    [Ignore("need to implement this.")]
    public class when_starting_a_bar
    {
        private LairController _controller;
        private IRepository<Lair> _baseRepository;
        private Lair _newLair;
        private const int _lairId = 1;

        [SetUp]
        public void SetUp()
        {
            _newLair = CreateNewBar();
            _baseRepository = new InMemoryRepository<Lair>(_newLair);
            _controller = new LairController(Mapper.Engine, _baseRepository);
        }

        private static Lair CreateNewBar()
        {
            var lair = new Lair();
            lair.SetProperty(m => m.Id, _lairId);
            return lair;
        }

        private LairModel Act()
        {
            var result = _controller.Details(_lairId).AssertViewRendered();
            Assert.IsInstanceOfType<LairModel>(result.Model);
            var lairModel = result.Model as LairModel;
            Assert.AreEqual(_lairId, lairModel.Id);
            return lairModel;
        }

        [Test]
        public void Level_1_bar_should_have_1_empty_section()
        {
            var lairModel = Act();
            Assert.AreEqual(1, lairModel.EmptySections);
        }


        [Test]
        public void Level_1_bar_with_1_filled_section_should_have_0_available_upgrades()
        {
            _newLair.AddSection(new FakeSection());
            var lairModel = Act();
            Assert.AreEqual(0, lairModel.EmptySections);
        }

        [Test]
        public void Level_1_bar_should_be_upgradeable()
        {
            var lairModel = Act();
            Assert.IsTrue(lairModel.CanUpgrade);
        }

        [Test]
      //  [Row()]
        public void Level_1_bar_allowed_sections()
        {
            var lairModel = Act();
            Assert.Contains(lairModel.AllowedSections, SectionType.Game);
        }

        [Test]
        public void Level_1_bar_can_add_security_section()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void Bar_being_upgraded_should_not_be_upgradeable()
        {
            _newLair.BeginUpgrade();
            var lairModel = Act();
            Assert.IsFalse(lairModel.CanUpgrade);
        }

        [Test]
        public void Bar_being_upgraded_should_report_time_till_upgraded()
        {
            _newLair.BeginUpgrade();
            var lairModel = Act();
            Assert.GreaterThan(lairModel.UpgradeFinished, DateTime.Now);
        }

        //[Test]
        //public void should_be_able_to_assign_bar_tender()
        //{
        //    throw new NotImplementedException();
        //}
        
        //[Test]
        //public void should_be_able_to_assign_waitress()
        //{
        //    throw new NotImplementedException();            
        //}

        //[Test]
        //public void should_list_available_jobs()
        //{
        //    var bar = Act();
        //    var barSection = GetBarSection(bar);
        //    Assert.That(barSection.Jobs.Count(), Is.EqualTo(2));
        //    foreach (var job in barSection.Jobs)
        //    {
        //        Assert.That(job.IsVacant, Is.True);
        //    }
        //}

        //[Test]
        //public void should_be_no_agents_assigned()
        //{
        //    var bar = Act();
        //    var barSection = GetBarSection(bar);
        //    Assert.That(barSection.Jobs.Count(), Is.EqualTo(2));
        //    foreach (var job in barSection.Jobs)
        //    {
        //        Assert.That(job.AssignedAgent, Is.Null);
        //    }
        //}
    }

    public class FakeSection : Section
    {
    }

    //public static class SectionExtensions
    //{
    //    public static T GetSection<T>(this IEnumerable<Section> section) where T : Section
    //    {
    //        return section.OfType<T>().First();
    //    }
    //}
}

// ReSharper restore PossibleNullReferenceException
// ReSharper restore InconsistentNaming