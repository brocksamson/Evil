using System.Web.Mvc;
using Evil.Common;
using Evil.Users;
using Evil.Web.ActionFilters;
using Evil.Web.Services;

namespace Evil.Web.Controllers
{
    [Authorize, InGame]
    public class MissionController : Controller
    {
        private readonly IMapGenerator _mapGenerator;
        private readonly IRepository<Target> _targetRepository;

        public MissionController(IMapGenerator mapGenerator, IRepository<Target> targetRepository)
        {
            _mapGenerator = mapGenerator;
            _targetRepository = targetRepository;
        }

        [HttpGet]
        public ActionResult Index(Player currentPlayer, MissionTypes missionType)
        {
            //TODO: make sure player is valid for missionType
            return View(missionType);
        }

        [HttpGet]
        public ActionResult GetMissions(Player currentPlayer, MissionTypes missionType)
        {
            var missions = _targetRepository.Get.ValidTargetsFor(currentPlayer);
            var map = _mapGenerator.GenerateTargetMap(missions);
            var json = Json(map);
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return json;
        }

        [HttpGet]
        public ActionResult Details()
        {
            return PartialView();
        }
    }

    public enum MissionTypes
    {
        All,
        Robberies
    }
}
