using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Responses
{
    using System.Net;

    public class ApiResponse<T>
    {
        
        public ApiResponse(HttpStatusCode httpStatusCode)
        {
            IsSuccess=false;
            ErrorMessages = new List<string>();
            HttpStatusCode = httpStatusCode;
        }
        public ApiResponse()
        {
        }

        public bool IsSuccess { get; set; }
        public T Result { get; set; }
        public List<string> ErrorMessages { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
    }

}
