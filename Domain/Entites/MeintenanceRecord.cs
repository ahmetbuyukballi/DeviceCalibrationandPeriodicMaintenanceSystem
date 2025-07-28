using Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class MeintenanceRecord:EntityBase
    {
        public string name { get; set; }
        public string description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime startMeintenceDay { get; set; }
        public DateTime lastMaintenceDay { get; set; }
        public Guid? devicesId { get; set; }
        public Devices devices { get; set; }   
        public Guid? userId { get; set; }
        public AppUser appUser { get; set; }
        public ICollection<NotificationLog> notificationLogs { get; set; }

    }
}
