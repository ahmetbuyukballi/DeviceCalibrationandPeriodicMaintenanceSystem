using ApplicationCore.Abstraction;
using ApplicationCore.Dto.Token;
using ApplicationCore.Dto.UserDto;
using ApplicationCore.Responses;
using Domain.Entites;
using Domain.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Net;

namespace DeviceCalibrationAndPeriodicMaintenanceSystemm.Controllers
{
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;
        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }
        [HttpPost("login-user")]
        public async Task<ApiResponse<LoginResponseModels>> Login(LoginUserDto model)
        {
            var _apiresponse = new ApiResponse<LoginResponseModels>();
            _logger.LogInformation("İşlem başladı");
            var result = await _userService.Login(model);

            _apiresponse.IsSuccess = true;
            _apiresponse.Result = result;
            _apiresponse.HttpStatusCode = HttpStatusCode.OK;
            return _apiresponse;
        }
        [HttpPost("add-user")]
        public async Task<ApiResponse<CreateUserDto>> Register(CreateUserDto model)
        {
            var _apiresponse = new ApiResponse<CreateUserDto>();
            var result = await _userService.Register(model);
            
            _apiresponse.IsSuccess = true;
            _apiresponse.HttpStatusCode = HttpStatusCode.OK;
            _apiresponse.Result = result;
            return _apiresponse;
            


        }
        [HttpPut("update-user")]
        public async Task<ApiResponse<UpdateUserDto>> Update(UpdateUserDto model)
        {
            var _apiResponse=new ApiResponse<UpdateUserDto>();
            Expression<Func<AppUser,bool>> method=x=>x.Id==model.Id;
            _logger.LogInformation("Update işlemi başladı");
            var result = await _userService.UpdateUser(model, method);
            if(result != null) 

            {
                    _apiResponse.IsSuccess = true;
                    _apiResponse.HttpStatusCode = HttpStatusCode.OK;
                    _apiResponse.Result = result;
                    return _apiResponse;
            }
            _apiResponse.IsSuccess= false;
            _apiResponse.ErrorMessages.Add("Böyle bir  entity yok");
            _apiResponse.HttpStatusCode= HttpStatusCode.NotFound;
            return _apiResponse;

            }
        [HttpDelete("delete-user")]
        public async Task<ApiResponse<DeleteUserDtos>> Delete(Guid id)
        {
            var _apiResponse = new ApiResponse<DeleteUserDtos>();
            _logger.LogInformation("Delete işlemi başladı");
            var result=await _userService.DeleteUser(id);
            if (result != null)
            {
                _apiResponse.IsSuccess = true;
                _apiResponse.HttpStatusCode = HttpStatusCode.OK;
                _apiResponse.Result = result;
                return _apiResponse;
            }
            _apiResponse.IsSuccess= false;
            _apiResponse.ErrorMessages.Add("Entity bulunamadı");
            _apiResponse.HttpStatusCode=HttpStatusCode.NotFound;
            return _apiResponse;
        }
       // [Authorize(Policy= "Admin rolü ara")]
        [HttpGet("getall-user")]
        public async Task<ApiResponse<GetAllUserDto>> GetAll()
        {
            var _apiResponse=new ApiResponse<GetAllUserDto>();
            _logger.LogInformation("GetAll işlemi başlatıldı");
            var result = await _userService.GetAllUser();
            if (result!=null)
            {
                _apiResponse.IsSuccess = true;
                _apiResponse.HttpStatusCode = HttpStatusCode.OK;
                _apiResponse.Result = result;
                return _apiResponse;
            }
            _apiResponse.IsSuccess= false;
            _apiResponse.ErrorMessages.Add("Herhangi bir users girilmemiş");
            _apiResponse.HttpStatusCode=HttpStatusCode.NotFound;
            return _apiResponse;
            
        }
        [HttpGet("getbyid-user")]
        public async Task<ApiResponse<GetByIdUserDto>> GetById(Guid id)
        {
            var _apiResponse=new ApiResponse<GetByIdUserDto>();
            Expression<Func<AppUser,bool>> filtre=x=>x.Id==id;
            var result = await _userService.GetByIdUser(filtre);
            if (result != null)
            {
                _apiResponse.IsSuccess= true;
               _apiResponse.HttpStatusCode=HttpStatusCode.OK;
                _apiResponse.Result= result;
                return _apiResponse;
            }
            _apiResponse.IsSuccess=false;
            _apiResponse.ErrorMessages.Add("Bu idye sahip kullanıcı yok");
            _apiResponse.HttpStatusCode=HttpStatusCode.NotFound;
            return _apiResponse;

        }
        [HttpPost("refresh-token")]
        public async Task<ApiResponse<LoginResponseModels>> RefreshToken([FromBody] RefreshTokenRequestDtos models)
        {
            var _apiResponse=new ApiResponse<LoginResponseModels>();
            var result=await _userService.RefreshTokenAsync(models);
            _apiResponse.IsSuccess= true;
            _apiResponse.HttpStatusCode=HttpStatusCode.OK;
            _apiResponse.Result= result;
            return _apiResponse;
        }
    }
}
