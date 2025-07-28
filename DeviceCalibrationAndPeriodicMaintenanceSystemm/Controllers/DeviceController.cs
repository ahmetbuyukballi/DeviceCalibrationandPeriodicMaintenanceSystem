
using ApplicationCore.Abstraction;
using ApplicationCore.Dto.DevicesDto;
using ApplicationCore.Responses;
using Domain.Entites;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;
using System.Linq.Expressions;

namespace DeviceCalibrationAndPeriodicMaintenanceSystemm.Controllers
{
    [Route("api/device")]
    public class DeviceController : ControllerBase
    {
        private readonly IDevicesService _deviceService;
        private readonly ILogger<DeviceController> _logger;
        public DeviceController(IDevicesService deviceService, ILogger<DeviceController> logger)
        {
            _deviceService = deviceService;
            _logger = logger;
        }
        [HttpPost("add-device")]
        public async Task<ApiResponse<CreateDeviceDto>> CreateDevie(CreateDeviceDto models)
        {
            Expression<Func<Devices, bool>> method = x => x.serialNo==models.serialNo;
            _logger.LogInformation("Ekleme işlemi başlıyor");
            var result = await _deviceService.CreateDevice(models, method);
            var _apiResponse = new ApiResponse<CreateDeviceDto?>();
            if (result == null)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.HttpStatusCode = System.Net.HttpStatusCode.BadRequest;
                _apiResponse.ErrorMessages.Add("Dto boş döndü");
                return _apiResponse;
            }
            _apiResponse.IsSuccess=true;
            _apiResponse.HttpStatusCode=System.Net.HttpStatusCode.OK;
            _apiResponse.Result = result;
            return _apiResponse;
        }
        [HttpDelete("delete-device")]
        public async Task<ApiResponse<DeleteDeviceDtos>> DeleteDevice(Guid id)
        {
            _logger.LogInformation("Silme işlemi başlatılıyor");
            var _apiResponse=new ApiResponse<DeleteDeviceDtos>();
            var result=await _deviceService.DeleteDevice(id);
            if (result != null)
            {
                _apiResponse.IsSuccess = true;
                _apiResponse.HttpStatusCode= System.Net.HttpStatusCode.OK;
                _apiResponse.Result= result;
                return _apiResponse;
            }
            _apiResponse.IsSuccess=false;
            _apiResponse.ErrorMessages.Add("Entity bulunamadı");
            _apiResponse.HttpStatusCode = System.Net.HttpStatusCode.BadRequest;
            return _apiResponse;
        }
        [HttpGet("get-device")]
        public async Task<ApiResponse<GetDeviceDto>> GetDevice()
        {
            var _apiResponse = new ApiResponse<GetDeviceDto>();
            _logger.LogInformation("Cihazlar listeleniyor");
            var result = await _deviceService.GetAllDevice();
            _apiResponse.IsSuccess = true;
            _apiResponse.HttpStatusCode=System.Net.HttpStatusCode.OK;
            _apiResponse.Result= result;
            return _apiResponse;
        }
        [HttpGet("get-id-device")]
        public async Task<ApiResponse<GetDeviceDto>> GetIdDevice(Guid id)
        {
            var _apiResponse=new ApiResponse<GetDeviceDto>();
            _logger.LogInformation("Cihaz listeleniyor");
            Expression<Func<Devices, bool>> filtre = x => x.Id == id;
            var result = await _deviceService.GetByIdDevice(filtre);
            _apiResponse.IsSuccess = true;
            _apiResponse.HttpStatusCode=System.Net.HttpStatusCode.OK;
            _apiResponse.Result= result;
            return _apiResponse;

        }
        [HttpPut("update-device")]
        public async Task<ApiResponse<UpdateDeviceDto>> UpdateDevice([FromBody] UpdateDeviceDto model,Guid id)
        {
            var _apiResponse=new ApiResponse<UpdateDeviceDto>();
            _logger.LogInformation("Cihaz güncellenecek");
            Expression<Func<Devices,bool>> filtre=x=>x.Id==id;
            var result=await  _deviceService.UpdateDevice(model,filtre);
            _apiResponse.IsSuccess = true;
            _apiResponse.HttpStatusCode = System.Net.HttpStatusCode.OK;
            _apiResponse.Result= result;
            return _apiResponse;
        }
    }       
}
