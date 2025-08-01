using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class EntityBase : IEntityBase
    {
        public Guid Id { get ; set; }
        public DateTime CreatedTime { get; set ; }=DateTime.Now.Date;
        public string? CreatedBy { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}
