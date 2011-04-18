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
            var myBase = new Lair();
            myBase.SetProperty(m => m.Id, _lairId);
            return myBase;
        }

        [Test]
        public void Level_1_bar_should_have_1_available_upgrade()
        {
            var result = _controller.Details(_lairId).AssertViewRendered();
            Assert.IsInstanceOfType<LairModel>(result.Model);
            var lairView = result.Model as LairModel;
            Assert.AreEqual(_lairId, lairView.Id);
            Assert.AreEqual(1, lairView.AvailableUpgrades);
        }

        [Test]
        public void Level_1_bar_with_one_upgrade_should_have_0_empty_sections()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void Level_1_bar_should_be_upgradeable()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void Level_1_bar_being_upgraded_should_not_be_upgradeable()
        {
            throw new NotImplementedException();
        }


        //[Test]
        //public void should_get_list_of_empty_sections()
        //{
        //    var bar = Act();
        //    //Assert.That(bar.Sections.Where(section => section.Type == SectionType.Empty).Count(), Is.EqualTo(3), "bar should start with 3 empty sections");
        //    Assert.AreEqual(bar.Id, _lairId);
        //    //foreach (var section in _newBar.Sections)
        //    //{
        //    //    Assert.That(bar.Sections.Any(m => m.Id == section.Id), "Missing section");
        //    //}
        //}

        //private BaseView Act()
        //{
        //    var result = _controller.Details(_lairId);
        //    var view = result as ViewResult;
        //    Assert.IsInstanceOfType<ViewResult>(result);
        //    Assert.IsInstanceOfType<BaseView>(view.ViewData.Model);
        //    return view.ViewData.Model as BaseView;
        //}

        //[Test]
        //public void should_list_upgrades_correctly()
        //{
        //    var bar = Act();
        //    var actualBarSection = GetBarSection(bar);
        //    Assert.That(actualBarSection.CanUpgrade, Is.True);
        //    foreach(var section in bar.Sections.Where(m => m.Type == SectionType.Empty))
        //    {
        //        Assert.That(section.CanUpgrade, Is.False);
        //    }
            
        //}

        //private SectionView GetBarSection(BaseView bar)
        //{
        //    var expectedBarSection = _newBar.Sections.GetSection<BarSection>();
        //    return bar.Sections.First(m => m.Id == expectedBarSection.Id);
        //}

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