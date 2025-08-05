using Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class FeedBack:EntityBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid? UserId { get; set; }
        [ForeignKey("UserId")]
        public AppUser AppUsers { get; set; }
       
        public Guid? DeviceId { get; set; }
        [ForeignKey("DeviceId")]
        public Devices devices { get; set; }

    }
}
