using ApplicationCore.Abstraction;
using ApplicationCore.Dto.MeintenancePlanDtos;
using ApplicationCore.Responses;
using Domain.Entites;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace DeviceCalibrationAndPeriodicMaintenanceSystemm.Controllers
{
    public class MeintancePlanController : ControllerBase
    {
        private readonly IMeintenancePlanService _planService;
        private readonly ILogger<MeintancePlanController> _logger;   
        public MeintancePlanController(IMeintenancePlanService planService,ILogger<MeintancePlanController> logger) 
        {
            _planService = planService;
            _logger = logger;
        }
        [HttpPost("create-plan")]
        public async Task<ApiResponse<CreateMeintenancePlanDto>> CreatePlan(CreateMeintenancePlanDto model)
        {
            Expression<Func<MeintenancePlan,bool>> filtre=x=>x.Name==model.Name;
            _logger.LogInformation("Ekleme işlemi başlıyor");
            var result=await _planService.CreatePlan(model,filtre);
            var _apiResponse = new ApiResponse<CreateMeintenancePlanDto>();
            _apiResponse.IsSuccess = true;
            _apiResponse.HttpStatusCode=System.Net.HttpStatusCode.OK;
            _apiResponse.Result = result;
            return _apiResponse;
        }
    }
}
