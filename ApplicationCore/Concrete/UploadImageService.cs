using ApplicationCore.Abstraction;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Concrete
{
    public class UploadImageService : IUploadImageService
    {
        private readonly IWebHostEnvironment _env;
        public UploadImageService(IWebHostEnvironment env)
        {
            _env = env;
        }
        public async Task<string> UploadImageAsync(IFormFile file)
        {
            var fileName=Guid.NewGuid()+ Path.GetExtension(file.FileName);//Gelen dosyanın adını benzersiz yapç

            var path = Path.Combine(_env.WebRootPath, "Uploads");//Sonra da klasör yolunu al.

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);//Eğer böyle bir klasör yolu yoksa oluştur.
            }

            var filePath=Path.Combine(path, fileName);//Sonra tam dosya yolunu al.
            using var stream=new FileStream(filePath,FileMode.Create);//Bu dosyayı oluştur.Using kullanmamızın sebebi işimiz bittiğinde kapatmak.
            await file.CopyToAsync(stream);//Gelen dosya içeriğini bu dosyaya yaz.

            return $"/uploads/{fileName}";//Sonra bu dosya yolunu dışarı at.
        }
    }
}
