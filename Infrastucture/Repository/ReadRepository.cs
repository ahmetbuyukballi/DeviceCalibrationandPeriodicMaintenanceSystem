using Domain;
using Domain.Repository;
using Infrastucture.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastucture.Repository
{
    public class ReadRepository<T> : IReadRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        public ReadRepository(AppDbContext context) 
        { 
            _context = context;
        }
        public DbSet<T> Table => _context.Set<T>();
        public IQueryable<T> GetAll() => Table;


        public async Task<T> GetByIdAsync(Guid id) => await Table.FindAsync(id);

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method) => await Table.FirstOrDefaultAsync(method);
        

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method)=>Table.Where(method);
       
    }
}
