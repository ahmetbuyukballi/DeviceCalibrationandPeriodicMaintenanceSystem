using ApplicationCore.BaseService;
using AutoMapper;
using Domain.Identity;
using Domain.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Abstraction
{
    public interface IGenericSingletionService<TModel>  where TModel : class
    {
        
    }
}
