using System.Web.Mvc;
using Evil.Common;
using Evil.Users;
using Evil.Web.Extensions;
using Evil.Web.Models;
using Evil.Web.Services;
using MvcContrib;

namespace Evil.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IFormsService _formsService;
        private readonly IRepository<Account> _accountRepository;
        private readonly IRepository<Player> _playerRepository;

        public AccountController(IRepository<Account> accountRepository, IRepository<Player> playerRepository, IFormsService formsService)
        {
            _accountRepository = accountRepository;
            _playerRepository = playerRepository;
            _formsService = formsService;
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(AccountCreationModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var account = _accountRepository.Get.ByEmailAddress(model.EmailAddress);
            if (account != null && account.EmailAddress == model.EmailAddress)
            {
                //add a better message here...
                ModelState.AddModelErrorFor<AccountCreationModel, string>(m => m.EmailAddress,
                                                                      "Email address already used, please specify another one.");
                return View(model);
            }
            var player = new Account
                             {
                                 FirstName = model.FirstName,
                                 LastName = model.LastName,
                                 Password = model.Password,
                                 EmailAddress = model.EmailAddress
                             };
            _accountRepository.Save(player);
            //TODO: Enable email activation
            //return this.RedirectToAction(c => c.Created());
            return RedirectToAction("Start", "Game");
        }

        public ActionResult Created()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginView login)
        {
            if (!ModelState.IsValid)
                return View(login);
            Account account = _accountRepository.Get.ByEmailAddress(login.EmailAddress);
            if (account == null)
            {
                ModelState.AddModelErrorFor<LoginView, string>(m => m.EmailAddress, "Unknown Email Address");
                return View(login);
            }
            if (account.Password != login.Password)
            {
                ModelState.AddModelErrorFor<LoginView, string>(m => m.Password, "Incorrect Password.");
                return View(login);
            }

            _formsService.SignIn(login.EmailAddress, login.RememberMe);
            var player = _playerRepository.Get.CurrentPlayerFor(account);
            if (player == null || player.Account == null || player.Account != account)
                return RedirectToAction("Start", "Game");
            return RedirectToAction("Index", "Game");
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Logoff()
        {
            _formsService.SignOut();
            return this.RedirectToAction<HomeController>(m => m.Index());
        }
    }
}