using ApplicationCore.Abstraction;
using ApplicationCore.BaseService;
using ApplicationCore.Dto.DevicesDto;
using ApplicationCore.Dto.UserDto;
using AutoMapper;
using Domain.Entites;
using Domain.Identity;
using Domain.Repository;
using Microsoft.AspNetCore.Http;
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
        public async Task<CreateDeviceDto> CreateDevice(CreateDeviceDto device, Expression<Func<Devices, bool>> method, params Expression<Func<Devices, object>>[] includes)
        {
           var entity=_mapper.Map<Devices>(device);
           var dto = await _baseService.AddAsync(entity,null,method,includes);
           return _mapper.Map<CreateDeviceDto>(dto);

        }
        public async Task<UpdateDeviceDto> UpdateDevice(UpdateDeviceDto models,Expression<Func<Devices,bool>> method,params Expression<Func<Devices, object>>[] includes)
        {
            var entity = await _readRepository.GetSingleAsync(method);
            if (entity == null) 
            {
                throw new ArgumentNullException("Böyle bir entity yok");
            }
            var model = _mapper.Map(models, entity);
            var result = await _baseService.UpdateAsync(model, method,includes);
            if (result==null)
            {
                throw new ArgumentNullException("Dto hatalı döndü");
            }
            return _mapper.Map<UpdateDeviceDto>(result);
        }
        public async Task<DeleteDeviceDtos> DeleteDevice(Guid id)
        {
            var result=await _baseService.DeleteAsync(id);

                var model = _mapper.Map<DeleteDeviceDtos>(result);
                return model;
 
        }
        public async Task<List<GetDeviceDto>> GetAllDevice()
        {
            var result = await _baseService.GetAllAsync();
            var users=new List<GetDeviceDto>();
            if (result == null)
            {
                throw new ArgumentNullException("Cihaz yok");
            }
            foreach(var models in result)
            {
                var dto=_mapper.Map<GetDeviceDto>(models);
                users.Add(dto);
            }
            return users;
        }
        public async Task<GetDeviceDto> GetByIdDevice(Expression<Func<Devices, bool>> filtre, params Expression<Func<Devices, object>>[] includes)
        {
            var result = await _baseService.GetByIdAsync(filtre, includes);
            if (result == null) 
            {
                throw new ArgumentNullException("Böyle bir cihaz sistemimizde bulunmamaktadır");

            }
            return _mapper.Map<GetDeviceDto>(result);
          
        }

    }
}
