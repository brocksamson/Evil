using System.Web.Mvc;
using AutoMapper;
using Evil.Bases;
using Evil.Common;
using Evil.Web.Models;

namespace Evil.Web.Controllers
{
    public class LairController : Controller
    {
        private readonly IMappingEngine _mapper;
        private readonly IRepository<Base> _baseRepository;

        public LairController(IMappingEngine mapper, IRepository<Base> baseRepository)
        {
            _mapper = mapper;
            _baseRepository = baseRepository;
        }

        public ActionResult Details(int id)
        {
            var lair = _baseRepository.GetById(id);
            var baseView = _mapper.Map<Base, BaseView>(lair);
            return View(baseView);
        }

    }
}