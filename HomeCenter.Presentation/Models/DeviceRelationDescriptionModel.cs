using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TypeLite;

namespace HomeCenter.Models
{
    [TsClass(Name = "IDeviceRelationDescriptionModel", Module = "HomeCenter")]
    public class DeviceRelationDescriptionModel
    {
        public long Id { get; set; }
        public string Description { get; set; }
    }
}