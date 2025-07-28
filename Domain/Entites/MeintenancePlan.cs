using Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class MeintenancePlan:EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int intervalDays { get; set; }
        public bool IsActive { get; set; }
        public Guid? DevicesId { get; set; }
        public Devices devices { get; set; }
      
    }
}
