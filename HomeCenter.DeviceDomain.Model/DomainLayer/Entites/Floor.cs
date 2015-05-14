using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCenter.DeviceDomain
{
    public class Floor
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public virtual List<Room> Rooms { get; set; }
        public long BuildingId { get; set; }
        protected virtual Building Building { get; set; }
    }
}
