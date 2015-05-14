using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using HomeCenter.DeviceDomain;
using HomeCenter.DeviceDomain.Service.ServiceLayer;

namespace HomeCenter.Controllers
{
    [Authorize(Roles = "User,Administrator")]
    public class DeviceControlController : Controller
    {
        private readonly IBuildingControlService _buildingControlService;
        private readonly ICreateControlUiService _buildingControlUiService;
        private readonly IDeviceService _deviceService;
        public DeviceControlController(IBuildingControlService buildingControlService, 
            ICreateControlUiService buildingControlUiService, 
            IDeviceService deviceService)
        {
            _buildingControlService = buildingControlService;
            _buildingControlUiService = buildingControlUiService;
            _deviceService = deviceService;
        }

        public ActionResult Index()
        {
            return View();
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

        public ActionResult GetBuildingsControlDevices()
        {
            var buildingsforUi = _buildingControlService.GetBuildingsDevicesForUiCreating();
            return Json(buildingsforUi, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetDevicesForUi()
        {
            var devicesforUi = _buildingControlUiService.GetDevicesForUiCreating();

            return Json(devicesforUi, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Administrator, Builder, User")]
        public ActionResult GetBuildings()
        {
            var buildings = _buildingControlService.GetAllBuildings();
            return Json(buildings, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Administrator, Builder, User")]
        public ActionResult GetBuildingsForUser()
        {
            var buildings = _buildingControlService.GetAllBuildingsForUser(GetUsernameFromCookie());
            return Json(buildings, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateDeviceStateFromUi(long deviceId, long deviceState)
        {
            _deviceService.UpdateDeviceStateFromUi(deviceId, deviceState, GetUsernameFromCookie());
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [Authorize(Roles = "Administrator, Builder, User")]
        public ActionResult GetDeviceTypes()
        {
            var deviceTypes = _deviceService.GetDeviceTypes();
            return Json(deviceTypes, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetGroups()
        {
            var groups = _deviceService.GetGroups();
            return Json(groups, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult 

    }
}
