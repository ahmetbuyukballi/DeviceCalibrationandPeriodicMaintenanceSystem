using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastucture.Encyrption
{
    public class EncyrptionConvertor
    {
        public class EncyrptionConvertors : ValueConverter<string, string>
        {
            public EncyrptionConvertors(RelationalConverterMappingHints hints=null)
                :base(
                     v=>AesService.EncryptStringToBytes(v),
                     v=>AesService.DecryptStringFromBytes(v),
                     hints)
            { }
        }
    }
}
