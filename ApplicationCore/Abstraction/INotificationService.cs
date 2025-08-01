using ApplicationCore.Dto.MailServiceDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Abstraction
{
    public interface INotificationService
    {
        public Task<MailDto> CreateNotification(MailDto models);
    }
}
