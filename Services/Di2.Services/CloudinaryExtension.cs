namespace Di2.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;

    public class CloudinaryExtension
    {
        public static async Task<List<string>> Upload(Cloudinary cloudinary, ICollection<IFormFile> files)
        {
            List<string> list = new List<string>();

            foreach (var file in files)
            {
                byte[] destinationImage;

                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    destinationImage = memoryStream.ToArray();
                }

                // UploadResult uploadResult = null;

                using (var destinationStream = new MemoryStream(destinationImage))
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        Folder = "Di2pix",
                        File = new FileDescription(file.FileName, destinationStream),
                    };
                    var res = await cloudinary.UploadAsync(uploadParams);
                    list.Add(res?.SecureUri.AbsoluteUri);
                }
            }

            return list;
        }
    }
}
