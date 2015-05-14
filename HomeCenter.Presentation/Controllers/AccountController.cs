using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper.Internal;
using HomeCenter.AuthenticationDomain;
using HomeCenter.HistoryDomain;
using HomeCenter.Models;
using Microsoft.Ajax.Utilities;
using TypeLite;

namespace HomeCenter.Controllers
{
    [Authorize(Roles = "Administrator, AccountManager")]
    public class AccountController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IDomainLoggerService _domainLoggerService;
        public AccountController(IAuthenticationService authenticationService, 
            IDomainLoggerService domainLoggerService)
        {
            _authenticationService = authenticationService;
            _domainLoggerService = domainLoggerService;
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserDtoInput model, string returnUrl)
        {
            var created = _authenticationService.CreateUser(model, GetUsernameFromCookie());
            if(created)
                return RedirectToAction("Index", "Home");
            ViewBag.Header = "There was problem at account creating. Try again.";
            return View("Create");
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            ViewBag.Header = "Please, log in to you account.";
            return View();
        }

        public ActionResult Logout()
        {
            _domainLoggerService.Publish(GetUsernameFromCookie(), "User has logged out");
            FormsAuthentication.SignOut();
            
            return RedirectToAction("Index", "Home");
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(UserDtoInput model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Header = "Infromations you provided were incorrect.";
                return View("Login");
            }

            var userValid = _authenticationService.IsUserValid(model);

            if (!userValid)
            {
                ViewBag.Header = "Infromations you provided were incorrect.";
                return View("Login");
            }
            _domainLoggerService.Publish(model.Username, "User has logged in");
            FormsAuthentication.SetAuthCookie(model.Username, false);
            return RedirectToAction("Index", "Home");
        }
        private static Regex sUserNameAllowedRegEx = new Regex(@"^[a-zA-Z][a-zA-Z0-9\._\-]{0,22}?[a-zA-Z0-9]{0,2}$", RegexOptions.Compiled);
    
        public ActionResult IsUsernameValid(string username)
        {
            var model = new UsernameValidationModel();

            if (string.IsNullOrEmpty(username))
            {
                model.IsCorrect = false;
                model.Message = "Username can't be blank.";
            }
            else if (_authenticationService.CheckIfAlreadyExist(username))
            {
                model.IsCorrect = false;
                model.Message = "Username already exists";
            }
            else if (!sUserNameAllowedRegEx.IsMatch(username))
            {
                model.IsCorrect = false;
                model.Message = "Username contains wrong elements or is to long!";
            }
            else
            {
                model.IsCorrect = true;
            }

            return Json(model);
        }

        public ActionResult GetRoles()
        {
            return Json(_authenticationService.GetRoles(),JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetRolesForUser(string username)
        {
            var roles = _authenticationService.GetRolesForUser(username).Split(';');
            return Json(roles);
        }

        public ActionResult GetRoomAccessForUser(string username)
        {
            var roles = _authenticationService.GetRoomAccessForUser(username).Split(';');
            return Json(roles);
        }


        public ActionResult GetAllUsernames()
        {  
            return Json(_authenticationService.GetAllUsernames(),JsonRequestBehavior.AllowGet);
        }

        public ActionResult ChangeRoles(string username, string[] roles, string[] roomAccess)
        {
            var rolesString = string.Join(";", roles);
            var roomAccessString = string.Join(";", roomAccess);
            _authenticationService.ChangeRoles(username, rolesString, GetUsernameFromCookie(), roomAccessString);
            return Json(null);
        }

        private string GetUsernameFromCookie()
        {
            if (FormsAuthentication.CookiesSupported)
                if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                {
                    return FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
                }
            return null;
        }

    }
}
