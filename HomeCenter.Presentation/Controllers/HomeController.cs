using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using HomeCenter.DeviceDomain;
using HomeCenter.HistoryDomain;

namespace HomeCenter.Controllers
{

    public class HomeController : Controller
    {
        private readonly IDeviceService _deviceService;
        private readonly IDeviceTypeControlService _deviceTypeControlService;
        private readonly IBuildingControlService _buildingControlService;
        private readonly IDomainLoggerService _domainLoggerService;

        public HomeController(IDeviceService deviceService, 
            IDeviceTypeControlService deviceTypeControlService, 
            IBuildingControlService buildingControlService, 
            IDomainLoggerService domainLoggerService)
        {
            _deviceService = deviceService;
            _deviceTypeControlService = deviceTypeControlService;
            _buildingControlService = buildingControlService;
            _domainLoggerService = domainLoggerService;
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult GetLogsForPage(int page, int count, string username, long? fromDateTime, long? toDateTime)
        {
            var logs = _domainLoggerService.GetLogs(page, count, username, fromDateTime, toDateTime);
            return Json(logs);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult GetLogsLength(string username, long? fromDateTime, long? toDateTime)
        {
            return Json(_domainLoggerService.GetLogsCountForUser(username, fromDateTime, toDateTime), JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public ActionResult angularTemplate()
        {
            return PartialView("angular-notify.html");
        }
    }
}