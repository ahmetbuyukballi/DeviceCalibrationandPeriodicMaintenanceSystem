using Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Dto.UserDto
{
    public class GetByIdUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string userName { get; set; }
        public string Email { get; set; }
        public string roles { get; set; }
    }
}
