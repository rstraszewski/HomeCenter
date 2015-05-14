using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AutoMapper;
using HomeCenter.AuthenticationDomain;
using HomeCenter.DeviceDomain;
using HomeCenter.HistoryDomain;

namespace HomeCenter.DeviceDomain
{
    public class BuildingControlService : IBuildingControlService
    {
        private readonly IBuildingRepository _buildingRepository;
        private readonly IDeviceRepository _deviceRepository;
        private readonly IDomainLoggerService _domainLoggerService;
        private readonly IAuthenticationService _authenticationService;
        public BuildingControlService(IBuildingRepository buildingRepository, IDeviceRepository deviceRepository, IDomainLoggerService domainLoggerService, IAuthenticationService authenticationService)
        {
            _buildingRepository = buildingRepository;
            _deviceRepository = deviceRepository;
            _domainLoggerService = domainLoggerService;
            _authenticationService = authenticationService;
        }

        public IEnumerable<BuildingDtoOutput> GetBuildingsForSummary()
        {
            var buildings = _buildingRepository.GetAllBuildings();
            var buildingsDto = Mapper.Map<IEnumerable<BuildingDtoOutput>>(buildings);

            return buildingsDto;
        }

        public IEnumerable<BuildingDtoOutput> GetBuildingsDevicesForUiCreating()
        {
            var buildingsWithVirtualDevices = _buildingRepository.GetAllBuildings();

            var output = Mapper.Map<IEnumerable<BuildingDtoOutput>>(buildingsWithVirtualDevices);
            return output;
        }

        public IEnumerable<BuildingDtoOutput> GetAllBuildings()
        {
            var buildings = _buildingRepository.GetAllBuildings();
            var buildingsDto = Mapper.Map<IEnumerable<BuildingDtoOutput>>(buildings);
            return buildingsDto;
        }

        public IEnumerable<BuildingDtoOutput> GetAllBuildingsForUser(string username)
        {
            var buildings = _buildingRepository.GetAllBuildings();
            var buildingsDto = Mapper.Map<IEnumerable<BuildingDtoOutput>>(buildings);
            var roles =_authenticationService.GetRolesForUser(username);
            var isUserRole = roles.Contains("User") && !roles.Contains("Administrator");
            if (isUserRole)
            {
                var roomAccess = _authenticationService.GetRoomAccessForUser(username).Split(';').ToList();
                foreach (var building in buildingsDto)
                {
                    foreach (var floor in building.Floors)
                    {
                        ((List<RoomDtoOutput>) floor.Rooms).RemoveAll(room => !roomAccess.Contains(room.Id.ToString()));
                    }
                }
            }

            return buildingsDto;
        }


        public void AddBuilding(BuildingDtoInput building, string username)
        {
            try
            {
                var entity = Mapper.Map<Building>(building);
                _buildingRepository.AddBuilding(entity);
                _buildingRepository.Save();
                _domainLoggerService.Publish(username, string.Format("Building with name \"{0}\" has been created", building.Name));
            }
            catch (Exception)
            {
                throw;
            }

        }

        public void AddFloor(FloorDtoInput floor, long buildingId, string username)
        {
            try
            {
                var entity = Mapper.Map<Floor>(floor);
                _buildingRepository.AddFloor(entity, buildingId);
                _buildingRepository.Save();
                _domainLoggerService.Publish(username, string.Format("Floor with name \"{0}\" has been created in building with id {1}", floor.Name, buildingId));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DestroyBuilding(long buildingId, string username)
        {
            try
            {
                _buildingRepository.RemoveBuilding(buildingId);
                _buildingRepository.Save();

                _domainLoggerService.Publish(username, string.Format("Building with id {0} has been removed", buildingId));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void RemoveRoom(long roomId, long floorId, long buildingId, string username)
        {
            _buildingRepository.RemoveRoom(roomId, floorId, buildingId);
            _buildingRepository.Save();
        }

        public void RemoveFloor(long floorId, long buildingId, string username)
        {
            try
            {
                _buildingRepository.RemoveFloor(floorId, buildingId);
                _buildingRepository.Save();
                _domainLoggerService.Publish(username, string.Format("Floor with id {0} has been removed from building with id {1}", floorId, buildingId));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AddRoom(RoomDtoInput room, long floorId, long buildingId, string username)
        {
            try
            {
                var entity = Mapper.Map<Room>(room);
                _buildingRepository.AddRoom(entity, floorId, buildingId);
                _buildingRepository.Save();
                _domainLoggerService.Publish(username, string.Format("Room with name \"{0}\" has been removed from floor with id {1} from building with id {2}", room.Name, floorId, buildingId));
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
