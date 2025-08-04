using ApplicationCore.Abstraction;
using ApplicationCore.Concrete;
using ApplicationCore.Dto.FeedBackDtos;
using ApplicationCore.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace DeviceCalibrationAndPeriodicMaintenanceSystemm.Controllers
{
    [Authorize]
    public class FeedBackController : ControllerBase
    {
        private readonly IFeedBackService _feedBackService;
        private readonly ILogger _logger;

        public FeedBackController(IFeedBackService feedBackService, ILogger logger)
        {
            _feedBackService = feedBackService;
            _logger = logger;
        }

        [HttpPost("add-feedback")]
        public async Task<ApiResponse<CreateFeedBackDto>> CreateFeedBack([FromBody] CreateFeedBackDto models)
        {
            _logger.LogInformation("Ekleme işlemi başlıyor");
            var _apiResponse=new ApiResponse<CreateFeedBackDto>();

            var result=await _feedBackService.CreateFeedBack(models);
            _apiResponse.IsSuccess = true;
            _apiResponse.HttpStatusCode = System.Net.HttpStatusCode.OK;
            _apiResponse.Result=result;
            return _apiResponse;
        }

        [HttpDelete("delete-feedback")]
        public async Task<ApiResponse<DeleteFeedBackDto>> DeleteFeedBack(Guid id)
        {
            _logger.LogInformation("Silme işlemi başlıyor");
            var _apiResponse=new ApiResponse<DeleteFeedBackDto>();

            var result=await _feedBackService.DeleteFeedBack(id);
            _apiResponse.IsSuccess = true;
            _apiResponse.HttpStatusCode=System.Net.HttpStatusCode.OK;
            _apiResponse.Result=result;
            return _apiResponse;
        }

        [HttpGet("getall-feedback")]
        public async Task<ApiResponse<GetFeedBackDto>> GetAllFeedBack()
        {
            _logger.LogInformation("Listeleme işlemi başlıyor");
            var _apiResponse=new ApiResponse<GetFeedBackDto>();

            var result=await _feedBackService.GetAllFeedBack(); 
            _apiResponse.IsSuccess=true;
            _apiResponse.HttpStatusCode= System.Net.HttpStatusCode.OK;
            _apiResponse.Result=result;
            return _apiResponse;
        }

        [HttpGet("get-FeedBack")]
        public async Task<ApiResponse<GetFeedBackDto>> GetFeedBack(Guid id)
        {
            _logger.LogInformation("Listeleme işlemi başlıyor");
            var _apiResponse=new ApiResponse<GetFeedBackDto>();

            var result = await _feedBackService.GetByIdFeedBack(id);
            _apiResponse.IsSuccess=true;
            _apiResponse.HttpStatusCode = System.Net.HttpStatusCode.OK;
            _apiResponse.Result=result;
            return _apiResponse;
        }
        [HttpPut("update-feedbacl")]
        public async Task<ApiResponse<UpdateFeedBackDto>> UpdateFeedBack(UpdateFeedBackDto models)
        {
            _logger.LogInformation("Guncelleme işlemi başlıyor");
            var _apiResponse=new ApiResponse<UpdateFeedBackDto>();

            var result=await _feedBackService.UpdateFeedback(models);
            _apiResponse.IsSuccess=true;
            _apiResponse.HttpStatusCode=System.Net.HttpStatusCode.OK;
            _apiResponse.Result=result;
            return _apiResponse;

        }

    }
}
