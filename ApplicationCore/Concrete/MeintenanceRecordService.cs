using ApplicationCore.Abstraction;
using ApplicationCore.BaseService;
using ApplicationCore.Dto.MeintenanceRecordsDtos;
using AutoMapper;
using Domain.Entites;
using Domain.Identity;
using Domain.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Concrete
{
    public class MeintenanceRecordService : BaseService<MeintenanceRecord>,IMeintenanceRecordService
    {
      
        private readonly IMapper _mapper;
        public MeintenanceRecordService(IWriteRepository<MeintenanceRecord> writeRepository,
            IRepository<MeintenanceRecord> repository,
            IReadRepository<MeintenanceRecord> readRepository,
            IMapper mapper,
            UserManager<AppUser> userManager,
            IHttpContextAccessor httpContextAccessor) : base(writeRepository, mapper, repository, readRepository, userManager, httpContextAccessor)
        {
            _mapper = mapper;
        }
        public async Task<CreateRecordsDtos> CreateRecords(CreateRecordsDtos models, params Expression<Func<MeintenanceRecord, object>>[] includes)
        {
            var result = await AddAsync(models, null, x => x.name == models.name);
            return _mapper.Map<CreateRecordsDtos>(result);
        }

        public async Task<DeleteRecordsDtos> DeleteRecords(Guid id)
        {
            
            var result = await DeleteAsync(id);
            return _mapper.Map<DeleteRecordsDtos>(result);
        }

        public async Task<List<GetRecordsDtos>> GetAllRecords()
        {
            var result = await GetAllAsync();
            var models=new List<GetRecordsDtos>();

                foreach (var record in result)
                {
                    var dto = _mapper.Map<GetRecordsDtos>(record);
                    models.Add(dto);
                }
                return models;
        }

        public async Task<GetRecordsDtos> GetByIdRecords(Guid id, params Expression<Func<MeintenanceRecord, object>>[] includes)
        {
          
            var result = await GetByIdAsync(x=>x.Id==id);
            return _mapper.Map<GetRecordsDtos>(result);

        }

        public async Task<UpdateRecordsDtos> UpdateRecords(UpdateRecordsDtos models, params Expression<Func<MeintenanceRecord, object>>[] includes)
        {
           var result=await UpdateAsync(models,x=>x.Id==models.Id, includes);
           return _mapper.Map<UpdateRecordsDtos>(result);
        }
    }
}
