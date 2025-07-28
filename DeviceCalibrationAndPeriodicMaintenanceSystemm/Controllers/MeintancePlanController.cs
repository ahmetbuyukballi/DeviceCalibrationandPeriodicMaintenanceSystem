using ApplicationCore.Abstraction;
using ApplicationCore.Dto.MeintenancePlanDtos;
using ApplicationCore.Responses;
using Domain.Entites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using System.Text.Json.Serialization;

namespace DeviceCalibrationAndPeriodicMaintenanceSystemm.Controllers
{
    [Route("api/user")]
    public class MeintancePlanController : ControllerBase
    {
        private readonly IMeintenancePlanService _planService;
        private readonly ILogger<MeintancePlanController> _logger;
        public MeintancePlanController(IMeintenancePlanService planService, ILogger<MeintancePlanController> logger)
        {
            _planService = planService;
            _logger = logger;
        }
        [HttpPost("create-plan")]
        public async Task<ApiResponse<CreateMeintenancePlanDto>> CreatePlan(CreateMeintenancePlanDto model)
        {
            Expression<Func<MeintenancePlan, bool>> filtre = x => x.Name == model.Name;
            _logger.LogInformation("Ekleme işlemi başlıyor");
            var result = await _planService.CreatePlan(model, filtre);
            var _apiResponse = new ApiResponse<CreateMeintenancePlanDto>();
            _apiResponse.IsSuccess = true;
            _apiResponse.HttpStatusCode = System.Net.HttpStatusCode.OK;
            _apiResponse.Result = result;
            return _apiResponse;
        }
        [HttpDelete("delete-plan")]
        public async Task<ApiResponse<DeleteMeintenancePlanDto>> DeletePlan(Guid id)
        {
            _logger.LogInformation("Silme işlemi başlıyor");
            var result = await _planService.DeletePlan(id);
            var _apiResponse = new ApiResponse<DeleteMeintenancePlanDto>();
            if (result == null)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessages.Add("Entity bulunamadı");
                _apiResponse.HttpStatusCode = System.Net.HttpStatusCode.NotFound;
                return _apiResponse;
            }
            _apiResponse.IsSuccess = true;
            _apiResponse.HttpStatusCode = System.Net.HttpStatusCode.OK;
            _apiResponse.Result = result;
            return _apiResponse;
        }
        [HttpPut("update-plan")]
        public async Task<ApiResponse<UpdateMeintenancePlanDto>> UpdatePlan(UpdateMeintenancePlanDto models, Guid id, params Expression<Func<MeintenancePlan, object>>[] includes)
        {
            _logger.LogInformation("Güncelleme işlemi başlıyor");
            var result = await _planService.UpdatePlan(models, id, includes);
            var _apiResponse = new ApiResponse<UpdateMeintenancePlanDto>();
            if (result == null)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessages.Add("Entity bulunamadı");
                _apiResponse.HttpStatusCode = System.Net.HttpStatusCode.NotFound;
                return _apiResponse;
            }
            _apiResponse.IsSuccess = true;
            _apiResponse.HttpStatusCode = System.Net.HttpStatusCode.OK;
            _apiResponse.Result = result;
            return _apiResponse;
        }
        [HttpGet("get-plan")]
        public async Task<ApiResponse<GetMeintenancePlanDtos>> GetAllPlan()
        {
            _logger.LogInformation("Listeleme işlemi başlıyor");
            var _apiResponse = new ApiResponse<GetMeintenancePlanDtos>();
            var result = await _planService.GetAllPlan();
            if (result == null)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessages.Add("Entity bulunamadı");
                _apiResponse.HttpStatusCode = System.Net.HttpStatusCode.NotFound;
                return _apiResponse;
            }
            _apiResponse.IsSuccess = true;
            _apiResponse.HttpStatusCode = System.Net.HttpStatusCode.OK;
            _apiResponse.Result = result;
            return _apiResponse;
        }
        [HttpGet("get-id-plan")]
        public async Task<ApiResponse<GetMeintenancePlanDtos>> GetIdPlan(Guid id, params Expression<Func<MeintenancePlan, object>>[] includes)
        {
            _logger.LogInformation("Idye göre listeleme işlemi başlıyor");
            var _apiResponse = new ApiResponse<GetMeintenancePlanDtos>();
            var result = await _planService.GetIdPlan(id, includes);
            if (result == null)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.HttpStatusCode = System.Net.HttpStatusCode.NotFound;
                _apiResponse.ErrorMessages.Add("Entity bulunamadı");
                return _apiResponse;
            }
            _apiResponse.IsSuccess = true;
            _apiResponse.HttpStatusCode = System.Net.HttpStatusCode.OK;
            _apiResponse.Result = result;
            return _apiResponse;
        }
    }
}