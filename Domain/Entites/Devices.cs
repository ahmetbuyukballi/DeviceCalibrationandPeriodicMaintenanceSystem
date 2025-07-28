using Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class Devices:EntityBase
    {
        public string Name { get; set; }
        public string Model {  get; set; }
        public DateTime PurchaseDate { get; set; }
        public Guid? serialNo { get; set; }

        public Guid? UserId { get; set; }
        [ForeignKey("UserId")]
        public AppUser AppUsers { get; set; }

        public ICollection<MeintenancePlan> meintenancePlan { get; set; }
        public ICollection<MeintenanceRecord> meintenanceRecords { get; set; }
        public ICollection<NotificationLog> NotificationLogs { get; set; }
        public ICollection<FeedBack> feedBack { get; set; }  


        
    }
}
