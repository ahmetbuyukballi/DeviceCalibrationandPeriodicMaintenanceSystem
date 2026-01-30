using ApplicationCore.Abstraction;
using ApplicationCore.BaseService;
using ApplicationCore.Dto.DevicesDto;
using ApplicationCore.Dto.UserDto;
using AutoMapper;
using Domain.Entites;
using Domain.Identity;
using Domain.Repository;
using Microsoft.AspNetCore.Authorization;
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
    public class DeviceService:BaseService<Devices>, IDevicesService
    {
        private readonly IMapper _mapper;
        private readonly IUploadImageService _uploadImageService;
        private readonly GetClaimsBaseService _getClaimsBaseService;
        public DeviceService(IWriteRepository<Devices> writeRepository,
            IRepository<Devices> repository,
            IReadRepository<Devices> readRepository,
            IMapper mapper,
            UserManager<AppUser> userManager,
            IHttpContextAccessor httpContextAccessor,
            IUploadImageService uploadImageService,
            GetClaimsBaseService getClaimsBaseService) :base(writeRepository, mapper, repository, readRepository, userManager, httpContextAccessor)
        {
            _mapper = mapper;
            _uploadImageService = uploadImageService;
            _getClaimsBaseService = getClaimsBaseService;
        }
        public async Task<CreateDeviceDto> CreateDevice(CreateDeviceDto device, IFormFile? file=null)
        {
            if (file != null)
            {
                if(file.ContentType == "image/png" || file.ContentType == "image/jpeg")
                {
                    var ImageUrl = await _uploadImageService.UploadImageAsync(file);
                    var entity = _mapper.Map<Devices>(device);
                    entity.UserId = _getClaimsBaseService.GetUserId();
                    entity.ImagePath = ImageUrl;
                    var result = await AddAsync(entity, null, x => x.SerialNo == device.serialNo);
                    return _mapper.Map<CreateDeviceDto>(result);
                }
               

            }
                var models = _mapper.Map<Devices>(device);
                var model = await AddAsync(models, null, x=>x.SerialNo==device.serialNo);
                model.UserId = _getClaimsBaseService.GetUserId();
                return _mapper.Map<CreateDeviceDto>(model);

        }
        [Authorize(Roles ="admin")]
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
        [Authorize(Roles ="admin")]
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
        [Authorize(Roles ="admin")]
        public async Task<GetDeviceDto> GetByIdDevice(Guid id, params Expression<Func<Devices, object>>[] includes)
        {
            var result = await GetByIdAsync(x=>x.Id==id);
            return _mapper.Map<GetDeviceDto>(result);

          
        }

    }
}
