using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml;
using AutoMapper;
using HomeCenter.DeviceDomain;
using HomeCenter.CommonDomain;
using HomeCenter.DeviceDomain.DomainLayer.RepositoryInterface;
using HomeCenter.HistoryDomain;
using HomeCenter.RepositoryLayer;
using MySql.Data.MySqlClient;

namespace HomeCenter.DeviceDomain
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IDeviceTypeRepository _deviceTypeRepository;
        private readonly IBuildingRepository _buildingRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IDomainEvents _domainEvents;
        private readonly IDomainLoggerService _domainLoggerService;
        private readonly IDeviceRelationRepository _deviceRelationRepository;
        private readonly IDeviceJobRepository _deviceJobRepository;
        private readonly IAddressRepository _addressRepository;
        public DeviceService(IDeviceRepository deviceRepository,
            IDeviceTypeRepository deviceTypeRepository,
            IBuildingRepository buildingRepository,
            IGroupRepository groupRepository,
            IDomainEvents domainEvents,
            IDomainLoggerService domainLoggerService,
            IDeviceRelationRepository deviceRelationRepository,
            IDeviceJobRepository deviceJobRepository, IAddressRepository addressRepository)
        {
            _deviceRepository = deviceRepository;
            _deviceTypeRepository = deviceTypeRepository;
            _buildingRepository = buildingRepository;
            _groupRepository = groupRepository;
            _domainEvents = domainEvents;
            _domainLoggerService = domainLoggerService;
            _deviceRelationRepository = deviceRelationRepository;
            _deviceJobRepository = deviceJobRepository;
            _addressRepository = addressRepository;
        }

        public void UpdateDeviceStateFromUi(long deviceId, long deviceState, string username)
        {
            //using (var mysqlconnection = new MySqlConnection(_deviceRepository.getConnectionString()))
            //{
            //    mysqlconnection.Open();
            //    var transaction = mysqlconnection.BeginTransaction();
            try
            {


                var deviceFromRepo = _deviceRepository.GetById(deviceId);
                deviceFromRepo.SetState(deviceState);
                _deviceRepository.Update(deviceFromRepo);

                _domainEvents.Raise(new DeviceStateHasChanged()
                {
                    Device = deviceFromRepo
                });
                _domainEvents.Raise(new DeviceStateHasChangedFromUi() { Device = deviceFromRepo, Username = username });
                _deviceRepository.Save();
            }
            catch (Exception e)
            {
                //Debug.WriteLine(e.InnerException);
                throw;

            }
            //    transaction.Commit();
            //    mysqlconnection.Close();
            //}
        }





        public List<Device> GetAllDevicesWithinGroup(Device device)
        {
            return _deviceRepository.GetAllDevices().Where(d => d.GroupId == device.GroupId && (d.Id != device.Id)).ToList();
        }

        public void SetAllDevicesWithinGroup(Device deviceEntity)
        {
            var devices = _deviceRepository.GetAllDevices().Where(d => d.GroupId == deviceEntity.GroupId && (d.Id != deviceEntity.Id)).ToList();
            if (devices != null && devices.Count != 0)
            {
                foreach (var device in devices)
                {
                    device.AutomaticStateSetting(deviceEntity.DeviceState);
                    _deviceRepository.Update(device);
                    _deviceRepository.Save();
                    _domainEvents.Raise(new DeviceStateHasChanged()
                    {
                        Device = device
                    });
                };
            }

        }

        public void AddDeviceRelation(DeviceRelationDtoInput relation)
        {
            var entity = Mapper.Map<DeviceRelation>(relation);
            _deviceRelationRepository.AddDeviceRelation(entity);
            _deviceRelationRepository.Save();

        }

        public void RemoveDeviceRelation(long deviceRelationId)
        {
            _deviceRelationRepository.RemoveDeviceRelation(deviceRelationId);
            _deviceRelationRepository.Save();
        }

        public IEnumerable<DeviceRelationDto> GetAllDeviceRelations()
        {
            var relationEntites = _deviceRelationRepository.GetAllDeviceRelations().ToList();

            return Mapper.Map<List<DeviceRelationDto>>(relationEntites);
        }

        public void SetDeviceRelationBasedState(long groupId, long deviceState, long lastDeviceState)
        {
            var relations = _deviceRelationRepository.GetAllDeviceRelations().ToList();
            foreach (var relation in relations)
            {
                if (groupId == relation.GroupActionId)
                    if (relation.CheckRelation(deviceState) && !relation.CheckRelation(lastDeviceState))
                    {
                        SetAllDevicesWithinGroup(relation.GroupReactionId, relation.ThenStateValue);

                    }

            }
        }

        public GroupDtoOutput GetGroup(long groupActionId)
        {
            return Mapper.Map<GroupDtoOutput>(_groupRepository.GetGroup(groupActionId));
        }

        public void ChangeDeviceStateWith(Device device)
        {
            SetAllDevicesWithinGroup(device);
            SetDeviceRelationBasedState(device.GroupId, device.DeviceState, device.LastDeviceState);
            //_deviceRepository.Save();
        }



        public void RemoveDevice(long deviceId)
        {

            var device = _deviceRepository.GetById(deviceId);
            if (!device.DeviceType.IsVirtual)
            {
                var address = _addressRepository.GetAddressByNumber(device.Address);
                address.DetachFromDevice();
                _addressRepository.Update(address);
            }

            _deviceRepository.RemoveDevice(deviceId);

            _deviceRepository.Save();

        }

        public IEnumerable<DeviceDtoOutput> GetAllDevices()
        {
            using (var transactionScope = new TransactionScope())
            {
                try
                {
                    var deviceEntities = _deviceRepository.GetAllDevices().ToList();
                    var devices = deviceEntities != null
                        ? Mapper.Map<IEnumerable<DeviceDtoOutput>>(deviceEntities)
                        : new List<DeviceDtoOutput>();

                    transactionScope.Complete();
                    return devices;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                return new List<DeviceDtoOutput>();
            }
        }

        public Device GetDevice(long address)
        {
            using (var ts = new TransactionScope())
            {
                try
                {
                    var entity = _deviceRepository.GetByAddres(address);
                    ts.Complete();
                    return entity;

                }
                catch (Exception)
                {
                    return null;
                }

            }
        }

        public void UpdateDeviceStateFromSerialPort(long address, long state)
        {
            //using (var mysqlconnection = new MySqlConnection(_deviceRepository.getConnectionString()))
            //{
            //    mysqlconnection.Open();
            //    var transaction = mysqlconnection.BeginTransaction();
            try
            {
                Console.WriteLine("Update state from serial port started");

                var deviceFromRepo = _deviceRepository.GetByAddres(address);
                deviceFromRepo.SetState(state);
                _deviceRepository.Update(deviceFromRepo);

                _domainEvents.Raise(new DeviceStateHasChanged()
                {
                    Device = deviceFromRepo,
                    onlyWebsocket = true
                });
                _domainEvents.Raise(new DeviceStateHasChangedFromSerialPort(deviceFromRepo));
                _deviceRepository.Save();
                Console.WriteLine("Update state from serial port ended");

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.InnerException);

            }
            //    transaction.Commit();
            //    mysqlconnection.Close();
            //}
        }

        public DeviceJobDto GetDeviceJob(long id)
        {
            var entity = _deviceJobRepository.GetDeviceJob(id);
            return Mapper.Map<DeviceJobDto>(entity);
        }

        public void AddJob(DeviceJobDtoInput deviceJob)
        {
            //var job = Mapper.Map<DeviceJob>(deviceJob);
            DateTime? date = null;
            string cron = null;
            if (deviceJob.IsCron == false && deviceJob.DateTime != null)
            {
                date = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                    .AddMilliseconds(deviceJob.DateTime.Value)
                    .ToLocalTime();
            }
            else if (deviceJob.IsCron == true && deviceJob.CronExpression != null)
            {
                cron = deviceJob.CronExpression;
            }
            var jobEntity = new DeviceJob()
            {
                CronExpression = cron,
                IsCron = deviceJob.IsCron,
                DateTime = date,
                GroupId = deviceJob.GroupId,
                State = deviceJob.State
            };
            _deviceJobRepository.Add(jobEntity);
            _deviceJobRepository.Save();
            _domainEvents.Raise(new StartDeviceJob() { DeviceJob = jobEntity });
        }

        public void RemoveJob(long id)
        {
            _deviceJobRepository.Remove(id);
            _deviceJobRepository.Save();
            _domainEvents.Raise(new RemoveDeviceJob() { DeviceJobId = id });
        }

        public IEnumerable<DeviceJobDto> GetAllJobs()
        {
            var entities = _deviceJobRepository.GetAll().ToList();
            return Mapper.Map<IEnumerable<DeviceJobDto>>(entities);
        }

        public void ChangeDeviceGroup(long deviceId, long groupId)
        {
            using (var ts = new TransactionScope())
            {
                var entity = _deviceRepository.GetById(deviceId);
                entity.SetGroup(groupId);
                _deviceRepository.Save();
                ts.Complete();
            }
        }

        public long GetAddresForPhisicalDevice()
        {
            using (var ts = new TransactionScope())
            {
                try
                {

                    var address = _addressRepository.GetAvailableAddressesForPhisicalDevice().First();
                    address.SetAssigned();
                    _addressRepository.Update(address);
                    _addressRepository.Save();

                    ts.Complete();
                    return address.AddressNumber;

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.InnerException);
                    Console.WriteLine(e.Message);
                }
            }
            return 0;
        }

        public IEnumerable<long> GetAvailableAddressesForUi()
        {
            using (var ts = new TransactionScope())
            {
                var addresses =
                    _addressRepository.GetAvailableAddressesForDevice().Select(address => address.AddressNumber);

                ts.Complete();
                return addresses;
            }
        }


        public void SetAllDevicesWithinGroup(long groupId, long state)
        {
            var devices = _deviceRepository.GetAllDevices().Where(d => d.GroupId == groupId).ToList();
            if (devices != null && devices.Count != 0)
            {
                foreach (var device in devices)
                {
                    device.AutomaticStateSetting(state);
                    _deviceRepository.Update(device);
                    _deviceRepository.Save();
                    _domainEvents.Raise(new DeviceStateHasChanged()
                    {
                        Device = device
                    });
                };
            }

        }

        public List<Device> GetAllDevicesWithinGroup(long groupId)
        {
            return _deviceRepository.GetAllDevices().Where(d => d.GroupId == groupId).ToList();
        }

        public List<Device> GetAllDevices(Device device)
        {
            return _deviceRepository.GetAllDevices().ToList();
        }

        public void UpdateDevice(Device device)
        {
            _deviceRepository.Update(device);
            _deviceRepository.Save();
        }

        public List<DeviceTypeDtoOutput> GetDeviceTypes()
        {
            var deviceTypes = _deviceTypeRepository.GetAllDeviceTypes().ToList();
            var result = Mapper.Map<List<DeviceTypeDtoOutput>>(deviceTypes);
            return result;
        }

        public List<GroupDtoOutput> GetGroups()
        {
            using (var ts = new TransactionScope())
            {

                var groups = _groupRepository.GetAllGroup().ToList();
                ts.Complete();
                var result = Mapper.Map<List<GroupDtoOutput>>(groups);

                return result;

            }
        }

        public IEnumerable<GroupDtoOutput> GetLonelyGroups()
        {
            var groups = _groupRepository.GetAllGroup().ToList();
            var devices = _deviceRepository.GetAllDevices().ToList();
            var lonelyGroups = new List<Group>();

            foreach (var group in groups)
            {
                if (devices.Select(device => device.GroupId).Contains(group.Id))
                    lonelyGroups.Add(group);
            }
            var lonelyGroupsDto = Mapper.Map<IEnumerable<GroupDtoOutput>>(lonelyGroups);
            return lonelyGroupsDto.Distinct();
        }



        public void CreateDevice(DeviceDtoInput deviceInput, string username, GroupDtoInput group = null)
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    var deviceTypeEntity = _deviceTypeRepository.GetDeviceType(deviceInput.DeviceTypeId);
                    var roomEntity = _buildingRepository.GetRoom(deviceInput.RoomId, deviceInput.FloorId,
                        deviceInput.BuildingId);
                    Group groupEntity = null;
                    if (deviceInput.GroupId.HasValue)
                    {
                        groupEntity = _groupRepository.GetGroup(deviceInput.GroupId.Value);
                    }
                    else
                    {
                        if (group == null) return;

                        groupEntity = Mapper.Map<Group>(group);
                        _groupRepository.AddGroup(groupEntity);
                        _groupRepository.Save();

                    }

                    if (!deviceTypeEntity.IsVirtual)
                    {
                        var address = _addressRepository.GetAddressByNumber(deviceInput.Address);
                        var deviceEntity = new Device(deviceTypeEntity.Id, roomEntity.Id, groupEntity.Id, address.AddressNumber,
                        deviceInput.Name);
                        _deviceRepository.AddDevice(deviceEntity);
                        _deviceRepository.Save();
                        _domainLoggerService.Publish(username,
                            string.Format("Device with name {0} with type {1} has been created in group {2}",
                                deviceEntity.Name, deviceTypeEntity.Type, groupEntity.Name));
                        address.DeviceId = deviceEntity.Id;
                        _addressRepository.Update(address);
                        _addressRepository.Save();
                    }
                    else
                    {
                        var deviceEntity = new Device(deviceTypeEntity.Id, roomEntity.Id, groupEntity.Id, 0,
                        deviceInput.Name);
                        _deviceRepository.AddDevice(deviceEntity);
                        _deviceRepository.Save();
                        _domainLoggerService.Publish(username,
                            string.Format("Device with name {0} with type {1} has been created in group {2}",
                                deviceEntity.Name, deviceTypeEntity.Type, groupEntity.Name));
                    }



                    ts.Complete();
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
