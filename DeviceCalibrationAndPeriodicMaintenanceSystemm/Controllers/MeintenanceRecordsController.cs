using ApplicationCore.Abstraction;
using ApplicationCore.Dto.MeintenanceRecordsDtos;
using ApplicationCore.Responses;
using Domain.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace DeviceCalibrationAndPeriodicMaintenanceSystemm.Controllers
{
    [Authorize(Roles ="admin")]
    [Route("api/MeintenanceRecords")]
    public class MeintenanceRecordsController : ControllerBase
    {
        private readonly IMeintenanceRecordService _service;
        private readonly ILogger<MeintenanceRecord> _logger;
       public MeintenanceRecordsController(IMeintenanceRecordService service, ILogger<MeintenanceRecord> logger)
        {
            _service = service;
            _logger = logger;
        }
        [HttpPost("create-record")]
        public async Task<ApiResponse<CreateRecordsDtos>> CreateRecord(CreateRecordsDtos models)
        {
            _logger.LogInformation("Kayıt ekleme işlemi başlıyor");
            var _apiResponse = new ApiResponse<CreateRecordsDtos>();
            var result = await _service.CreateRecords(models);
            _apiResponse.IsSuccess = true;
            _apiResponse.HttpStatusCode = System.Net.HttpStatusCode.OK;
            _apiResponse.Result = result;
            return _apiResponse;
        }
        [HttpDelete("delete-record")]
        public async Task<ApiResponse<DeleteRecordsDtos>> DeleteRecord(Guid id)
        {
            _logger.LogInformation("Kayıt silme işlemi başlatılıyor");
            var _apiResponse = new ApiResponse<DeleteRecordsDtos>();
            var result = await _service.DeleteRecords(id);
            _apiResponse.IsSuccess = true;
            _apiResponse.HttpStatusCode = System.Net.HttpStatusCode.OK;
            _apiResponse.Result = result;
            return _apiResponse;
        }
        [HttpPut("update-record")]
        public async Task<ApiResponse<UpdateRecordsDtos>> UpdateRecord(UpdateRecordsDtos models)
        {
            _logger.LogInformation("Kayıt güncelleme işlemi başlatılıyor");
            var _apiResponse = new ApiResponse<UpdateRecordsDtos>();
            var result = await _service.UpdateRecords(models);
            _apiResponse.IsSuccess = true;
            _apiResponse.HttpStatusCode = System.Net.HttpStatusCode.OK;
            _apiResponse.Result = result;
            return _apiResponse;
        }
        [AllowAnonymous]
        [HttpGet("get-all-records")]
        public async Task<ApiResponse<GetRecordsDtos>> GetAllRecord()
        {
            _logger.LogInformation("Kayıt listeleme işlemi başlatılıyor");
            var _apiResponse = new ApiResponse<GetRecordsDtos>();
            var result = await _service.GetAllRecords();
            _apiResponse.IsSuccess = true;
            _apiResponse.HttpStatusCode = System.Net.HttpStatusCode.OK;
            _apiResponse.Result = result;
            return _apiResponse;
        }
        [AllowAnonymous]
        [HttpGet("get-id-records")]
        public async Task<ApiResponse<GetRecordsDtos>> GetIdRecords(Guid id)
        {
            _logger.LogInformation("Idye göre kayıt listeleme işlemi başlatılıyor");
            var _apiResponse = new ApiResponse<GetRecordsDtos>();
            var result = await _service.GetByIdRecords(id);
            _apiResponse.IsSuccess = true;
            _apiResponse.HttpStatusCode = System.Net.HttpStatusCode.OK;
            _apiResponse.Result = result;
            return _apiResponse;
        }
        [HttpPost("dowloand-recordsexcel")]
        public async Task<IActionResult> GetRecordsExcel(Guid id)
        {
            _logger.LogInformation("Excel dosyası oluşturuluyor");
            var stream=await _service.GetRecordsExcel(id);

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "BakimRaporu.xlsx");
        }
    }
}
