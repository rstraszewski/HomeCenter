using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using HomeCenter.DeviceDomain;
using HomeCenter.HistoryDomain;

namespace HomeCenter.Controllers
{
    [Authorize(Roles = "Builder,Administrator")]
    public class CreateBuildingController : Controller
    {
        //
        // GET: /Create/
        
        private readonly IBuildingControlService _buildingControlService;
        private readonly IDomainLoggerService _domainLoggerService;

        public CreateBuildingController(IBuildingControlService buildingControlService, IDomainLoggerService domainLoggerService)
        {
            _buildingControlService = buildingControlService;
            _domainLoggerService = domainLoggerService;
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

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateBuilding(BuildingDtoInput building)
        {
            _buildingControlService.AddBuilding(building, GetUsernameFromCookie());
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
        public ActionResult CreateBuildings(List<BuildingDtoInput> buildings)
        {
            foreach (var building in buildings)
                _buildingControlService.AddBuilding(building, GetUsernameFromCookie());
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
        public ActionResult RemoveBuilding(long buildingId)
        {
            _buildingControlService.DestroyBuilding(buildingId, GetUsernameFromCookie());
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public ActionResult RemoveFloor(long floorId, long buildingId)
        {
            _buildingControlService.RemoveFloor(floorId, buildingId, GetUsernameFromCookie());
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public ActionResult RemoveRoom(long roomId, long floorId, long buildingId)
        {
            _buildingControlService.RemoveRoom(roomId, floorId, buildingId, GetUsernameFromCookie());
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
        public ActionResult CreateFloor(FloorDtoInput floor, long buildingId)
        {
            _buildingControlService.AddFloor(floor, buildingId, GetUsernameFromCookie());
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public ActionResult CreateFloors(List<FloorDtoInput> floors, long buildingId)
        {
            foreach(var floor in floors)
                _buildingControlService.AddFloor(floor, buildingId, GetUsernameFromCookie());
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
        public ActionResult CreateRoom(RoomDtoInput room, long floorId, long buildingId)
        {
            _buildingControlService.AddRoom(room, floorId, buildingId, GetUsernameFromCookie());
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
        public ActionResult CreateRooms(List<RoomDtoInput> rooms, long floorId, long buildingId)
        {
            foreach(var room in rooms)
                _buildingControlService.AddRoom(room, floorId, buildingId, GetUsernameFromCookie());
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

    }
}
