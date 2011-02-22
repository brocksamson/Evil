using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Evil.Bases;
using Evil.Common;
using Evil.Tests.Extensions;
using Evil.Web.Controllers;
using Evil.Web.Models;
using MbUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Constraints;

// ReSharper disable PossibleNullReferenceException
// ReSharper disable InconsistentNaming

namespace Evil.Web.UnitTests.Controllers.Lairs
{
    [TestFixture]
    public class when_starting_a_bar
    {
        private LairController _controller;
        private IRepository<Base> _baseRepository;
        private Base _newBar;
        private const int _barSectionId = 10;
        private const int _lairId = 1;

        //[SetUp]
        //public void SetUp()
        //{
        //    _newBar = CreateBar();
        //    _baseRepository = MockRepository.GenerateMock<IRepository<Base>>();
        //    _baseRepository.Stub(m => m.GetById(_lairId)).Return(_newBar);
        //    _controller = new LairController(Mapper.Engine, _baseRepository);
        //}

        //private static Base CreateBar()
        //{
        //    var myBase = new Base();
        //    myBase.SetProperty(m => m.Id, _lairId);
        //    return myBase;
        //}


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