using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Web.Mvc;
using Evil.Bases;
using Evil.Common;
using Evil.Users;
using Evil.Web.Models;
using Evil.Web.Services;

namespace Evil.Web.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
        private readonly IRepository<Player> _playerRepository;
        private readonly IRepository<Area> _areaRepository;
        private readonly IMapGenerator _mapGenerator;

        public GameController(IRepository<Player> playerRepository, IRepository<Area> areaRepository, IMapGenerator mapGenerator)
        {
            _playerRepository = playerRepository;
            _mapGenerator = mapGenerator;
            _areaRepository = areaRepository;
        }

        public ActionResult Start()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Start(Account currentAccount, CreatePlayerView model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var latitude = model.BaseLatitude;
            var longitude = model.BaseLongitude;
            var player = new Player
                             {
                                 Account = currentAccount,
                                 Name = model.Name,
                                 MainBase =
                                     CreateFirstBase(model, latitude, longitude)
                             };
            _playerRepository.Save(player);
            return RedirectToAction("Index");
            
        }

        private Base CreateFirstBase(CreatePlayerView model, double latitude, double longitude)
        {
            return new Base
                       {
                           Position = new Position(latitude, longitude),
                           Name = model.BaseName
                       };
        }

        [HttpGet]
        public ActionResult StartupLocations()
        {
            var startingCity = GetStartingCity();
            var map = _mapGenerator.CreateStartingMap(startingCity);
            var json = Json(map);
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return json;
        }

        private Area GetStartingCity()
        {
            return new Area
            {
                Name = "Manhatten",
                UpperLeft = new Position(40.771312, -73.993821),
                LowerRight = new Position(40.739909, -73.973351)
            };
        }

        public ActionResult Index()
        {
            return View();
        }
    }

}