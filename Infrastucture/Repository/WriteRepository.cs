using Domain;
using Domain.Repository;
using Infrastucture.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastucture.Repository
{
    public class WriteRepository<T> : IWriteRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        public WriteRepository(AppDbContext context)
        {
            _context = context;
        }
        public DbSet<T> Table => _context.Set<T>();
        public async Task<bool> AddAsync(T model)
        {
            EntityEntry entity=await Table.AddAsync(model);
            return entity.State==EntityState.Added;
        }
        
        public async Task<bool> AddRangeAsync(List<T> models)
        {
           await Table.AddRangeAsync(models);
            return true;
        }


        public  bool DeleteAsync(T model)
        {
            EntityEntry entity =  Table.Remove(model);
            return entity.State==EntityState.Deleted;
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            var user = await Table.FindAsync(id);
            if (user == null) return false;
            EntityEntry<T> entity =  Table.Remove(user);
            return entity.State==EntityState.Deleted;
            
        }

        public  bool DeleteRangeAsync(IEnumerable<T> models)
        {
             Table.RemoveRange(models);
            return true;
        }

        public async Task<EntityEntry<T>> EntiyEntry(Expression<Func<T,bool>> method)
        {
            var entity = await Table.FirstOrDefaultAsync(method);
            if (entity != null)
            {
                EntityEntry<T> entityEntry=Table.Entry(entity);
                return entityEntry;
            }
            return null;
            
        }


        public async Task<int> SaveAsync() => await _context.SaveChangesAsync();
        

        public async Task<bool> UpdateAsync(T model)
        {
            EntityEntry<T> entity=Table.Update(model);
            return entity.State== EntityState.Modified;
        }
    }
}
