using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Dto.NotificationLogDto
{
    public class CreateNotificationLogDtos
    {
        public string Content { get; set; }
        public bool IsSend { get; set; } = true;
        public string Type { get; set; }
        public Guid? devicesId { get; set; }
        public Guid? MeintenanceRecordId { get; set; }
    }
}
