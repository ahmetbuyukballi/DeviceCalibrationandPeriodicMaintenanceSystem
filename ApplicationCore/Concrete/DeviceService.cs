using ApplicationCore.Abstraction;
using ApplicationCore.BaseService;
using ApplicationCore.Dto.DevicesDto;
using ApplicationCore.Dto.UserDto;
using AutoMapper;
using Domain.Entites;
using Domain.Identity;
using Domain.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
    public class DeviceService: BaseService<Devices>, IDevicesService
    {
        private readonly IMapper _mapper;

        public DeviceService(IWriteRepository<Devices> writeRepository,
            IRepository<Devices> repository,
            IReadRepository<Devices> readRepository,
            IMapper mapper,
            UserManager<AppUser> userManager,
            IHttpContextAccessor httpContextAccessor) :base(writeRepository, mapper, repository, readRepository, userManager, httpContextAccessor)
        {
            _mapper = mapper;
        }
        public async Task<CreateDeviceDto> CreateDevice(CreateDeviceDto device)
        {   
                var models = _mapper.Map<Devices>(device);
                var dto = await AddAsync(models, null, x=>x.serialNo==device.serialNo);
                return _mapper.Map<CreateDeviceDto>(dto);

        }
        public async Task<UpdateDeviceDto> UpdateDevice(UpdateDeviceDto models,Guid id,params Expression<Func<Devices, object>>[] includes)
        {
            var entity = _mapper.Map<Devices>(models);
            var model = _mapper.Map(models, entity);
            var result = await UpdateAsync(model, x=>x.Id==id,includes);
            return _mapper.Map<UpdateDeviceDto>(result);
        }
        public async Task<DeleteDeviceDtos> DeleteDevice(Guid id)
        {   var result=await DeleteAsync(id);
            return _mapper.Map<DeleteDeviceDtos>(result);
 
        }
        public async Task<List<GetDeviceDto>> GetAllDevice()
        {
            var result = await GetAllAsync();
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
            var result = await GetByIdAsync(x=>x.Id==id);

              return _mapper.Map<GetDeviceDto>(result);

          
        }

    }
}
