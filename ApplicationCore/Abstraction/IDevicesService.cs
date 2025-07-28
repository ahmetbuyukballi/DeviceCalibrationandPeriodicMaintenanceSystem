using ApplicationCore.Dto.DevicesDto;
using Domain.Entites;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Abstraction
{
    public interface IDevicesService
    {
        public Task<CreateDeviceDto> CreateDevice(CreateDeviceDto model,Expression<Func<Devices,bool>> method,params Expression<Func<Devices, object>>[] includes);
        public Task<UpdateDeviceDto> UpdateDevice(UpdateDeviceDto device,Expression<Func<Devices,bool>> method,params Expression<Func<Devices,object>>[] includes);
        public Task<DeleteDeviceDtos> DeleteDevice(Guid id);
        public Task<List<GetDeviceDto>> GetAllDevice();
        public Task<GetDeviceDto> GetByIdDevice(Expression<Func<Devices, bool>> filtre, params Expression<Func<Devices, object>>[] includes);
    }
}
