using ApplicationCore.Dto.UserDto;
using ApplicationCore.Responses;
using Domain;
using Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Abstraction
{
    public interface IUserService
    {
      
        public  Task<CreateUserDto> Register(CreateUserDto model, Expression<Func<AppUser, bool>>?method=null);
        public Task<LoginResponseModels> Login(LoginUserDto model);
        public Task<UpdateUserDto> UpdateUser(UpdateUserDto model,Expression<Func<AppUser, bool>>method,params Expression<Func<AppUser, object>>[] includes);
        public Task<DeleteUserDtos> DeleteUser(Guid id);
        public Task<List<GetAllUserDto>> GetAllUser();
        public Task<GetByIdUserDto> GetByIdUser(Expression<Func<AppUser,bool>> filtre,params Expression<Func<AppUser,object>>[] includes);
    }
}
