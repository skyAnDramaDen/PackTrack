using AbcParcel.Data;
using AbcParcel.Models;
using AbcParcel.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace AbcParcel.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userServices;
        public AccountController(IUserService userServices)
        {
            _userServices = userServices;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreateAdmin()
        {
            return View();
        }
        public async Task<IActionResult> CreateAdminUser(RegisterAdminViewModel model)
        {
            var entity = await _userServices.RegisterAdmin(model);
            if (entity.Successful)
            {
                return RedirectToAction("Index", "Parcel");
            }
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginUser(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userServices.Login(model.UserName, model.Password, model.RememberMe);
                var entity = await _userServices.GetUserByUserName(model.UserName);
                if (result.Succeeded && entity.Result.UserType == UserType.Admin)
                {
                    return RedirectToAction("Index", "Parcel");
                }

                if (result.Succeeded && entity.Result.UserType == UserType.Customer)
                {
                    return RedirectToAction("ViewParcel", "Parcel");
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt");
            }

            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        public async Task<IActionResult> RegisterUser(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new Applicationuser
                {
                    UserName = model.UserName,
                };

                var result = await _userServices.RegisterUser(user, model.Password);

                var entity = await _userServices.GetUserByUserName(model.UserName);

                if (result.Succeeded && entity.Successful)
                {
                    await _userServices.Login(model.UserName, model.Password, rememberMe: true);
                    return RedirectToAction("ViewParcel", "Parcel");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }
        public IActionResult LogoutUser()
        {
            _userServices.Logout().Wait();
            return RedirectToAction("Index", "Home");
        }


    }
}
