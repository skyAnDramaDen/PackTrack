using AbcParcel.Data;
using AbcParcel.Models;
using AbcParcel.Services.UserServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;

namespace AbcParcel.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userServices;

        // Constructor to inject IUserService dependency.
        public AccountController(IUserService userServices)
        {
            _userServices = userServices;
        }

        public IActionResult Index()
        {
            return View();
        }
        //  method to display the create admin view.
        public IActionResult CreateAdmin()
        {
            return View();
        }

        // method to handle the creation of admin users.
        public async Task<IActionResult> CreateAdminUser(RegisterAdminViewModel model)
        {
            // register admin user using user services.
            var entity = await _userServices.RegisterAdmin(model);

            // redirect to the parcel index if registration is successful.
            if (entity.Successful)
            {
                return RedirectToAction("Index", "Parcel");
            }

            // if not, return the view.
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        // action to handle user login

        [HttpPost]
        public async Task<IActionResult> LoginUser(LoginViewModel model)
        {
            //validation based on model state
            if (ModelState.IsValid)
            {
                var result = await _userServices.Login(model.UserName, model.Password, model.RememberMe);
                var entity = await _userServices.GetUserByUserName(model.UserName);
                //redirection based on which user it is. Admin to parcels and User to track parcels
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
        //display registration vies
        public IActionResult Register()
        {
            return View();
        }

        // Register user using user services.
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

                // redirect to parcel view if registration is successful.
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
