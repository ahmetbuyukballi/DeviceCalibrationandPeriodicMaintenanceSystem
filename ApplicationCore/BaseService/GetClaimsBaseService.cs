using ApplicationCore.Abstraction;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.BaseService
{
    public abstract class GetClaimsBaseService 
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GetClaimsBaseService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public  Guid GetUserId()
        {
            var IdStr = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.TryParse(IdStr, out var id) ? id : Guid.Empty;
        }
    }
}
