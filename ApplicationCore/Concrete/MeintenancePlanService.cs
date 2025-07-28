using ApplicationCore.Abstraction;
using ApplicationCore.Dto.MeintenancePlanDtos;
using AutoMapper;
using Domain.Entites;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Concrete
{
    public class MeintenancePlanService : IMeintenancePlanService
    {
        private readonly IMapper _mapper;
        private readonly IBaseService<MeintenancePlan> _baseService;
        private readonly IReadRepository<MeintenancePlan> _readRepository;
        public MeintenancePlanService(IMapper mapper,IBaseService<MeintenancePlan> baseService,IReadRepository<MeintenancePlan> readRepository)
        {
            _baseService = baseService;
            _mapper = mapper;
            _readRepository = readRepository;
        }
        public async Task<CreateMeintenancePlanDto> CreatePlan(CreateMeintenancePlanDto model,Expression<Func<MeintenancePlan,bool>>?filtre=null,params Expression<Func<MeintenancePlan, object>>[] include)
        { 
            var entity=_mapper.Map<MeintenancePlan>(model);
            var result=await _baseService.AddAsync(entity,null,filtre);
            return _mapper.Map<CreateMeintenancePlanDto>(result);

        }

        public async Task<DeleteMeintenancePlanDto> DeletePlan(Guid id)
        {
            var entity = await _readRepository.GetByIdAsync(id);
            if (entity!= null)
            {
                var result = await _baseService.DeleteAsync(id);
                return _mapper.Map<DeleteMeintenancePlanDto>(result);
            }
            return null;
        }

        public async Task<List<GetMeintenancePlanDtos>> GetAllPlan()
        {
           var result=await _baseService.GetAllAsync();
            var model=new List<GetMeintenancePlanDtos>();
            foreach(var entity in result)
            {
                var map=_mapper.Map<GetMeintenancePlanDtos>(entity);
                model.Add(map);
            }
            return model;
        }

        public async Task<GetMeintenancePlanDtos> GetIdPlan(Guid id,params Expression<Func<MeintenancePlan, object>>[] includes)
        {
            Expression<Func<MeintenancePlan, bool>> filtre = x => x.Id == id;
            if (filtre == null)
            {
                return null;
            }
            var result=await _baseService.GetByIdAsync(filtre, includes);
            return _mapper.Map<GetMeintenancePlanDtos>(result);
        }

        public async Task<UpdateMeintenancePlanDto> UpdatePlan(UpdateMeintenancePlanDto models,Guid id, params Expression<Func<MeintenancePlan, object>>[] includes)
        {
            Expression<Func<MeintenancePlan,bool>> filtre=x=>x.Id==id;
            var entity=await _readRepository.GetSingleAsync(filtre);
            if (entity == null) return null;
            var model=_mapper.Map(models,entity);
            var result=await _baseService.UpdateAsync(model,filtre, includes);
            return _mapper.Map<UpdateMeintenancePlanDto>(result);
        }
    }
}
