using ApplicationCore.Abstraction;
using ApplicationCore.Dto.MeintenancePlanDtos;
using ApplicationCore.Responses;
using Domain.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using System.Text.Json.Serialization;

namespace DeviceCalibrationAndPeriodicMaintenanceSystemm.Controllers
{
    [Authorize(Roles ="admin")]
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
 
            _apiResponse.IsSuccess = true;
            _apiResponse.HttpStatusCode = System.Net.HttpStatusCode.OK;
            _apiResponse.Result = result;
            return _apiResponse;
        }
        [HttpPut("update-plan")]
        public async Task<ApiResponse<UpdateMeintenancePlanDto>> UpdatePlan(UpdateMeintenancePlanDto models, Guid id)
        {
            _logger.LogInformation("Güncelleme işlemi başlıyor");
            var result = await _planService.UpdatePlan(models, id);
            var _apiResponse = new ApiResponse<UpdateMeintenancePlanDto>();
 
            _apiResponse.IsSuccess = true;
            _apiResponse.HttpStatusCode = System.Net.HttpStatusCode.OK;
            _apiResponse.Result = result;
            return _apiResponse;
        }
        [AllowAnonymous]
        [HttpGet("get-plan")]
        public async Task<ApiResponse<List<GetMeintenancePlanDtos>>> GetAllPlan()
        {
            _logger.LogInformation("Listeleme işlemi başlıyor");
            var _apiResponse = new ApiResponse<List<GetMeintenancePlanDtos>>();
            var result = await _planService.GetAllPlan();
       
            _apiResponse.IsSuccess = true;
            _apiResponse.HttpStatusCode = System.Net.HttpStatusCode.OK;
            _apiResponse.Result = result;
            return _apiResponse;
        }
        [AllowAnonymous]
        [HttpGet("get-id-plan")]
        public async Task<ApiResponse<GetMeintenancePlanDtos>> GetIdPlan(Guid id)
        {
            _logger.LogInformation("Idye göre listeleme işlemi başlıyor");
            var _apiResponse = new ApiResponse<GetMeintenancePlanDtos>();
            var result = await _planService.GetIdPlan(id);
      
            _apiResponse.IsSuccess = true;
            _apiResponse.HttpStatusCode = System.Net.HttpStatusCode.OK;
            _apiResponse.Result = result;
            return _apiResponse;
        }
    }
}