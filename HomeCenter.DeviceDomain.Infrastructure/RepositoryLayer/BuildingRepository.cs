using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using HomeCenter.CommonDomain;
using System.Data.Entity.Infrastructure;

namespace HomeCenter.DeviceDomain
{
    public class BuildingRepository : GenericRepository<Building>, IBuildingRepository
    {
        public BuildingRepository(IDbContext contextDb)
            : base(contextDb)
        {
        }

        //Remove methods
        public void RemoveBuilding(long buildingId)
        {
            var entity = _set.Find(buildingId);
            _set.Remove(entity);
        }

        public void RemoveFloor(long floorId, long buildingId)
        {
            var entityBuilding = _set.Find(buildingId);
            var entityFloor = entityBuilding.Floors.Find((floor) => floor.Id == floorId);
            entityBuilding.Floors.Remove(entityFloor);
            _contextDb.Entry(entityFloor).State = EntityState.Deleted;
        }

        public void RemoveRoom(long roomId, long floorId, long buildingId)
        {
            var entityBuilding = _set.Find(buildingId);
            var entityFloor = entityBuilding.Floors.Find((floor) => floor.Id == floorId);
            var entityRoom = entityFloor.Rooms.Find((room) => room.Id == roomId);
            entityFloor.Rooms.Remove(entityRoom);
            _contextDb.Entry(entityRoom).State = EntityState.Deleted;
        }

        //Edit methods
        public void EditBuilding(Building entity)
        {
            _set.Attach(entity);
            _contextDb.Entry(entity).State = EntityState.Modified;
        }

        //Add methods
        public void AddFloor(Floor floor, long buildingId)
        {
            _set.Find(buildingId).Floors.Add(floor);
        }

        public void AddRoom(Room room, long floorId, long buildingId)
        {
            _set.Find(buildingId).Floors.Find(floor => floor.Id == floorId).Rooms.Add(room);

        }

        public void AddBuilding(Building entity)
        {
            _set.Add(entity);
        }

        //Get methods
        public IEnumerable<Building> GetAllBuildings()
        {
            return _set;
        }

        public Room GetRoom(long roomId, long floorId, long buildingId)
        {
            return _set.Find(buildingId).Floors.Find(floor => floor.Id == floorId).Rooms.Find(room => room.Id == roomId);
        }
    }
}
