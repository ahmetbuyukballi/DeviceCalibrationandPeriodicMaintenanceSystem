using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Dto
{
    public class ErrorResponseDto
    {
        public string Message { get; set; }
        public int StatusCode {  get; set; } 
    }
}
