using ApplicationCore.Abstraction;
using ApplicationCore.BaseService;
using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApplicationCore.Dto.DevicesDto
{
    
    public class CreateDeviceDto
    {
        public string Name { get; set; }
        public string Model { get; set; }
        public DateTime PurchaseDate { get; set; }
        public Guid serialNo { get; set; }

    }
}
