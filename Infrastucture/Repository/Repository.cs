using Domain;
using Domain.Repository;
using Infrastucture.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastucture.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        public Repository(AppDbContext appDbContext)
        {
            _context= appDbContext;
        }
        public DbSet<T> Table => _context.Set<T>();
    }
}
