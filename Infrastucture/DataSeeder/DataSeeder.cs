
using Domain.Identity;
using Infrastucture.Persistence;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastucture.DataSeeder
{
    public class DataSeeder
    {
        public static async Task Seed(AppDbContext dbContext,UserManager<AppUser> _usermanager,RoleManager<AppRole> _roleManager)
        {
            if (dbContext == null)
            {
                return;
            }
            string[] roles = { "admin", "user" };
            foreach (string role in roles) 
            {
                if(!await _roleManager.RoleExistsAsync(role))
                {
                    var addRole = new AppRole()
                    {
                        Name = role,
                        Id = Guid.NewGuid(),
                    };
                    await _roleManager.CreateAsync(addRole);
                }
            }
            
            var existingUser = await _usermanager.FindByNameAsync("ahmetbuyukballi");
            if (existingUser != null) return;
            var adminUser = new AppUser()
            {
                FirstName = "Ahmet Hamdi",
                LastName = "Buyukballi",
                UserName = "ahmetbuyukballi",
                Email = "ahmetbuyukballi@gmail.com",
            };
           var result= await _usermanager.CreateAsync(adminUser, "Ahmet.07040");
            if (result.Succeeded)
            {
                await _usermanager.AddToRoleAsync(adminUser, "admin");
            }
            
           
        }
    }
}