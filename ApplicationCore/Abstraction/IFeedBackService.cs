using ApplicationCore.Dto.FeedBackDtos;
using ApplicationCore.Dto.UserDto;
using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Abstraction
{
    public interface IFeedBackService
    {
        public  Task<CreateFeedBackDto> CreateFeedBack(CreateFeedBackDto models,params Expression<Func<FeedBack, object>>[] includes);
        public Task<UpdateFeedBackDto> UpdateFeedback(UpdateFeedBackDto models, params Expression<Func<FeedBack, object>>[] includes);
        public Task<DeleteFeedBackDto> DeleteFeedBack(Guid id);
        public Task<List<GetFeedBackDto>> GetAllFeedBack();
        public Task<GetFeedBackDto> GetByIdFeedBack(Guid id);
    }
}
