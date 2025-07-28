using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IWriteRepository<T>:IRepository<T> where T : class
    {
        public Task<EntityEntry<T>> EntiyEntry(Expression<Func<T, bool>> method);
        public Task<bool> AddAsync(T model);
        public Task<bool> AddRangeAsync(List<T> models);
        public Task<bool> UpdateAsync(T model);
        bool DeleteAsync(T model);
        Task<bool> DeleteByIdAsync(Guid id);
        bool DeleteRangeAsync(IEnumerable<T> models);
        Task<int> SaveAsync();

    }
}
