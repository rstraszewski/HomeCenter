using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCenter.DeviceDomain.Service.ServiceLayer
{
    public interface ICreateControlUiService
    {
        IEnumerable<DeviceDtoOutput> GetDevicesForUiCreating();
        IEnumerable<Building> GetAllBuildings();
    }
}
