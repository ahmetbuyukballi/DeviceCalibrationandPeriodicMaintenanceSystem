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
using OfficeOpenXml;
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
        private readonly GetClaimsBaseService _getClaimsBaseService;
        public MeintenanceRecordService(IWriteRepository<MeintenanceRecord> writeRepository,
            IRepository<MeintenanceRecord> repository,
            IReadRepository<MeintenanceRecord> readRepository,
            IMapper mapper,
            UserManager<AppUser> userManager,
            IHttpContextAccessor httpContextAccessor,
            GetClaimsBaseService getClaimsBaseService) : base(writeRepository, mapper, repository, readRepository, userManager, httpContextAccessor)
        {
            _mapper = mapper;
            _getClaimsBaseService= getClaimsBaseService;
        }
        public async Task<CreateRecordsDtos> CreateRecords(CreateRecordsDtos models, params Expression<Func<MeintenanceRecord, object>>[] includes)
        {
            var result = await AddAsync(models, null, x => x.name == models.name);
            result.UserId=_getClaimsBaseService.GetUserId();
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

        public async Task<MemoryStream> GetRecordsExcel(Guid id)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var result = await GetAllAsync(x => x.UserId == id);
            var dto = _mapper.Map<List<GetRecordsDtos>>(result);

            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Cihazın Bakım geçmişi");

            worksheet.Cells[1, 1].Value = "Bakım Adı";
            worksheet.Cells[1, 2].Value = "Bakım Açıklaması";
            worksheet.Cells[1, 3].Value = "Bakım Rutini";
            worksheet.Cells[1, 4].Value = "Bakım Başlangıç Günü";
            worksheet.Cells[1, 5].Value = "Bakım Bitiş Günü";
            worksheet.Cells[1, 6].Value = "Cihaz Adı";
            worksheet.Cells[1, 7].Value = "Kullanıcı Adı";


            foreach (var entity in result)
            {
                var device = entity.devices;
                var userName = entity.appUser.UserName;
                for (int i = 0; i < result.Count; i++)
                {
                    worksheet.Cells[i + 2, 1].Value = entity.name;
                    worksheet.Cells[i + 2, 2].Value = entity.Description;
                    worksheet.Cells[i + 2, 3].Value = entity.Intervaldays;
                    worksheet.Cells[i + 2, 4].Value = entity.StartMeintenceDay;
                    worksheet.Cells[i + 2, 5].Value = entity.LastMaintenceDay;
                    worksheet.Cells[i + 2, 6].Value = device.Name;
                    worksheet.Cells[i + 2, 7].Value = userName;
                }
            }
            var stream = new MemoryStream();
            package.SaveAs(stream);
            stream.Position = 0;

            return stream;
        }
    }
}
