using CloudinaryDotNet.Actions;
using Di2.Web.ViewModels.Pictures;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Di2.Services.Data
{
    public interface IPictureService
    {
        Task<IEnumerable<UploadResult>> Upload(ICollection<IFormFile> pictures, string orderId);

        void Delete(string itemId);

        Task Delete(string itemId, string pictureId);

        Task<T> GetPictureById<T>(string pictureId);

        HashSet<PictureViewModel> GetAllPictures<T>(string orderId);
    }
}
