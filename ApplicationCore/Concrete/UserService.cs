using ApplicationCore.Abstraction;
using ApplicationCore.BaseService;
using ApplicationCore.Dto.Token;
using ApplicationCore.Dto.UserDto;
using ApplicationCore.Responses;
using AutoMapper;
using Domain;
using Domain.Identity;
using Domain.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Concrete
{
    public class UserService:BaseService<AppUser>,IUserService
    {
        
        private readonly IReadRepository<AppUser> _readRepository;
        private readonly IReadRepository<AppRole> _roleReadRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly string secretKey;
        private readonly IMapper _automapper;
        public UserService(IWriteRepository<AppUser>writeRepository,
            IRepository<AppUser> repository,
            IReadRepository<AppUser> readRepository,
            IMapper mapper,
            UserManager<AppUser> userManager,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration,
            IReadRepository<AppRole> readRepository1):base(writeRepository, mapper, repository,readRepository,userManager,httpContextAccessor)
        {
            _readRepository = readRepository;
            _userManager = userManager;
            secretKey = configuration.GetValue<string>("SecretKey:jwtKey");
            _automapper = mapper;
            _roleReadRepository = readRepository1;
        }

        public async Task<DeleteUserDtos> DeleteUser(Guid id)
        {
            var result = await DeleteAsync(id);
           return _automapper.Map<DeleteUserDtos>(result);
        }

        public async Task<List<GetAllUserDto>> GetAllUser()
        {
            var result = await GetAllAsync();
            var users=new List<GetAllUserDto>();

                foreach(var user in result)
                {
                    var dto=_automapper.Map<GetAllUserDto>(user);
                    users.Add(dto);
                }
                return users;
        }

        public async Task<GetByIdUserDto> GetByIdUser(Expression<Func<AppUser, bool>> filtre, params Expression<Func<AppUser, object>>[] includes)
        {

            var entity =  await GetByIdAsync(filtre);  
            return _automapper.Map<GetByIdUserDto>(entity);
        }

        public async Task<LoginResponseModels> Login(LoginUserDto model)
        {
            var user =  await _readRepository.GetSingleAsync(x => x.Email == model.UserName);
            if (user != null)
            {
                bool IsValid = await _userManager.CheckPasswordAsync(user, model.Password);
                if (!IsValid)
                {
                    throw new UnauthorizedAccessException("Kullanıcı bilgisi yanlış");
                }
                var role = await _userManager.GetRolesAsync(user);
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                byte[] key = Encoding.ASCII.GetBytes(secretKey);
                SecurityTokenDescriptor securityTokenDescriptor = new()
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                        new Claim(ClaimTypes.Email,user.Email),
                        new Claim(ClaimTypes.Role,role.FirstOrDefault()),
                    }),

                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)

                };
                SecurityToken token = tokenHandler.CreateToken(securityTokenDescriptor);

                var refreshToken=Guid.NewGuid().ToString();
                var encyrptRefresh=
                await _userManager.RemoveAuthenticationTokenAsync(user, "MyApp", "RefreshToken");
                await _userManager.SetAuthenticationTokenAsync(user,"MyApp","RefreshToken",refreshToken);

                LoginResponseModels loginResponseModel = new()
                {
                    Email = user.Email,
                    Token = tokenHandler.WriteToken(token),
                    RefreshToken = refreshToken
                };
                return loginResponseModel;
            }
            throw new Exception();
            }

        public async Task<CreateUserDto> Register(CreateUserDto model)
        {
          
                var result = await AddAsync(model, model.Password,x=>x.UserName==model.userName);
                if (result != null)
                {
                    await _userManager.AddToRoleAsync(result, "user");
                }

                return _automapper.Map<CreateUserDto>(result);
          
            
        }
        public async Task<UpdateUserDto> UpdateUser(UpdateUserDto model, Expression<Func<AppUser, bool>> method, Expression<Func<AppUser, object>>[] includes)
        {

            var result = await UpdateAsync(model, x=>x.Id==model.Id, includes);
            return _automapper.Map<UpdateUserDto>(result);


        }
        
        public async Task<RefreshTokenResponseDtos> RefreshTokenAsync(RefreshTokenRequestDtos models)
        {
            var principal=GetPrincipalFromExpiredToken(models.AccessToken);
            var userid=principal.FindFirstValue(ClaimTypes.NameIdentifier);
            var user =await _userManager.FindByIdAsync(userid);

            if (user == null) throw new UnauthorizedAccessException();

            var savedRefreshToken = await _userManager.GetAuthenticationTokenAsync(user, "MyApp", "RefreshToken");
            if (savedRefreshToken != models.RefreshToken) throw new UnauthorizedAccessException("Refresh token geçersiz");
            
            var role=await _userManager.GetRolesAsync(user);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key=Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                    new Claim(ClaimTypes.Email,user.Email),
                    new Claim(ClaimTypes.Role,role.FirstOrDefault())
                }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials=new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256)
            };

            var newAccessToken = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
            var newRefreshToken=Guid.NewGuid().ToString();

            await _userManager.RemoveAuthenticationTokenAsync(user, "MyApp", "RefreshToken");
            await _userManager.SetAuthenticationTokenAsync(user,"MyApp","RefreshToken",newRefreshToken); ;

            return new RefreshTokenResponseDtos
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }
        protected ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

            if (securityToken is not JwtSecurityToken jwtToken ||
                !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }

    }
}
