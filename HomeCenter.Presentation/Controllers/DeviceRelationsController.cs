using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HomeCenter.App_Start;
using HomeCenter.DeviceDomain;
using HomeCenter.Models;
using HomeCenter.Models.Helpers;
using HomeCenter.RepositoryLayer;
using WebGrease.Css.Extensions;

namespace HomeCenter.Controllers
{
    [Authorize(Roles = "Scheduler,Administrator")]
    public class DeviceRelationsController : Controller
    {
        //
        // GET: /DeviceRelation/
        private readonly IDeviceService _deviceService;

        public DeviceRelationsController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateRelation(DeviceRelationDtoInput deviceRelation)
        {
            _deviceService.AddDeviceRelation(deviceRelation);
            return Json(null);
        }

        public ActionResult RemoveRelation(long deviceRelationId)
        {
            _deviceService.RemoveDeviceRelation(deviceRelationId);
            return Json(null);
        }

        public ActionResult GetDeviceRelations()
        {
            var relations = _deviceService.GetAllDeviceRelations();
            var result = relations.Select(r => new DeviceRelationDescriptionModel()
            {
                Description = string.Format("When state in group {0} {1} {2} set state in group {3} to {4}",
                    _deviceService.GetGroup(r.GroupActionId).Name,
                    r.StateRelation.GetDescription(),
                    r.IfStateValue,
                    _deviceService.GetGroup(r.GroupReactionId).Name,
                    r.ThenStateValue),
                Id = r.Id

            });
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetStateRelations()
        {
            var relations = Enum.GetValues(typeof(StateRelation)).Cast<StateRelation>()
                .Select(enumValue => new StateRelationModel
                {
                    StateRelation = enumValue,
                    StateRelationDescription = enumValue.GetDescription()
                });

            return Json(relations, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddDeviceJob(DeviceJobDtoInput deviceJob)
        {
            _deviceService.AddJob(deviceJob);
            return Json(null);
        }

        public ActionResult RemoveDeviceJob(long id)
        {
            _deviceService.RemoveJob(id);
            return Json(null);
        }

        public ActionResult GetAllJobs()
        {
            var jobList = _deviceService.GetAllJobs().Select(job => new DeviceJobModel()
            {
                Id = job.Id,
                Description =
                    string.Format("Set state in group {0} to {1}. ", _deviceService.GetGroup(job.GroupId).Name,
                        job.State)
                    +
                    (job.IsCron
                        ? string.Format("Cron expression: {0}", job.CronExpression)
                        : string.Format("Date: {0} - {1}", job.DateTime.ToLongTimeString(), job.DateTime.ToLongDateString()))
            });
            return Json(jobList, JsonRequestBehavior.AllowGet);
        }
    }
}
