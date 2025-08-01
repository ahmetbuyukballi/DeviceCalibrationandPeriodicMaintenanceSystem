using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Dto.Token
{
    public class RefreshTokenRequestDtos
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
