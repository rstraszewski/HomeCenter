using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeCenter;
using HomeCenter.CommonDomain;
using HomeCenter.DeviceDomain;

namespace HomeCenter.DeviceDomain
{
    public interface IBuildingRepository : IGenericRepository<Device>
    {
    //    void Create(Device device);
    //    void Update(Device device);
    //    void Delete(Device device);
    //    void Read(long id);
        //IList<Device> GetAll();

        //IList<Device> Get();
        //void RollbackChanges();
        void EditBuilding(Building entity);
        void RemoveBuilding(long buildingId);
        void RemoveFloor(long floorId, long buildingId);
        void RemoveRoom(long roomId,  long floorId, long buildingId);
        void AddFloor(Floor floor, long buildingId);
        void AddRoom(Room room, long floorId, long buildingId);
        void AddBuilding(Building entity);
        IEnumerable<Building> GetAllBuildings();

        Room GetRoom(long roomId, long floorId, long buildingId);
    }

}
