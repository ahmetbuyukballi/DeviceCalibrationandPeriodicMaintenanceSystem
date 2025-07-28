using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Dto.MeintenancePlanDtos
{
    public class UpdateMeintenancePlanDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int intervalDays { get; set; }
        public bool IsActive { get; set; }
    }
}
