using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HomeCenter.DeviceDomain;
using TypeLite;

namespace HomeCenter.Models
{
    [TsClass(Name = "IStateRelationModel", Module = "HomeCenter")]
    public class StateRelationModel
    {
        public StateRelation StateRelation { get; set; }
        public string StateRelationDescription { get; set; }
    }
}