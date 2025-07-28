using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Dto.DevicesDto
{
    public class UpdateDeviceDto
    {

        public string Name { get; set; }
        public string Model { get; set; }
        public DateTime PurchaseDate { get; set; }
        public Guid serialNo { get; set; }

    }
}
