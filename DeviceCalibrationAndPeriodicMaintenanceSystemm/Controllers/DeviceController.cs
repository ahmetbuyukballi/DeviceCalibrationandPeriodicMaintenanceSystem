
using ApplicationCore.Abstraction;
using ApplicationCore.Dto.DevicesDto;
using ApplicationCore.Responses;
using Domain.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;
using System.Linq.Expressions;
using System.Net;

namespace DeviceCalibrationAndPeriodicMaintenanceSystemm.Controllers
{
    [Authorize]
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
        public async Task<ApiResponse<CreateDeviceDto>> CreateDevie(CreateDeviceDto models,IFormFile? formFile)
        {
            _logger.LogInformation("Ekleme işlemi başlatılıyor");
            var result = await _deviceService.CreateDevice(models,formFile);
            var _apiResponse = new ApiResponse<CreateDeviceDto?>();
          
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

                _apiResponse.IsSuccess = true;
                _apiResponse.HttpStatusCode= System.Net.HttpStatusCode.OK;
                _apiResponse.Result= result;
                return _apiResponse;
        }
        [HttpGet("get-device")]
        public async Task<ApiResponse<GetDeviceDto>> GetDevice()
        {
            var _apiResponse = new ApiResponse<GetDeviceDto>();
            _logger.LogInformation("Cihazlar listeleniyor");
            var result = await _deviceService.GetAllDevice();

                _apiResponse.IsSuccess = true;
                _apiResponse.HttpStatusCode = System.Net.HttpStatusCode.OK;
                _apiResponse.Result = result;
                return _apiResponse;
            
        }
        [HttpGet("get-id-device")]
        public async Task<ApiResponse<GetDeviceDto>> GetIdDevice(Guid id)
        {
            var _apiResponse=new ApiResponse<GetDeviceDto>();
            _logger.LogInformation("Cihaz listeleniyor");
            var result = await _deviceService.GetByIdDevice(id);

             _apiResponse.IsSuccess = true;
             _apiResponse.HttpStatusCode = System.Net.HttpStatusCode.OK;
             _apiResponse.Result = result;
             return _apiResponse;
        
        }
        [HttpPut("update-device")]
        public async Task<ApiResponse<UpdateDeviceDto>> UpdateDevice([FromBody] UpdateDeviceDto model,Guid id)
        {
            var _apiResponse=new ApiResponse<UpdateDeviceDto>();
            _logger.LogInformation("Cihaz güncellenecek");
            var result=await  _deviceService.UpdateDevice(model,id);
            
            
                _apiResponse.IsSuccess = true;
                _apiResponse.HttpStatusCode = System.Net.HttpStatusCode.OK;
                _apiResponse.Result = result;
                return _apiResponse;
        }
    }       
}
