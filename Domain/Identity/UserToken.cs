using Domain.Attribute;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Identity
{
    public class UserToken:IdentityUserToken<Guid>
    {
        [EncryptProperty]
        public override string? Value { get => base.Value; set => base.Value = value; }
    }
}
