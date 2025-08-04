using Domain.Entites;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Identity
{
    public class AppUser:IdentityUser<Guid>
    { 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? LastModifiedBy { get; set; }
        public ICollection<NotificationLog> NotificationLog { get; set; }
        public ICollection<FeedBack> FeedBacks { get; set; }
        public ICollection<MeintenanceRecord> MeintenanceRecords { get; set; }
        public ICollection<Devices> Devices { get; set; }
     
    }
}
