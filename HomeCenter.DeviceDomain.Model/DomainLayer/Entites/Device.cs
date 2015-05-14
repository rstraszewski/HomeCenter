using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeCenter.CommonDomain;
using HomeCenter.DeviceDomain;
namespace HomeCenter.DeviceDomain
{
    public class Device : IAggregateRoot
    {
        public long Id { get; set; }
        public long DeviceState { get; private set; }
        public long LastDeviceState { get; private set; }
        [Column]
        [ForeignKey("RoomId")]
        private Room Room { get; set; }
        public long RoomId { get; set; }

        [ForeignKey("DeviceTypeId")]
        public virtual DeviceType DeviceType { get; set; }
        public long DeviceTypeId { get; set; }

        [ForeignKey("GroupId")]
        public virtual Group Group { get; set; }
        public long GroupId { get; set; }

        //public long Address {  }

        public long Address { get; set; }
        public string Name { get; set; }

        public Device(long deviceType, long room, long group, long address, string name)
        {
            DeviceState = 0;
            DeviceTypeId = deviceType;
            RoomId = room;
            GroupId = group;
            LastDeviceState = 0;
            Name = name;
            Address = address;
        }

        protected Device()
        {
            DeviceState = 0;
            LastDeviceState = 0;
        }
        public void SwitchState()
        {
            if (DeviceType.Binary && DeviceType.Changable)
            {
                LastDeviceState = DeviceState;
                DeviceState = DeviceState > DeviceType.Range.MinValue
                    ? DeviceType.Range.MinValue
                    : DeviceType.Range.MaxValue;
            }
        }

        public void SetState(long lvl)
        {
            if (lvl == DeviceState || !DeviceType.Changable)
                return;
            if (lvl > DeviceType.Range.MaxValue)
            {
                LastDeviceState = DeviceState;
                DeviceState = DeviceType.Range.MaxValue;
            }
            else if (lvl < DeviceType.Range.MinValue)
            {
                LastDeviceState = DeviceState;
                DeviceState = DeviceType.Range.MinValue;
            }
            else
            {
                LastDeviceState = DeviceState;
                DeviceState = lvl;
            }
        }

        public void SetGroup(long groupId)
        {
            GroupId = groupId;
        }

        public void AutomaticStateSetting(long lvl)
        {
            if (DeviceType.Changable)
            {
                if (DeviceType.Binary)
                {
                    LastDeviceState = DeviceState;
                    DeviceState = lvl <= DeviceType.Range.MinValue
                        ? DeviceType.Range.MinValue
                        : DeviceType.Range.MaxValue;
                }
                else
                {
                    SetState(lvl);
                }
            }
        }


    }

}
