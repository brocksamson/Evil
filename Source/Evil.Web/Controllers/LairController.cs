using System.Web.Mvc;
using AutoMapper;
using Evil.Common;
using Evil.Lairs;
using Evil.Web.Models;

namespace Evil.Web.Controllers
{
    public class LairController : Controller
    {
        private readonly IMappingEngine _mapper;
        private readonly IRepository<Lair> _baseRepository;

        public LairController(IMappingEngine mapper, IRepository<Lair> baseRepository)
        {
            _mapper = mapper;
            _baseRepository = baseRepository;
        }

        public ActionResult Details(int id)
        {
            var lair = _baseRepository.GetById(id);
            var baseView = _mapper.Map<Lair, LairModel>(lair);
            return View(baseView);
        }

    }
}