using ApplicationCore.Abstraction;
using ApplicationCore.BaseService;
using ApplicationCore.Dto.FeedBackDtos;
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
    public class FeedBackService : BaseService<FeedBack>,IFeedBackService
    {
        
        private readonly IMapper _mapper;
        private readonly GetClaimsBaseService _getClaimsBaseService;

        public FeedBackService(IWriteRepository<FeedBack> writeRepository,
            IRepository<FeedBack> repository,
            IReadRepository<FeedBack> readRepository,
            IMapper mapper,
            UserManager<AppUser> userManager,
            IHttpContextAccessor httpContextAccessor,
            GetClaimsBaseService getClaimsBaseService):base(writeRepository, mapper, repository, readRepository, userManager, httpContextAccessor)
        {
          _mapper = mapper;
            _getClaimsBaseService = getClaimsBaseService;
        }

        public async Task<CreateFeedBackDto> CreateFeedBack(CreateFeedBackDto models, params Expression<Func<FeedBack, object>>[] includes)
        {
            var entity=_mapper.Map<FeedBack>(models); 
            entity.UserId=_getClaimsBaseService.GetUserId();
            var result = await AddAsync(entity,null,null);
            return _mapper.Map<CreateFeedBackDto>(result);
        }

        public async Task<DeleteFeedBackDto> DeleteFeedBack(Guid id)
        {
            var result = await DeleteAsync(id);
            return _mapper.Map<DeleteFeedBackDto>(result);
        }

        public async Task<List<GetFeedBackDto>> GetAllFeedBack(Expression<Func<FeedBack,bool>>? filter=null,params Expression<Func<FeedBack, object>>[] includes)
        {
            var result=await GetAllAsync();
            var feedBacks=new List<GetFeedBackDto>();
            foreach (var item in result)
            { 
                var dto=_mapper.Map<GetFeedBackDto>(item);
                feedBacks.Add(dto);

            }
            return feedBacks;
        }

        public async Task<GetFeedBackDto> GetByIdFeedBack(Guid id)
        {
            var result = await GetByIdAsync(x => x.Id == id);
            return _mapper.Map<GetFeedBackDto>(result);
           
        }

        public async Task<UpdateFeedBackDto> UpdateFeedback(UpdateFeedBackDto models, params Expression<Func<FeedBack, object>>[] includes)
        {
           var result = await UpdateAsync(models,x=>x.Id==models.Id,includes);
           return _mapper.Map<UpdateFeedBackDto>(result);

        }
    }
}
