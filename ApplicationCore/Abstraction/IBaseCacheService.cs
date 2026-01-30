using ApplicationCore.BaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Abstraction
{
    public interface IBaseCacheService<TModel> where TModel:class
    {
          Task<bool> SetCache(string key, string value);
          Task<TModel> GetOrAddCache(Guid? id,string key, Func<Task<TModel>> func);
         Task <bool> RemoveCache(string key);
    }
}
