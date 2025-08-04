using Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class NotificationLog:EntityBase
    {
        public string Content { get; set; }
        public bool IsSend { get; set; }
        public string Type { get; set; }
        public ICollection<AppUser> AppUsers { get; set; }
        public Guid? DevicesId { get; set; }
        [ForeignKey("DevicesId")]
        public Devices devices { get; set; }
        public Guid? MeintenanceRecordId { get; set; }
        [ForeignKey("MeintenanceRecordId")]
        public MeintenanceRecord meintenanceRecord { get; set; }
    }
}
