using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using HomeCenter.DeviceDomain;
using User = HomeCenter.AuthenticationDomain.User;

namespace HomeCenter.App.Controllers
{
    [Authorize(Roles = "Builder,Administrator")]
    public class AddDeviceController : Controller
    {
        private readonly IDeviceService _deviceService;

        public AddDeviceController(IDeviceService deviceService)
        {
            this._deviceService = deviceService;
        }

        //
        // GET: /CreateDevice/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateDevice(DeviceDtoInput device, GroupDtoInput group = null)
        {
            _deviceService.CreateDevice(device, GetUsernameFromCookie(), group);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
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

        public ActionResult ChangeGroup(long deviceId, long groupId)
        {
            _deviceService.ChangeDeviceGroup(deviceId, groupId);
            return Json(null);
        }

        public ActionResult GetAddresses()
        {
            return Json(_deviceService.GetAvailableAddressesForUi(),JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveDevice(long deviceId)
        {
            _deviceService.RemoveDevice(deviceId);
            return Json(null);
        }
        [Authorize(Roles = "Administrator, Builder, User")]
        public ActionResult GetDevices()
        {
            var device = _deviceService.GetAllDevices();
            return Json(device, JsonRequestBehavior.AllowGet);
        }
    }
}
