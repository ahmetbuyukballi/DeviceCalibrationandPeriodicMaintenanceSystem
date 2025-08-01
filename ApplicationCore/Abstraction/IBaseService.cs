using Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Abstraction
{
    public interface IBaseService<TModel>
        where TModel : class
    {

        Task<List<TModel>> GetAllAsync(Expression<Func<TModel, bool>>? filter = null,
                                        params Expression<Func<TModel, object>>[] includes);
        Task<TModel> GetByIdAsync(Expression<Func<TModel, bool>> filter,
                                        params Expression<Func<TModel, object>>[] includes);
        Task<TModel> AddAsync<TDto>(TDto model,string? password, Expression<Func<TModel, bool>> method, params Expression<Func<TModel, object>>[] references);
        Task<TModel?> UpdateAsync<TDto>(TDto model,Expression<Func<TModel, bool>> filter,
                                 Expression<Func<TModel, object>>[] references);
        Task<TModel> DeleteAsync(Guid id);

        Task<TModel> GetIncludeAsync(string toInclude,Expression<Func<TModel,bool>> filter,params Expression<Func<TModel, object>>[] references);
    }
}
