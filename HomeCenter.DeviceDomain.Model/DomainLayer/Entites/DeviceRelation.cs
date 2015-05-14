using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeCenter.CommonDomain;

namespace HomeCenter.DeviceDomain
{
    public class DeviceRelation : IAggregateRoot
    {
        public long Id { get; set; }
        public long GroupActionId { get; set; }
        public long GroupReactionId { get; set; }
        public long IfStateValue { get; set; }
        public long ThenStateValue { get; set; }
        public StateRelation StateRelation { get; set; }


        public bool CheckRelation(long state)
        {
            switch (StateRelation)
            {
                case StateRelation.GreaterThen:
                {

                    return state > IfStateValue;
                }
                case StateRelation.GreaterThenOrEqualTo:
                {
                    return state >= IfStateValue;
                }
                case StateRelation.LesserThen:
                {
                    return state < IfStateValue;
                    
                }
                case StateRelation.LesserThenOrEqualto:
                {
                    return state <= IfStateValue;
                    
                }
                case StateRelation.EqualTo:
                {
                    return state == IfStateValue;
                    
                }
                default:
                    return false;
            }
        }
    }
}
