using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Dto.MeintenanceRecordsDtos
{
    public class GetRecordsDtos
    {
        public string name { get; set; }
        public string description { get; set; }
        public bool IsCompleted { get; set; }
        public int intervaldays { get; set; }
        public DateTime startMeintenceDay { get; set; }
        public DateTime lastMaintenceDay { get; set; }
        public Guid? devicesId { get; set; }
        public Guid? userId { get; set; }
    }
}
