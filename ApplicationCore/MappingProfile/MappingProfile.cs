using ApplicationCore.Dto.DevicesDto;
using ApplicationCore.Dto.FeedBackDtos;
using ApplicationCore.Dto.MeintenancePlanDtos;
using ApplicationCore.Dto.MeintenanceRecordsDtos;
using ApplicationCore.Dto.NotificationLogDto;
using ApplicationCore.Dto.UserDto;
using AutoMapper;
using Domain.Entites;
using Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Kullanıcı mappingleri
            CreateMap<AppUser, CreateUserDto>();
            CreateMap<CreateUserDto, AppUser>().ForMember(opt=>opt.LastModifiedBy,
                x=>x.MapFrom(src=>"System"));
            CreateMap<UpdateUserDto, AppUser>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => true));
            CreateMap<AppUser, UpdateUserDto>();
            CreateMap<AppUser, GetAllUserDto>();
            CreateMap<AppUser, GetByIdUserDto>()
                .ForMember(x => x.roles, opt => opt.Ignore());
            CreateMap<AppUser, DeleteUserDtos>();
            CreateMap<DeleteUserDtos, AppUser>();

            // Cihaz mappingleri
            CreateMap<Devices, CreateDeviceDto>();
            CreateMap<CreateDeviceDto, Devices>();
            CreateMap<Devices, DeleteDeviceDtos>();
            CreateMap<Devices, UpdateDeviceDto>();
            CreateMap<UpdateDeviceDto, Devices>();
            CreateMap<GetDeviceDto, Devices>()
               .ForMember(dest => dest.AppUsers, opt => opt.Ignore())
                .ForMember(dest => dest.meintenancePlan, opt => opt.Ignore())
                .ForMember(dest => dest.meintenanceRecords, opt => opt.Ignore())
                .ForMember(dest => dest.NotificationLogs, opt => opt.Ignore())
            .ForMember(dest => dest.feedBack, opt => opt.Ignore());
            CreateMap<Devices, GetDeviceDto>();

            //Bakım planları mappingler
            CreateMap<CreateMeintenancePlanDto, MeintenancePlan>();
            CreateMap<MeintenancePlan,CreateMeintenancePlanDto>();
            CreateMap<GetMeintenancePlanDtos, MeintenancePlan>();
            CreateMap<MeintenancePlan,GetMeintenancePlanDtos>();
            CreateMap<MeintenancePlan, DeleteMeintenancePlanDto>();
            CreateMap<GetMeintenancePlanDtos, MeintenancePlan>();
            CreateMap<MeintenancePlan, GetMeintenancePlanDtos>();
            CreateMap<UpdateMeintenancePlanDto,MeintenancePlan>();
            CreateMap<MeintenancePlan, UpdateMeintenancePlanDto>();
            //Bakım kayıtları mappingleri
            CreateMap<CreateRecordsDtos, MeintenanceRecord>();
            CreateMap<MeintenanceRecord,CreateRecordsDtos>();
            CreateMap<GetRecordsDtos, MeintenanceRecord>();
            CreateMap<UpdateRecordsDtos,MeintenanceRecord>();
            CreateMap<MeintenanceRecord, UpdateRecordsDtos>();
            CreateMap<MeintenanceRecord, DeleteRecordsDtos>();
            CreateMap<MeintenanceRecord, GetRecordsDtos>();
            //Bildirim işlemler
            CreateMap<CreateNotificationLogDtos, NotificationLog>();
            //FeedBack mappingleri
            CreateMap<CreateFeedBackDto, FeedBack>();
            CreateMap<FeedBack,CreateFeedBackDto>();
            CreateMap<FeedBack,UpdateFeedBackDto>();
            CreateMap<UpdateFeedBackDto, FeedBack>();
            CreateMap<FeedBack, DeleteFeedBackDto>();
            CreateMap<FeedBack, GetFeedBackDto>();
        }
    }

}
