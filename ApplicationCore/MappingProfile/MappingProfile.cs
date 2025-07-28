using ApplicationCore.Dto.DevicesDto;
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
            CreateMap<UpdateUserDto, AppUser>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => true));
            CreateMap<AppUser, UpdateUserDto>();
            CreateMap<AppUser, GetAllUserDto>();
            CreateMap<AppUser, GetByIdUserDto>()
                .ForMember(x => x.roles, opt => opt.Ignore());
            CreateMap<AppUser, DeleteUserDtos>();
            CreateMap<DeleteUserDtos, AppUser>();

            // Cihaz mappingleri
            CreateMap<Devices, CreateDeviceDto>()
                .ForMember(x => x.UserId,opt=>opt.MapFrom(x=>x.UserId))
                .ReverseMap();
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
            CreateMap<CreateDeviceDto, Devices>();
            CreateMap<Devices,CreateDeviceDto>();
        }
    }

}
