using ApplicationCore.Dto.MeintenanceRecordsDtos;
using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Abstraction
{
    public interface IMeintenanceRecordService
    {
        public Task<CreateRecordsDtos> CreateRecords(CreateRecordsDtos models, params Expression<Func<MeintenanceRecord, object>>[] includes);
        public Task<UpdateRecordsDtos> UpdateRecords(UpdateRecordsDtos models, params Expression<Func<MeintenanceRecord, object>>[] includes);
        public Task<DeleteRecordsDtos> DeleteRecords(Guid id);
        public Task<List<GetRecordsDtos>> GetAllRecords();
        public Task<GetRecordsDtos> GetByIdRecords(Guid id, params Expression<Func<MeintenanceRecord, object>>[] includes);
    }
}
