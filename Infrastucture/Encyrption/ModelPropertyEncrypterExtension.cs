using Domain.Attribute;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Infrastucture.Encyrption.EncyrptionConvertor;

namespace Infrastucture.Encyrption
{
    public static class ModelPropertyEncrypterExtension
    {
        public static void UseEncryption(this ModelBuilder modelBuilder)
        {
            var converter = new EncyrptionConvertors();

            foreach(var entity in modelBuilder.Model.GetEntityTypes())
            {
                foreach(var property in entity.GetProperties())
                {
                    var isEncrypted = property.PropertyInfo?
                        .GetCustomAttributes(typeof(EncryptPropertyAttribute), false)
                        .Any() ?? false;
                    if (isEncrypted)
                    {
                        property.SetValueConverter(converter);
                    }
                }
            }

            
        }
        private static bool IsDisriminator(IMutableProperty property)
      => property.Name == "Discriminator" || property.PropertyInfo == null;
    }
  
}
