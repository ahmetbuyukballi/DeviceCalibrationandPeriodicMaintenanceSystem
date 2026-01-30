using ApplicationCore.Abstraction;
using AutoMapper;
using Domain.Identity;
using Domain.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDatabase = StackExchange.Redis.IDatabase;

namespace ApplicationCore.BaseService
{
    public class BaseCacheService<TModel>: BaseService<TModel>//IBaseCacheService<TModel> 
        where TModel:class 
    {
        private readonly IDatabase _dataBase;
        private readonly IConnectionMultiplexer _redisCon;
        public BaseCacheService(IDatabase database,
            IConnectionMultiplexer connectionMultiplexer,
            IWriteRepository<TModel> writeRepository,
            IMapper mapper,
            IRepository<TModel> repository,
            IReadRepository<TModel> readRepository,
            UserManager<AppUser> userManager,
            IHttpContextAccessor httpContextAccessor):base(writeRepository,mapper,repository,readRepository,userManager,httpContextAccessor)
        {
            _dataBase = database;
            _redisCon = connectionMultiplexer;
        }


        /*public async Task<TModel> GetOrAddCache(Guid? id,string? key,Func<Task<TModel>> func)
        {
            var cacheKey=$"{typeof(TModel).Name}.{key}";
            var result = await _dataBase.StringGetAsync(cacheKey);
            if (result.IsNull)
            {
                if (id == Guid.Empty)
                {
                    var list = await GetAllAsync();
                    foreach (var item in list) 
                    { 
                        
                    }
                }
                
            }
           
        }*/

        public Task<bool> RemoveCache(string key)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetCache(string key, string value)
        {
            throw new NotImplementedException();
        }
    }
}
