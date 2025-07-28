using ApplicationCore.Abstraction;
using ApplicationCore.Dto.UserDto;
using AutoMapper;
using Domain;
using Domain.Identity;
using Domain.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.BaseService
{
    public class BaseService<TModel> : IBaseService<TModel>
        where TModel : class
    {
        private readonly IWriteRepository<TModel> _writeRepository;
        private readonly IRepository<TModel> _repository;
        private readonly IMapper _autpMapper;
        private readonly IReadRepository<TModel> _readRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpcontextAccessor;

        public BaseService(IWriteRepository<TModel> writeRepository,IMapper mapper,IRepository<TModel> repository,IReadRepository<TModel> readRepository,UserManager<AppUser> userManager,IHttpContextAccessor httpContextAccessor) 
        { 
            _writeRepository = writeRepository;
            _autpMapper = mapper;
            _repository = repository;
            _readRepository = readRepository;
            _userManager = userManager;
            _httpcontextAccessor = httpContextAccessor;
        }


        public async Task<TModel> AddAsync(TModel model ,string? password, Expression<Func<TModel,bool>>?method=null,params Expression<Func<TModel, object>>[] references)
        {
            if (model.GetType() == typeof(AppUser))
            {
                var user = await _userManager.CreateAsync((AppUser)(object)model, password);
                return model;
            }

            await LoadReference(method, references);
            await _writeRepository.AddAsync(model);
            await _writeRepository.SaveAsync();
            return model;

            
        }

        public async Task<TModel> DeleteAsync(Guid id)
        {
                var entity = await _readRepository.GetByIdAsync(id);
                await _writeRepository.DeleteByIdAsync(id);
                await _writeRepository.SaveAsync();
                return entity;
        }

        public async Task<List<TModel>> GetAllAsync(Expression<Func<TModel, bool>>? filter = null, params Expression<Func<TModel, object>>[] includes)
        {
            IQueryable<TModel> query = _readRepository.GetAll();    
            query=ApplyIncludes(query, includes);
            if (filter != null)
            {
                query=query.Where(filter);
               
            }
            var list=await query.ToListAsync();
            return _autpMapper.Map<List<TModel>>(list);
        }

        public async Task<TModel> GetByIdAsync(Expression<Func<TModel, bool>> filter, params Expression<Func<TModel, object>>[] includes)
        {
            IQueryable<TModel> query = _readRepository.GetAll();
            query=ApplyIncludes(query, includes);
            var entity =  await query.FirstOrDefaultAsync(filter);
            return entity;
        }

        public async Task<TModel?> UpdateAsync(TModel model, Expression<Func<TModel, bool>> filter,Expression<Func<TModel, object>>[] references)
        {
            if (model == null) return null;
            await LoadReference(filter, references);
            await _writeRepository.UpdateAsync(model);
            await _writeRepository.SaveAsync();
            return model;

        }

        private async Task LoadReference(Expression<Func<TModel,bool>> method, params Expression<Func<TModel, object>>[] references)
        {
            foreach(var reference in references)
            {
                var entry = await _writeRepository.EntiyEntry(method);
                var memberName = GetPropertyName(reference);
                if (!entry.References.Any(x => x.Metadata.Name == memberName)){
                    await entry.Reference(reference).LoadAsync();
                }
            }
        }
        private static string GetPropertyName(Expression<Func<TModel, object>> expression)
        {
            if (expression.Body is MemberExpression member)
                return member.Member.Name;

            if (expression.Body is UnaryExpression unary && unary.Operand is MemberExpression unaryMember)
                return unaryMember.Member.Name;

            throw new InvalidOperationException("Invalid reference expression");
        }
        private IQueryable<TModel> ApplyIncludes(IQueryable<TModel> query, Expression<Func<TModel, object>>[] includes)
        {
            foreach(var include in includes)
            {
                query=query.Include(include);
            }
            return query;
        }

        public async Task<TModel> GetIncludeAsync(string toInclude, Expression<Func<TModel, bool>> filter, params Expression<Func<TModel, object>>[] references)
        {
            IQueryable<TModel> query =  _readRepository.GetAll();
            query=ApplyIncludes(query, references);
            if (filter != null)
            {
               var result=await query.Include(toInclude).FirstOrDefaultAsync(filter);
                return result;
            }

            throw new InvalidOperationException();
        }
    }

}
