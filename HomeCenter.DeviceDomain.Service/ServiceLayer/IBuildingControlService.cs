using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HomeCenter.DeviceDomain
{
    public interface IBuildingControlService
    {
        IEnumerable<BuildingDtoOutput> GetBuildingsForSummary();
        IEnumerable<BuildingDtoOutput> GetBuildingsDevicesForUiCreating();
        IEnumerable<BuildingDtoOutput> GetAllBuildings();
        void AddBuilding(BuildingDtoInput building, string username);
        void AddRoom(RoomDtoInput room, long floorId, long buildingId, string username);
        void AddFloor(FloorDtoInput floor, long buildingId, string username);
        void DestroyBuilding(long buildingId, string username);
        void RemoveRoom(long roomId, long floorId, long buildingId, string username);
        void RemoveFloor(long floorId, long buildingId, string username);
        IEnumerable<BuildingDtoOutput> GetAllBuildingsForUser(string getUsernameFromCookie);
    }

}
