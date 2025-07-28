using Domain.Identity;
using Infrastucture.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastucture
{
    public static class ServiceRegistiration
    {
       
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer("Server=AHMET\\MSSQLSERVER_2022;Database=DeviceSystem;User Id=sa;Password=123456;TrustServerCertificate=True;"));
        }
    }
}
