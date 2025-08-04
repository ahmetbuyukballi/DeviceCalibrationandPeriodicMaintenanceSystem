using ApplicationCore.Abstraction;
using ApplicationCore.Dto.UserDto;
using AutoMapper;
using Domain;
using Domain.Entites;
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
    public abstract class BaseService<TModel> :IBaseService<TModel>
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

   

        public virtual async Task<TModel> AddAsync<TDto>(TDto model ,string? password, Expression<Func<TModel,bool>>?method=null,params Expression<Func<TModel, object>>?[] references)
        {
           Console.WriteLine("Entitiy bu:"+model.GetType().Name);
           var entity= _autpMapper.Map<TModel>(model);
            if (entity.GetType() == typeof(AppUser))
            {
                var user = await _userManager.CreateAsync((AppUser)(object)entity,password);
                return entity;
            }
            if (method != null || references != null) 
            {
                await LoadReference(method, references);
            }

          if(await _readRepository.GetSingleAsync(method)==null)
            {
                var user=await _writeRepository.AddAsync(entity);
                await _writeRepository.SaveAsync();
                return entity;
            }

            throw new InvalidOperationException("Bu veri veritabanında mevcut");
            
        }

        public virtual async Task<TModel> DeleteAsync(Guid id)
        {
             var entity = await _readRepository.GetByIdAsync(id);
            if (entity != null)
            {
                await _writeRepository.DeleteByIdAsync(id);
                await _writeRepository.SaveAsync();
                return entity;
            }
            throw new DirectoryNotFoundException("Bu veri elimizde yok");
        }

        public virtual async Task<List<TModel>> GetAllAsync(Expression<Func<TModel, bool>>? filter = null, params Expression<Func<TModel, object>>[] includes)
        {
            IQueryable<TModel> query = _readRepository.GetAll();
            if (query!=null)
            {
                query = ApplyIncludes(query, includes);
                if (filter != null)
                {
                    query = query.Where(filter);

                }
                var list = await query.ToListAsync();
                return _autpMapper.Map<List<TModel>>(list);
            }
            throw new DirectoryNotFoundException("Bu veri elimizde yok");
        }

        public virtual async Task<TModel> GetByIdAsync(Expression<Func<TModel, bool>> filter, params Expression<Func<TModel, object>>[] includes)
        {
            IQueryable<TModel> query = _readRepository.GetAll();
            query=ApplyIncludes(query, includes);
            var entity =  await query.FirstOrDefaultAsync(filter);
            if (entity != null)
            {
                return entity;
            }
            throw new DirectoryNotFoundException("Bu veri elimizde yok");
        }

        public virtual async Task<TModel?> UpdateAsync<TDto>(TDto model, Expression<Func<TModel, bool>> filter,Expression<Func<TModel, object>>[] references)
        {
            var entity = await _readRepository.GetSingleAsync(filter);
            if (entity!=null)
            {
                var result=_autpMapper.Map(model, entity);
                await LoadReference(filter, references);
                await _writeRepository.UpdateAsync(result);
                await _writeRepository.SaveAsync();
                return result;
            }
            throw new DirectoryNotFoundException("Bu veri elimizde yok");
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
        protected Guid GetUserId()
        {
            var IdStr = _httpcontextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.TryParse(IdStr, out var id) ? id : Guid.Empty;
        }
    }

}
