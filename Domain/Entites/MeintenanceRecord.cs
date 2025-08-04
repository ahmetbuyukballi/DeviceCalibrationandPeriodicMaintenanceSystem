using Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class MeintenanceRecord:EntityBase
    {
        public string name { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public int Intervaldays { get; set; }
        public DateTime StartMeintenceDay { get; set; }
        public DateTime LastMaintenceDay { get; set; }
        public Guid? DevicesId { get; set; }
        [ForeignKey("DevicesId")]
        public Devices devices { get; set; }   
        public Guid? UserId { get; set; }
        [ForeignKey("UserId")]
        public AppUser appUser { get; set; }
        public ICollection<NotificationLog> notificationLogs { get; set; }

    }
}
