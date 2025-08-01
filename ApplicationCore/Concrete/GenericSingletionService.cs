using ApplicationCore.Abstraction;
using ApplicationCore.BaseService;
using AutoMapper;
using Domain.Identity;
using Domain.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Concrete
{
    public class GenericSingletionService<TModel>:BaseService<TModel> where TModel : class
    {
        public GenericSingletionService(IWriteRepository<TModel> writeRepository,
            IRepository<TModel> repository,
            IReadRepository<TModel> readRepository,
            IMapper mapper,
            UserManager<AppUser> userManager,
            IHttpContextAccessor httpContextAccessor) : base(writeRepository, mapper, repository, readRepository, userManager, httpContextAccessor)
        {

        }
    }
}
