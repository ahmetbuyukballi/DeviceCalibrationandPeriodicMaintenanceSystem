using ApplicationCore.Abstraction;
using ApplicationCore.BaseService;
using ApplicationCore.Dto.DevicesDto;
using ApplicationCore.Dto.UserDto;
using AutoMapper;
using Domain.Entites;
using Domain.Identity;
using Domain.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Concrete
{
    public class DeviceService :GetClaimsBaseService,IDevicesService
    {
        private readonly IBaseService<Devices> _baseService;
        private readonly IMapper _mapper;
        private readonly IReadRepository<Devices> _readRepository;
        public DeviceService(IHttpContextAccessor _httpContextAccessor, IBaseService<Devices> baseService,IMapper mapper,IReadRepository<Devices> readRepository) :base(_httpContextAccessor)
        { 
             _baseService= baseService;
            _mapper = mapper;
            _readRepository= readRepository;
        }
        public async Task<CreateDeviceDto> CreateDevice(CreateDeviceDto device, params Expression<Func<Devices, object>>[] includes)
        {   Expression<Func<Devices,bool>> method=x=>x.serialNo==device.serialNo;
            var entity=await _readRepository.GetSingleAsync(method);
            if (entity == null)
            {
                var models = _mapper.Map<Devices>(device);
                var dto = await _baseService.AddAsync(models, null, method, includes);
                return _mapper.Map<CreateDeviceDto>(dto);
            }
            return null;

        }
        public async Task<UpdateDeviceDto> UpdateDevice(UpdateDeviceDto models,Guid id,params Expression<Func<Devices, object>>[] includes)
        {
            Expression<Func<Devices,bool>> method=x=>x.Id==id;
            var entity = await _readRepository.GetSingleAsync(method);
            if (entity == null) return null;
            var model = _mapper.Map(models, entity);
            var result = await _baseService.UpdateAsync(model, method,includes);
            return _mapper.Map<UpdateDeviceDto>(result);
        }
        public async Task<DeleteDeviceDtos> DeleteDevice(Guid id)
        {   var entity=await _readRepository.GetByIdAsync(id);
            if (entity != null)
            {
                var result = await _baseService.DeleteAsync(id);
                var model = _mapper.Map<DeleteDeviceDtos>(result);
            }
            return _mapper.Map<DeleteDeviceDtos>(entity);
 
        }
        public async Task<List<GetDeviceDto>> GetAllDevice()
        {
            var result = await _baseService.GetAllAsync();
            var users=new List<GetDeviceDto>();

            foreach (var models in result)
            {
             var dto = _mapper.Map<GetDeviceDto>(models);
             users.Add(dto);
            }
            return users;
        }
        public async Task<GetDeviceDto> GetByIdDevice(Guid id, params Expression<Func<Devices, object>>[] includes)
        {
            Expression<Func<Devices, bool>> filtre = x => x.Id == id;
            var entity = await _readRepository.GetSingleAsync(filtre);
            if (entity != null)
            {
                var result = await _baseService.GetByIdAsync(filtre, includes);
                return _mapper.Map<GetDeviceDto>(result);
            }
            return _mapper.Map<GetDeviceDto>(entity);
          
        }

    }
}
