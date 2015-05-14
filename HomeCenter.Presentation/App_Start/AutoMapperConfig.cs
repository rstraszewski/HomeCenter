using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using HomeCenter.DeviceDomain;
using HomeCenter.HistoryDomain;
using HomeCenter.Models.Helpers;

namespace HomeCenter.App_Start
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<Device, DeviceDtoOutput>()
                .ForMember(dest => dest.Binary, opt => opt.MapFrom(src => src.DeviceType.Binary))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Group.Description))
                .ForMember(dest => dest.DeviceState, opt => opt.MapFrom(src => src.DeviceState))
                .ForMember(dest => dest.GroupId, opt => opt.MapFrom(src => src.GroupId))
                .ForMember(dest => dest.MaxValue, opt => opt.MapFrom(src => src.DeviceType.Range.MaxValue))
                .ForMember(dest => dest.MinValue, opt => opt.MapFrom(src => src.DeviceType.Range.MinValue))
                .ForMember(dest => dest.GroupName, opt => opt.MapFrom(src => src.Group.Name))
                .ForMember(dest => dest.RoomId, opt => opt.MapFrom(src => src.RoomId))
                .ForMember(dest => dest.Changeable, opt => opt.MapFrom(src => src.DeviceType.Changable))
                .ForMember(dest => dest.DeviceName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.IsVirtual, opt => opt.MapFrom(src => src.DeviceType.IsVirtual))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));
            Mapper.CreateMap<Room, RoomDtoOutput>();
            Mapper.CreateMap<Floor, FloorDtoOutput>();
            Mapper.CreateMap<RoomDtoInput, Room>();
            Mapper.CreateMap<FloorDtoInput, Floor>();
            Mapper.CreateMap<BuildingDtoInput, Building>();
            Mapper.CreateMap<Building, BuildingDtoOutput>();
            Mapper.CreateMap<DeviceType, DeviceTypeDtoOutput>();
            Mapper.CreateMap<Group, GroupDtoOutput>();
            Mapper.CreateMap<GroupDtoInput, Group>();
            Mapper.CreateMap<DeviceRelationDtoInput, DeviceRelation>();
            Mapper.CreateMap<DeviceRelation, DeviceRelationDto>()
                .ForMember(dest => dest.StateRelationDescription, opt => opt.MapFrom(src => src.StateRelation.GetDescription()));
            Mapper.CreateMap<DeviceRelationDto, DeviceRelation>();
            Mapper.CreateMap<HistoryLog, HistoryLogDto>()
                .ForMember(dest => dest.DateTime, opt => opt.MapFrom(src => src.DateTime.ToLongTimeString() + " - " + src.DateTime.ToLongDateString()));
            Mapper.CreateMap<DeviceJob, DeviceJobDto>();
        }
    }
}