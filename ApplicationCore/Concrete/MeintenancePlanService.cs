using ApplicationCore.Abstraction;
using ApplicationCore.BaseService;
using ApplicationCore.Dto.MeintenancePlanDtos;
using ApplicationCore.Dto.UserDto;
using AutoMapper;
using Domain.Entites;
using Domain.Identity;
using Domain.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Concrete
{
    public class MeintenancePlanService : BaseService<MeintenancePlan>,IMeintenancePlanService
    {
        private readonly IMapper _mapper;

        public MeintenancePlanService(IWriteRepository<MeintenancePlan> writeRepository,
            IRepository<MeintenancePlan> repository,
            IReadRepository<MeintenancePlan> readRepository,
            IMapper mapper,
            UserManager<AppUser> userManager,
            IHttpContextAccessor httpContextAccessor) : base(writeRepository, mapper, repository, readRepository, userManager, httpContextAccessor)
        {
            _mapper = mapper;
        }
        public async Task<CreateMeintenancePlanDto> CreatePlan(CreateMeintenancePlanDto model,Expression<Func<MeintenancePlan,bool>>?filtre=null,params Expression<Func<MeintenancePlan, object>>[] include)
        { 
            
            var result=await AddAsync(model,null,x=>x.Name==model.Name);
            return _mapper.Map<CreateMeintenancePlanDto>(result);

        }

        public async Task<DeleteMeintenancePlanDto> DeletePlan(Guid id)
        {

                var result = await DeleteAsync(id);
                return _mapper.Map<DeleteMeintenancePlanDto>(result);

        }

        public async Task<List<GetMeintenancePlanDtos>> GetAllPlan()
        {
           var result=await GetAllAsync();
           var model=new List<GetMeintenancePlanDtos>();
            if (result == null)
            {
                throw new DirectoryNotFoundException("Entity bulunamadı");
            }
            foreach(var entity in result)
            {
                var map=_mapper.Map<GetMeintenancePlanDtos>(entity);
                model.Add(map);
            }
            return model;
        }

        public async Task<GetMeintenancePlanDtos> GetIdPlan(Guid id,params Expression<Func<MeintenancePlan, object>>[] includes)
        {
            var result=await GetByIdAsync(x=>x.Id==id, includes);
            return _mapper.Map<GetMeintenancePlanDtos>(result);
        }

        public async Task<UpdateMeintenancePlanDto> UpdatePlan(UpdateMeintenancePlanDto models,Guid id, params Expression<Func<MeintenancePlan, object>>[] includes)
        {
           
            var result=await UpdateAsync(models,x=>x.Id==id,includes);
            return _mapper.Map<UpdateMeintenancePlanDto>(result);
        }
    }
}
