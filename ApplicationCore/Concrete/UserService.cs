using ApplicationCore.Abstraction;
using ApplicationCore.Dto.UserDto;
using ApplicationCore.Responses;
using AutoMapper;
using Domain;
using Domain.Identity;
using Domain.Repository;
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
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Concrete
{
    public class UserService:IUserService
    {
        private readonly IBaseService<AppUser> _baseservice;
        private readonly IReadRepository<AppUser> _readRepository;
        private readonly IReadRepository<AppRole> _roleReadRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly string secretKey;
        private readonly IMapper _automapper;
        public UserService(IBaseService<AppUser> baseservice,IReadRepository<AppUser> readRepository,UserManager<AppUser> userManager,IConfiguration configuration,IMapper mapper,IReadRepository<AppRole> readRepository1)
        {
            _baseservice = baseservice;
            _readRepository = readRepository;
            _userManager = userManager;
            secretKey = configuration.GetValue<string>("SecretKey:jwtKey");
            _automapper = mapper;
            _roleReadRepository = readRepository1;
        }

        public async Task<DeleteUserDtos> DeleteUser(Guid id)
        {

            var result = await _baseservice.DeleteAsync(id);
            return _automapper.Map<DeleteUserDtos>(result);
        }

        public async Task<List<GetAllUserDto>> GetAllUser()
        {
            var result = await _baseservice.GetAllAsync();
            var users=new List<GetAllUserDto>();
            if (result != null)
            {
                foreach(var user in result)
                {
                    var dto=_automapper.Map<GetAllUserDto>(user);
                    users.Add(dto);
                }
                return users;
            }
            throw new ArgumentNullException("Result boş döndü");

        }

        public async Task<GetByIdUserDto> GetByIdUser(Expression<Func<AppUser, bool>> filtre, params Expression<Func<AppUser, object>>[] includes)
        {

            var entity =  await _baseservice.GetByIdAsync(filtre);  
            if (entity != null) 
            { 
                var dto= _automapper.Map<GetByIdUserDto>(entity);
                var role= await _userManager.GetRolesAsync(entity);
                dto.roles = string.Join(",", role);         
                return dto;
            }
            throw new ArgumentNullException("Bu Id de kayıt yok");
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
                LoginResponseModels loginResponseModel = new()
                {
                    Email = user.Email,
                    Token = tokenHandler.WriteToken(token),
                };
                return loginResponseModel;
            }
            throw new Exception();
            }

        public async Task<CreateUserDto> Register(CreateUserDto model,Expression<Func<AppUser,bool>>?method=null)
        {
            var entity = _automapper.Map<AppUser>(model);
            var result =  await _baseservice.AddAsync( entity, model.Password, method);
            var user =_automapper.Map<AppUser>(result);
            if (user != null)
            {
                await _userManager.AddToRoleAsync(user, "user");
            }

            return _automapper.Map<CreateUserDto>(user);
        }
        public async Task<UpdateUserDto> UpdateUser(UpdateUserDto model, Expression<Func<AppUser, bool>> method, Expression<Func<AppUser, object>>[] includes)
        {
            var entity = await _readRepository.GetSingleAsync(method);
            var map=_automapper.Map(model,entity);
            var result = await _baseservice.UpdateAsync(map, method, includes);
            if (result != null) 
            { 
                return _automapper.Map<UpdateUserDto>(result);
            }
            throw new ArgumentNullException();
        }

        
    }
}
