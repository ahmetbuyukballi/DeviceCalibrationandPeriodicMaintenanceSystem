using ApplicationCore.Dto.MeintenancePlanDtos;
using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Abstraction
{
    public interface IMeintenancePlanService
    {
        public Task<CreateMeintenancePlanDto> CreatePlan(CreateMeintenancePlanDto models,Expression<Func<MeintenancePlan,bool>> filtre,params Expression<Func<MeintenancePlan, object>>[] include);
        public Task<UpdateMeintenancePlanDto> UpdatePlan(UpdateMeintenancePlanDto models, Expression<Func<MeintenancePlan, bool>> filtre, params Expression<Func<MeintenancePlan, object>>[] includes);
        public Task<DeleteMeintenancePlanDto> DeletePlan(Guid id);
        public Task<List<GetMeintenancePlanDtos>> GetAllPlan(Expression<Func<MeintenancePlan, bool>> filtre, params Expression<Func<MeintenancePlan, object>>[] includes);
        public Task<GetMeintenancePlanDtos> GetIdPlan(Expression<Func<MeintenancePlan, bool>> filtre, params Expression<Func<MeintenancePlan, object>>[] includes);

    }
}
