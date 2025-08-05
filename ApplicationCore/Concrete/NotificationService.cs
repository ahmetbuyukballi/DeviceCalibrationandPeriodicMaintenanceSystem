using ApplicationCore.Abstraction;
using ApplicationCore.BaseService;
using ApplicationCore.Dto.MailServiceDto;
using ApplicationCore.Dto.NotificationLogDto;
using ApplicationCore.MailService;
using AutoMapper;
using Domain.Entites;
using Domain.Identity;
using Domain.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Concrete
{
    public class NotificationService : BackgroundService
    {
        private readonly TimeOnly _runTime = new(10,25);
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;
        public NotificationService(IServiceScopeFactory serviceScopeFactory,IMapper mapper)
        {
           _scopeFactory = serviceScopeFactory;
            _mapper = mapper;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var now = TimeOnly.FromDateTime(DateTime.Now);
                if (now.Hour == _runTime.Hour && now.Minute == _runTime.Minute)
                {
                    using (var scope = _scopeFactory.CreateScope())
                    {
                        var _baseService = scope.ServiceProvider.GetRequiredService<IBaseService<AppUser>>();//ibaseservice kullanmam baseservice bir abstract class olduğu için scope olmuyor tamam mı ben de baseservice:ibaseservice yaptım sonra da bir class oluşturdum ve o classa baseservice verdim ve sonra dedim ibaseservicei çağırınca o class gelsin ve olayı çözdüm.
                        var _mailService = scope.ServiceProvider.GetRequiredService<MailService.MailService>();
                        var _NotificationbaseService = scope.ServiceProvider.GetRequiredService<IBaseService<NotificationLog>>();
                        var users = await _baseService.GetAllAsync();
                        foreach (var item in users)
                        {
                            var record = await _baseService.GetByIdAsync(x => x.Id == item.Id, x => x.MeintenanceRecords);
                            foreach (var models in record.MeintenanceRecords)
                            {
                                var time = DateTime.Now - models.LastMaintenceDay;
                                var result = models.Intervaldays - (int)time.TotalDays;
                                if (result == 5)
                                {
                                    if (!string.IsNullOrWhiteSpace(record.Email))
                                    {
                                        await _mailService.SendEmailAsync(record.Email, $"{models.name} isimli bakım planı hakkında", "Bakımınıza 5 gün kalmıştır.İyi günler dileriz");
                                        var model = new CreateNotificationLogDtos
                                        {
                                            Content = $"{models.name} isimli bakım planı hakkında.Bakımınıza 5 gün kalmıştır.İyi günler dileriz",
                                            Type = "Gmail",
                                            devicesId = models.DevicesId,
                                            MeintenanceRecordId = models.Id
                                        };
                                        var notificationLg = _mapper.Map<NotificationLog>(model);
                                        var notResult = await _NotificationbaseService.AddAsync(notificationLg, null, null, null);
                                    }

                                }
                                else if (result < 0)
                                {
                                    if (!string.IsNullOrWhiteSpace(record.Email))
                                    {
                                        await _mailService.SendEmailAsync(record.Email, $"{models.name} isimli bakım planı hakkında", "Bakım gününüz geçmiştir.Lütfen en kısa sürede cihazınızı bakıma getiriniz.İyi günler dileriz");
                                        var model = new CreateNotificationLogDtos
                                        {
                                            Content = $"{models.name} isimli bakım planı hakkında.Bakım gününüz geçmiştir.Lütfen en kısa sürede cihazınızı bakıma getiriniz.İyi günler dileriz",
                                            Type = "Gmail",
                                            devicesId = models.DevicesId,
                                            MeintenanceRecordId = models.Id
                                        };
                                        var notificationLg = _mapper.Map<NotificationLog>(model);
                                        var notResult = await _NotificationbaseService.AddAsync(notificationLg, null, x => x.CreatedTime == DateTime.Now);
                                    }

                                }
                            }
                        }
                    }
                    await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
                }
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }

        }
    }
}
